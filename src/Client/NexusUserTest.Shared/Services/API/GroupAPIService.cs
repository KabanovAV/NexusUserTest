using NexusUserTest.Common.DTOs;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IGroupAPIService
    {
        Task<IEnumerable<GroupInfoDTO>> GetAllInfoGroup(string? include = null);
        Task<GroupInfoDetailsDTO?> GetInfoDetailsGroup(int id, string? include = null);
        Task<IEnumerable<GroupEditDTO>> GetAllEditGroup(string? include = null);
        Task<GroupEditDTO?> GetEditGroup(int id, string? include = null);
        Task<IEnumerable<SelectItem>> GetGroupSelect();
        Task<GroupEditDTO?> AddGroup(GroupEditDTO item, string? include = null);
        Task<GroupEditDTO?> UpdateGroup(GroupEditDTO item, string? include = null);
        Task DeleteGroup(int id);
    }

    public class GroupAPIService : IGroupAPIService
    {
        private readonly HttpClient _httpClient;

        public GroupAPIService(IHttpClientFactory httpClienFactory)
        {
            _httpClient = httpClienFactory.CreateClient("HttpClient");
        }

        public async Task<IEnumerable<GroupInfoDTO>> GetAllInfoGroup(string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groups/info?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<GroupInfoDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return [];
            }
        }

        public async Task<GroupInfoDetailsDTO?> GetInfoDetailsGroup(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groups/{id}/info?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupInfoDetailsDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<GroupEditDTO>> GetAllEditGroup(string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groups/edit?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<GroupEditDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return [];
            }
        }

        public async Task<GroupEditDTO?> GetEditGroup(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groups/{id}/edit?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupEditDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<SelectItem>> GetGroupSelect()
        {
            var s = await GetAllEditGroup();
            return s.Select(s => new SelectItem { Text = s.Title, Value = s.Id }).ToList();
        }

        public async Task<GroupEditDTO?> AddGroup(GroupEditDTO item, string? include = null)
        {
            try
            {
                using var response = await _httpClient.PostAsJsonAsync($"api/groups?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupEditDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<GroupEditDTO?> UpdateGroup(GroupEditDTO item, string? include = null)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/groups?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupEditDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task DeleteGroup(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/groups/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
            }
        }
    }
}
