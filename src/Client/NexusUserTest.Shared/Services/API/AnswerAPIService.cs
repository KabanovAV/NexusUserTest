using Microsoft.AspNetCore.WebUtilities;
using NexusUserTest.Common;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IAnswerAPIService
    {
        Task<ApiResponse<List<AnswerAdminDTO>>> GetAllAnswer(string? include = null);
        Task<ApiResponse<AnswerAdminDTO>> GetAnswer(int id, string? include = null);
        Task<ApiResponse<AnswerAdminDTO>> AddAnswer(AnswerAdminDTO item, string? include = null);
        Task<ApiResponse<List<AnswerAdminDTO>>> AddRangeAnswer(IEnumerable<AnswerAdminDTO> items, string? include = null);
        Task<ApiResponse<Unit>> UpdateAnswer(AnswerAdminDTO item);
        Task<ApiResponse<Unit>> DeleteAnswer(int id);
    }

    public class AnswerAPIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : IAnswerAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly IApiResponseHandler _responseHandler = responseHandler;

        public async Task<ApiResponse<List<AnswerAdminDTO>>> GetAllAnswer(string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/answers", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<AnswerAdminDTO>>(() =>
                    _httpClient.GetAsync(url), "GetAllAnswer");
            }
            return await _responseHandler.ExecuteHttpAsync<List<AnswerAdminDTO>>(() =>
                    _httpClient.GetAsync("api/answers"), "GetAllAnswer");
        }

        public async Task<ApiResponse<AnswerAdminDTO>> GetAnswer(int id, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/answers/{id}", "include", include);
                return await _responseHandler.ExecuteHttpAsync<AnswerAdminDTO>(() =>
                    _httpClient.GetAsync(url), "GetAnswer");
            }
            return await _responseHandler.ExecuteHttpAsync<AnswerAdminDTO>(() =>
                    _httpClient.GetAsync($"api/answers/{id}"), "GetAnswer");
        }

        public async Task<ApiResponse<AnswerAdminDTO>> AddAnswer(AnswerAdminDTO item, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/answers", "include", include);
                return await _responseHandler.ExecuteHttpAsync<AnswerAdminDTO>(() =>
                    _httpClient.PostAsJsonAsync(url, item), "AddAnswer");
            }
            return await _responseHandler.ExecuteHttpAsync<AnswerAdminDTO>(() =>
                    _httpClient.PostAsJsonAsync("api/answers", item), "AddAnswer");
        }

        public async Task<ApiResponse<List<AnswerAdminDTO>>> AddRangeAnswer(IEnumerable<AnswerAdminDTO> items, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/answers/batch", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<AnswerAdminDTO>>(() =>
                    _httpClient.PostAsJsonAsync(url, items), "AddRangeAnswer");
            }
            return await _responseHandler.ExecuteHttpAsync<List<AnswerAdminDTO>>(() =>
                    _httpClient.PostAsJsonAsync("api/answers/batch", items), "AddRangeAnswer");
        }

        public async Task<ApiResponse<Unit>> UpdateAnswer(AnswerAdminDTO item)
            => await _responseHandler.ExecuteHttpAsync(() =>
                _httpClient.PutAsJsonAsync($"api/answers/{item.Id}", item), "UpdateAnswer");

        public async Task<ApiResponse<Unit>> DeleteAnswer(int id)
            => await _responseHandler.ExecuteHttpAsync(() =>
                _httpClient.DeleteAsync($"api/answers/{id}"), "DeleteAnswer");
    }
}
