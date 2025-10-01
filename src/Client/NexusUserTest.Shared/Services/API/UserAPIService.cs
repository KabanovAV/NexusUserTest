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
        Task<ApiResponse<UserAdminDTO>> UpdateUser(UserAdminDTO item, string? include = null);
        Task<ApiResponse<bool>> DeleteUser(int id);
    }

    public class UserAPIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : IUserAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly IApiResponseHandler _responseHandler = responseHandler;

        public async Task<ApiResponse<List<UserAdminDTO>>> GetAllUser(string? include = null)
            => await _responseHandler.ExecuteHttpAsync<List<UserAdminDTO>>(async () =>
            {
                return await _httpClient.GetAsync($"api/users?include={include}");
            }, "GetAllUser");

        public async Task<ApiResponse<UserAdminDTO>> GetUser(int id, string? include = null)
            => await _responseHandler.ExecuteHttpAsync<UserAdminDTO>(async () =>
            {
                return await _httpClient.GetAsync($"api/users/{id}?include={include}");
            }, "GetUser");

        public async Task<ApiResponse<UserInfoTestDTO>> GetUserTestInfo(int id, string? include = null)
            => await _responseHandler.ExecuteHttpAsync<UserInfoTestDTO>(async () =>
            {
                return await _httpClient.GetAsync($"api/users/{id}?view=test&include={include}");
            }, "GetUserTestInfo");

        public async Task<ApiResponse<UserAdminDTO>> AddUser(UserAdminDTO item, string? include = null)
            => await _responseHandler.ExecuteHttpAsync<UserAdminDTO>(async () =>
            {
                return await _httpClient.PostAsJsonAsync($"api/users?include={include}", item);
            }, "AddUser");

        public async Task<ApiResponse<UserAdminDTO>> UpdateUser(UserAdminDTO item, string? include = null)
            => await _responseHandler.ExecuteHttpAsync<UserAdminDTO>(async () =>
            {
                return await _httpClient.PutAsJsonAsync($"api/users/{item.Id}?include={include}", item);
            }, "UpdateUser");

        public async Task<ApiResponse<bool>> DeleteUser(int id)
            => await _responseHandler.ExecuteAsync<bool>(async () =>
            {
                var response = await _httpClient.DeleteAsync($"api/users/{id}");
                return await response.Content.ReadFromJsonAsync<bool>();
            }, "DeleteUser");
    }
}
