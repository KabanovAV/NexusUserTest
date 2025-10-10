using Serilog;
using System.Net.Http.Json;
using System.Text.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IApiResponseHandler
    {
        Task<ApiResponse<T>> ExecuteHttpAsync<T>(Func<Task<HttpResponseMessage>> httpCall, string operationName);
        Task<ApiResponse<Unit>> ExecuteHttpAsync(Func<Task<HttpResponseMessage>> httpCall, string operationName);
    }

    public class ApiResponseHandler : IApiResponseHandler
    {
        public async Task<ApiResponse<T>> ExecuteHttpAsync<T>(Func<Task<HttpResponseMessage>> httpCall, string operationName)
        {
            try
            {
                var response = await httpCall();
                if (!response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var errorContent = result.GetProperty("message").GetString()!;

                    Log.Error("HTTP request failed in {OperationName}: {StatusCode} - {Content}",
                        operationName, response.StatusCode, errorContent);
                    return ApiResponse<T>.ErrorResult($"{errorContent}", $"{response.StatusCode}");
                }
                return ApiResponse<T>.SuccessResult(await response.Content.ReadFromJsonAsync<T>());
            }
            catch (HttpRequestException ex)
            {
                Log.Error(ex, "HTTP error in {OperationName}: {Message}", operationName, ex.Message);
                return ApiResponse<T>.ErrorResult($"Network error: {ex.Message}", "NETWORK_ERROR");
            }
            catch (TimeoutException ex)
            {
                Log.Error(ex, "Timeout in {OperationName}: {Message}", operationName, ex.Message);
                return ApiResponse<T>.ErrorResult("Request timeout", "TIMEOUT");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error in {OperationName}: {Message}", operationName, ex.Message);
                return ApiResponse<T>.ErrorResult($"Unexpected error: {ex.Message}", "UNEXPECTED_ERROR");
            }
        }

        public async Task<ApiResponse<Unit>> ExecuteHttpAsync(Func<Task<HttpResponseMessage>> httpCall, string operationName)
        {
            try
            {
                var response = await httpCall();
                if (!response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<JsonElement>();
                    var errorContent = result.GetProperty("message").GetString()!;

                    Log.Error("HTTP request failed in {OperationName}: {StatusCode} - {Content}",
                        operationName, response.StatusCode, errorContent);
                    return ApiResponse<Unit>.ErrorResult($"{errorContent}", $"{response.StatusCode}");
                }
                return ApiResponse<Unit>.SuccessResult(Unit.Value);
            }
            catch (HttpRequestException ex)
            {
                Log.Error(ex, "HTTP error in {OperationName}: {Message}", operationName, ex.Message);
                return ApiResponse<Unit>.ErrorResult($"Network error: {ex.Message}", "NETWORK_ERROR");
            }
            catch (TimeoutException ex)
            {
                Log.Error(ex, "Timeout in {OperationName}: {Message}", operationName, ex.Message);
                return ApiResponse<Unit>.ErrorResult("Request timeout", "TIMEOUT");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected error in {OperationName}: {Message}", operationName, ex.Message);
                return ApiResponse<Unit>.ErrorResult($"Unexpected error: {ex.Message}", "UNEXPECTED_ERROR");
            }
        }
    }
}
