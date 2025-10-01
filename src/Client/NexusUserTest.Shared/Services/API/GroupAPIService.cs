using NexusUserTest.Common;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IGroupAPIService
    {
        Task<ApiResponse<List<GroupInfoDTO>>> GetAllInfoGroup(string? include = null);
        Task<ApiResponse<List<GroupEditDTO>>> GetAllEditGroup(string? include = null);
        Task<ApiResponse<GroupInfoDetailsDTO>> GetInfoDetailsGroup(int id, string? include = null);        
        Task<ApiResponse<GroupEditDTO>> GetEditGroup(int id, string? include = null);
        Task<ApiResponse<List<SelectItem>>> GetGroupSelect();
        Task<ApiResponse<GroupEditDTO>> AddGroup(GroupEditDTO item, string? include = null);
        Task<ApiResponse<GroupEditDTO>> UpdateGroup(GroupEditDTO item, string? include = null);
        Task<ApiResponse<bool>> DeleteGroup(int id);
    }

    public class GroupAPIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : IGroupAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly IApiResponseHandler _responseHandler = responseHandler;

        public async Task<ApiResponse<List<GroupInfoDTO>>> GetAllInfoGroup(string? include = null)
            => await _responseHandler.ExecuteHttpAsync<List<GroupInfoDTO>>(async () =>
            {
                return await _httpClient.GetAsync($"api/groups?include={include}");
            }, "GetAllInfoGroup");

        public async Task<ApiResponse<List<GroupEditDTO>>> GetAllEditGroup(string? include = null)
            => await _responseHandler.ExecuteHttpAsync<List<GroupEditDTO>>(async () =>
            {
                return await _httpClient.GetAsync($"api/groups?view=edit&include={include}");
            }, "GetAllEditGroup");

        public async Task<ApiResponse<GroupInfoDetailsDTO>> GetInfoDetailsGroup(int id, string? include = null)
            => await _responseHandler.ExecuteHttpAsync<GroupInfoDetailsDTO>(async () =>
            {
                return await _httpClient.GetAsync($"api/groups/{id}?include={include}");
            }, "GetInfoDetailsGroup");        

        public async Task<ApiResponse<GroupEditDTO>> GetEditGroup(int id, string? include = null)
            => await _responseHandler.ExecuteHttpAsync<GroupEditDTO>(async () =>
            {
                return await _httpClient.GetAsync($"api/groups/{id}?view=edit&include={include}");
            }, "GetEditGroup");

        public async Task<ApiResponse<List<SelectItem>>> GetGroupSelect()
            => await _responseHandler.ExecuteHttpAsync<List<SelectItem>>(async () =>
            {
                return await _httpClient.GetAsync($"api/groups/select");
            }, "GetGroupSelect");

        public async Task<ApiResponse<GroupEditDTO>> AddGroup(GroupEditDTO item, string? include = null)
            => await _responseHandler.ExecuteHttpAsync<GroupEditDTO>(async () =>
            {
                return await _httpClient.PostAsJsonAsync($"api/groups?include={include}", item);
            }, "AddGroup");

        public async Task<ApiResponse<GroupEditDTO>> UpdateGroup(GroupEditDTO item, string? include = null)
            => await _responseHandler.ExecuteHttpAsync<GroupEditDTO>(async () =>
            {
                return await _httpClient.PutAsJsonAsync($"api/groups?include={include}", item);
            }, "UpdateGroup");

        public async Task<ApiResponse<bool>> DeleteGroup(int id)
            => await _responseHandler.ExecuteAsync<bool>(async () =>
            {
                var response = await _httpClient.DeleteAsync($"api/groups/{id}");
                return await response.Content.ReadFromJsonAsync<bool>();
            }, "DeleteGroup");
    }
}
