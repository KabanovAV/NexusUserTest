using NexusUserTest.Common.DTOs;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IGroupUserAPIService
    {
        Task<IEnumerable<GroupUserDTO>> GetAllUsersByGroupId(int groupId, string? include = null);
        Task<GroupUserDTO?> GetGroupUser(int id, string? include = null);
        Task<GroupUserDTO?> UpdateGroupUser(GroupUserDTO item, string? include = null);
    }

    public class GroupUserAPIService : IGroupUserAPIService
    {
        private readonly HttpClient _httpClient;

        public GroupUserAPIService(IHttpClientFactory httpClienFactory)
        {
            _httpClient = httpClienFactory.CreateClient("HttpClient");
        }

        public async Task<IEnumerable<GroupUserDTO>> GetAllUsersByGroupId(int groupId, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groupusers/group/{groupId}?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<GroupUserDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return new List<GroupUserDTO>();
            }
        }

        public async Task<GroupUserDTO?> GetGroupUser(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groupusers/{id}?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupUserDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<GroupUserDTO?> UpdateGroupUser(GroupUserDTO item, string? include = null)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/groupusers?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupUserDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }
    }
}
