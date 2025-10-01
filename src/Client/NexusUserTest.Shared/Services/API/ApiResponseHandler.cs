using Serilog;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IApiResponseHandler
    {
        Task<ApiResponse<T>> ExecuteAsync<T>(Func<Task<T?>> apiCall, string operationName);
        Task<ApiResponse<T>> ExecuteHttpAsync<T>(Func<Task<HttpResponseMessage>> httpCall, string operationName) where T : class;
    }

    public class ApiResponseHandler : IApiResponseHandler
    {
        public async Task<ApiResponse<T>> ExecuteAsync<T>(Func<Task<T?>> apiCall, string operationName)
        {
            try
            {
                var result = await apiCall();

                if (result == null)
                {
                    Log.Warning("API call {OperationName} returned null", operationName);
                    return ApiResponse<T>.ErrorResult($"Operation {operationName} returned no data", "NO_DATA");
                }

                return ApiResponse<T>.SuccessResult(result);
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

        public async Task<ApiResponse<T>> ExecuteHttpAsync<T>(Func<Task<HttpResponseMessage>> httpCall, string operationName) where T : class
        {
            return await ExecuteAsync<T>(async () =>
            {
                var response = await httpCall();

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Log.Warning("HTTP request failed in {OperationName}: {StatusCode} - {Content}",
                        operationName, response.StatusCode, errorContent);
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<T>();
            }, operationName);
        }
    }
}
