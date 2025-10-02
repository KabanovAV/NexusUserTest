using Microsoft.AspNetCore.WebUtilities;
using NexusUserTest.Common;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IUserAPIService
    {
        Task<ApiResponse<List<UserAdminDTO>>> GetAllUser(string? include = null);
        Task<ApiResponse<UserAdminDTO>> GetUser(int id, string? include = null);
        Task<ApiResponse<UserInfoTestDTO>> GetUserTestInfo(int id, string? include = null);
        Task<ApiResponse<UserAdminDTO>> AddUser(UserAdminDTO item, string? include = null);
        Task<ApiResponse<Unit>> UpdateUser(UserAdminDTO item, string? include = null);
        Task<ApiResponse<Unit>> DeleteUser(int id);
    }

    public class UserAPIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : IUserAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly IApiResponseHandler _responseHandler = responseHandler;

        public async Task<ApiResponse<List<UserAdminDTO>>> GetAllUser(string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/users", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<UserAdminDTO>>(() =>
                    _httpClient.GetAsync(url), "GetAllUser");
            }
            return await _responseHandler.ExecuteHttpAsync<List<UserAdminDTO>>(() =>
                    _httpClient.GetAsync("api/users"), "GetAllUser");
        }

        public async Task<ApiResponse<UserAdminDTO>> GetUser(int id, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/users/{id}", "include", include);
                return await _responseHandler.ExecuteHttpAsync<UserAdminDTO>(() =>
                    _httpClient.GetAsync(url), "GetUser");
            }
            return await _responseHandler.ExecuteHttpAsync<UserAdminDTO>(() =>
                _httpClient.GetAsync($"api/users/{id}"), "GetUser");
        }

        public async Task<ApiResponse<UserInfoTestDTO>> GetUserTestInfo(int id, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/users/{id}/test", "include", include);
                return await _responseHandler.ExecuteHttpAsync<UserInfoTestDTO>(() =>
                    _httpClient.GetAsync(url), "GetUserTestInfo");
            }
            return await _responseHandler.ExecuteHttpAsync<UserInfoTestDTO>(() =>
                _httpClient.GetAsync($"api/users/{id}/test"), "GetUserTestInfo");
        }

        public async Task<ApiResponse<UserAdminDTO>> AddUser(UserAdminDTO item, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString("api/users", "include", include);
                return await _responseHandler.ExecuteHttpAsync<UserAdminDTO>(() =>
                    _httpClient.PostAsJsonAsync(url, item), "AddUser");
            }
            return await _responseHandler.ExecuteHttpAsync<UserAdminDTO>(() =>
                _httpClient.PostAsJsonAsync("api/users", item), "AddUser");
        }

        public async Task<ApiResponse<Unit>> UpdateUser(UserAdminDTO item, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/users/{item.Id}", "include", include);
                return await _responseHandler.ExecuteHttpAsync(() =>
                    _httpClient.PutAsJsonAsync(url, item), "UpdateUser");
            }
            return await _responseHandler.ExecuteHttpAsync(() =>
                _httpClient.PutAsJsonAsync($"api/users/{item.Id}", item), "UpdateUser");
        }

        public async Task<ApiResponse<Unit>> DeleteUser(int id)
            => await _responseHandler.ExecuteHttpAsync(() =>
                _httpClient.DeleteAsync($"api/users/{id}"), "DeleteUser");
    }
}
