using Microsoft.AspNetCore.WebUtilities;
using NexusUserTest.Common;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IGroupAPIService
    {
        Task<ApiResponse<List<GroupDTO>>> GetAllGroup(string? include = null);
        Task<ApiResponse<GroupDTO>> GetGroup(int id, string? include = null);
        Task<ApiResponse<List<GroupInfoDTO>>> GetAllInfoGroup(string? include = null);        
        Task<ApiResponse<GroupInfoDetailsDTO>> GetInfoDetailsGroup(int id, string? include = null);        
        Task<ApiResponse<List<SelectItem>>> GetGroupSelect();
        Task<ApiResponse<GroupDTO>> AddGroup(GroupDTO item, string? include = null);
        Task<ApiResponse<Unit>> UpdateGroup(GroupDTO item);
        Task<ApiResponse<Unit>> DeleteGroup(int id);
    }

    public class GroupAPIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : IGroupAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly IApiResponseHandler _responseHandler = responseHandler;

        public async Task<ApiResponse<List<GroupDTO>>> GetAllGroup(string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/groups", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<GroupDTO>>(() =>
                    _httpClient.GetAsync(url), "GetAllEditGroup");
            }
            return await _responseHandler.ExecuteHttpAsync<List<GroupDTO>>(() =>
                    _httpClient.GetAsync("api/groups"), "GetAllEditGroup");
        }

        public async Task<ApiResponse<GroupDTO>> GetGroup(int id, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/groups/{id}", "include", include);
                return await _responseHandler.ExecuteHttpAsync<GroupDTO>(() =>
                    _httpClient.GetAsync(url), "GetEditGroup");
            }
            return await _responseHandler.ExecuteHttpAsync<GroupDTO>(() =>
                _httpClient.GetAsync($"api/groups/{id}"), "GetEditGroup");
        }

        public async Task<ApiResponse<List<GroupInfoDTO>>> GetAllInfoGroup(string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/groups/info", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<GroupInfoDTO>>(() =>
                    _httpClient.GetAsync(url), "GetAllInfoGroup");
            }
            return await _responseHandler.ExecuteHttpAsync<List<GroupInfoDTO>>(() =>
                    _httpClient.GetAsync("api/groups/info"), "GetAllInfoGroup");
        }

        public async Task<ApiResponse<GroupInfoDetailsDTO>> GetInfoDetailsGroup(int id, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/groups/{id}/info", "include", include);
                return await _responseHandler.ExecuteHttpAsync<GroupInfoDetailsDTO>(() =>
                    _httpClient.GetAsync(url), "GetInfoDetailsGroup");
            }
            return await _responseHandler.ExecuteHttpAsync<GroupInfoDetailsDTO>(() =>
                _httpClient.GetAsync($"api/groups/{id}/info"), "GetInfoDetailsGroup");
        }

        public async Task<ApiResponse<List<SelectItem>>> GetGroupSelect()
            => await _responseHandler.ExecuteHttpAsync<List<SelectItem>>(() =>
                _httpClient.GetAsync("api/groups/select"), "GetGroupSelect");

        public async Task<ApiResponse<GroupDTO>> AddGroup(GroupDTO item, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/groups", "include", include);
                return await _responseHandler.ExecuteHttpAsync<GroupDTO>(() =>
                    _httpClient.PostAsJsonAsync(url, item), "AddGroup");
            }
            return await _responseHandler.ExecuteHttpAsync<GroupDTO>(() =>
                _httpClient.PostAsJsonAsync("api/groups", item), "AddGroup");
        }

        public async Task<ApiResponse<Unit>> UpdateGroup(GroupDTO item)
            => await _responseHandler.ExecuteHttpAsync(() =>
                _httpClient.PutAsJsonAsync($"api/groups/{item.Id}", item), "UpdateGroup");

        public async Task<ApiResponse<Unit>> DeleteGroup(int id)
            => await _responseHandler.ExecuteHttpAsync(() =>
                _httpClient.DeleteAsync($"api/groups/{id}"), "DeleteGroup");
    }
}
