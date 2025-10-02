using Microsoft.AspNetCore.WebUtilities;
using NexusUserTest.Common;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IQuestionAPIService
    {
        Task<ApiResponse<List<QuestionAdminDTO>>> GetAllQuestion(string? include = null);
        Task<ApiResponse<QuestionAdminDTO>> GetQuestion(int id, string? include = null);
        Task<ApiResponse<QuestionAdminDTO>> AddQuestion(QuestionAdminDTO item, string? include = null);
        Task<ApiResponse<Unit>> UpdateQuestion(QuestionAdminDTO item, string? include = null);
        Task<ApiResponse<Unit>> DeleteQuestion(int id);
    }

    public class QuestionAPIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : IQuestionAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly IApiResponseHandler _responseHandler = responseHandler;

        public async Task<ApiResponse<List<QuestionAdminDTO>>> GetAllQuestion(string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/questions", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<QuestionAdminDTO>>(() =>
                    _httpClient.GetAsync(url), "GetAllQuestion");
            }
            return await _responseHandler.ExecuteHttpAsync<List<QuestionAdminDTO>>(() =>
                    _httpClient.GetAsync("api/questions"), "GetAllQuestion");
        }

        public async Task<ApiResponse<QuestionAdminDTO>> GetQuestion(int id, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/questions/{id}", "include", include);
                return await _responseHandler.ExecuteHttpAsync<QuestionAdminDTO>(() =>
                    _httpClient.GetAsync(url), "GetQuestion");
            }
            return await _responseHandler.ExecuteHttpAsync<QuestionAdminDTO>(() =>
                    _httpClient.GetAsync($"api/questions/{id}"), "GetQuestion");
        }

        public async Task<ApiResponse<QuestionAdminDTO>> AddQuestion(QuestionAdminDTO item, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/questions", "include", include);
                return await _responseHandler.ExecuteHttpAsync<QuestionAdminDTO>(() =>
                    _httpClient.PostAsJsonAsync(url, item), "AddQuestion");
            }
            return await _responseHandler.ExecuteHttpAsync<QuestionAdminDTO>(() =>
                    _httpClient.PostAsJsonAsync("api/questions", item), "AddQuestion");
        }

        public async Task<ApiResponse<Unit>> UpdateQuestion(QuestionAdminDTO item, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/questions/{item.Id}", "include", include);
                return await _responseHandler.ExecuteHttpAsync(() =>
                    _httpClient.PutAsJsonAsync(url, item), "UpdateQuestion");
            }
            return await _responseHandler.ExecuteHttpAsync(() =>
                    _httpClient.PutAsJsonAsync($"api/questions/{item.Id}", item), "UpdateQuestion");
        }

        public async Task<ApiResponse<Unit>> DeleteQuestion(int id)
            => await _responseHandler.ExecuteHttpAsync(() =>
                    _httpClient.DeleteAsync($"api/questions/{id}"), "DeleteQuestion");
    }
}
