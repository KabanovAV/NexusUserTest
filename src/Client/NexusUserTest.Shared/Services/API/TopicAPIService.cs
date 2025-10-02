using Microsoft.AspNetCore.WebUtilities;
using NexusUserTest.Common;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface ITopicAPIService
    {
        Task<ApiResponse<List<TopicDTO>>> GetAllTopic(string? include = null);
        Task<ApiResponse<TopicDTO>> GetTopic(int id, string? include = null);
        Task<ApiResponse<List<SelectItem>>> GetTopicSelect();
        Task<ApiResponse<TopicDTO>> AddTopic(TopicDTO item, string? include = null);
        Task<ApiResponse<Unit>> UpdateTopic(TopicDTO item);
        Task<ApiResponse<Unit>> DeleteTopic(int id);
    }

    public class TopicAPIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : ITopicAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly IApiResponseHandler _responseHandler = responseHandler;

        public async Task<ApiResponse<List<TopicDTO>>> GetAllTopic(string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/topics", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<TopicDTO>>(() =>
                    _httpClient.GetAsync(url), "GetAllTopic");
            }
            return await _responseHandler.ExecuteHttpAsync<List<TopicDTO>>(() =>
                    _httpClient.GetAsync("api/topics"), "GetAllTopic");
        }

        public async Task<ApiResponse<TopicDTO>> GetTopic(int id, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/topics/{id}", "include", include);
                return await _responseHandler.ExecuteHttpAsync<TopicDTO>(() =>
                    _httpClient.GetAsync(url), "GetTopic");
            }
            return await _responseHandler.ExecuteHttpAsync<TopicDTO>(() =>
                    _httpClient.GetAsync($"api/topics/{id}"), "GetTopic");
        }

        public async Task<ApiResponse<List<SelectItem>>> GetTopicSelect()
        => await _responseHandler.ExecuteHttpAsync<List<SelectItem>>(() =>
                _httpClient.GetAsync("api/topics/select"), "GetTopicSelect");

        public async Task<ApiResponse<TopicDTO>> AddTopic(TopicDTO item, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/topics", "include", include);
                return await _responseHandler.ExecuteHttpAsync<TopicDTO>(() =>
                    _httpClient.PostAsJsonAsync(url, item), "AddTopic");
            }
            return await _responseHandler.ExecuteHttpAsync<TopicDTO>(() =>
                    _httpClient.PostAsJsonAsync("api/topics", item), "AddTopic");
        }

        public async Task<ApiResponse<Unit>> UpdateTopic(TopicDTO item)
            => await _responseHandler.ExecuteHttpAsync(() =>
                    _httpClient.PutAsJsonAsync($"api/topics/{item.Id}", item), "UpdateTopic");

        public async Task<ApiResponse<Unit>> DeleteTopic(int id)
            => await _responseHandler.ExecuteHttpAsync(() =>
                    _httpClient.DeleteAsync($"api/topics/{id}"), "DeleteTopic");
    }
}
