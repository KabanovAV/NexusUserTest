using NexusUserTest.Common.DTOs;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IGroupUserAPIService
    {
        Task<List<GroupUserInfoAdminDTO>> GetAllInfoUsersByGroupId(int groupId, string? include = null);        
        Task<GroupUserInfoAdminDTO?> GetInfoGroupUser(int id, string? include = null);
        Task<GroupUserTestDTO?> GetTestGroupUser(int id, string? include = null);
        Task<GroupUserInfoAdminDTO?> UpdateInfoGroupUser(GroupUserInfoAdminDTO item, string? include = null);
        Task<GroupUserTestDTO?> UpdateTestGroupUser(GroupUserTestDTO item, string? include = null);
    }

    public class GroupUserAPIService(IHttpClientFactory httpClienFactory) : IGroupUserAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");

        public async Task<List<GroupUserInfoAdminDTO>> GetAllInfoUsersByGroupId(int groupId, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groupusers/group/{groupId}/info?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<GroupUserInfoAdminDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return [];
            }
        }

        public async Task<GroupUserInfoAdminDTO?> GetInfoGroupUser(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groupusers/{id}/info?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupUserInfoAdminDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<GroupUserTestDTO?> GetTestGroupUser(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groupusers/{id}/test?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupUserTestDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<GroupUserInfoAdminDTO?> UpdateInfoGroupUser(GroupUserInfoAdminDTO item, string? include = null)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/groupusers/info?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupUserInfoAdminDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<GroupUserTestDTO?> UpdateTestGroupUser(GroupUserTestDTO item, string? include = null)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/groupusers/test?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupUserTestDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }
    }
}
