using Microsoft.AspNetCore.WebUtilities;
using NexusUserTest.Common;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IGroupUserAPIService
    {
        Task<ApiResponse<List<GroupUserInfoAdminDTO>>> GetAllInfoUsersByGroupId(int groupId, string? include = null);
        Task<ApiResponse<GroupUserInfoAdminDTO>> GetInfoGroupUser(int id, string? include = null);
        Task<ApiResponse<GroupUserTestDTO>> GetTestGroupUser(int id, string? include = null);
        Task<ApiResponse<Unit>> UpdateGroupUser(int id, GroupUserUpdateDTO item);
    }

    public class GroupUserAPIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : IGroupUserAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly IApiResponseHandler _responseHandler = responseHandler;

        public async Task<ApiResponse<List<GroupUserInfoAdminDTO>>> GetAllInfoUsersByGroupId(int groupId, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/groupusers/group/{groupId}/info", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<GroupUserInfoAdminDTO>>(() =>
                    _httpClient.GetAsync(url), "GetAllInfoUsersByGroupId");
            }
            return await _responseHandler.ExecuteHttpAsync<List<GroupUserInfoAdminDTO>>(() =>
                    _httpClient.GetAsync($"api/groupusers/group/{groupId}/info"), "GetAllInfoUsersByGroupId");
        }

        public async Task<ApiResponse<GroupUserInfoAdminDTO>> GetInfoGroupUser(int id, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/groupusers/{id}/info", "include", include);
                return await _responseHandler.ExecuteHttpAsync<GroupUserInfoAdminDTO>(() =>
                    _httpClient.GetAsync(url), "GetInfoGroupUser");
            }
            return await _responseHandler.ExecuteHttpAsync<GroupUserInfoAdminDTO>(() =>
                    _httpClient.GetAsync($"api/groupusers/{id}/info"), "GetInfoGroupUser");
        }

        public async Task<ApiResponse<GroupUserTestDTO>> GetTestGroupUser(int id, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/groupusers/{id}/test", "include", include);
                return await _responseHandler.ExecuteHttpAsync<GroupUserTestDTO>(() =>
                    _httpClient.GetAsync(url), "GetTestGroupUser");
            }
            return await _responseHandler.ExecuteHttpAsync<GroupUserTestDTO>(() =>
                    _httpClient.GetAsync($"api/groupusers/{id}/test"), "GetTestGroupUser");
        }

        public async Task<ApiResponse<Unit>> UpdateGroupUser(int id, GroupUserUpdateDTO item)
            => await _responseHandler.ExecuteHttpAsync(() =>
                    _httpClient.PatchAsJsonAsync($"api/groupusers/{id}", item), "UpdateGroupUser");
    }
}
