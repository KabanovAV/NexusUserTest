using NexusUserTest.Common.DTOs;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IGroupAPIService
    {
        Task<List<GroupInfoDTO>> GetAllInfoGroup(string? include = null);
        Task<List<GroupEditDTO>> GetAllEditGroup(string? include = null);
        Task<GroupInfoDetailsDTO?> GetInfoDetailsGroup(int id, string? include = null);        
        Task<GroupEditDTO?> GetEditGroup(int id, string? include = null);
        Task<List<SelectItem>> GetGroupSelect();
        Task<GroupEditDTO?> AddGroup(GroupEditDTO item, string? include = null);
        Task<GroupEditDTO?> UpdateGroup(GroupEditDTO item, string? include = null);
        Task DeleteGroup(int id);
    }

    public class GroupAPIService(IHttpClientFactory httpClienFactory) : IGroupAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");

        public async Task<List<GroupInfoDTO>> GetAllInfoGroup(string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groups?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<GroupInfoDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return [];
            }
        }

        public async Task<List<GroupEditDTO>> GetAllEditGroup(string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groups?view=edit&include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<GroupEditDTO>>() ?? [];
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
                var response = await _httpClient.GetAsync($"api/groups/{id}?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupInfoDetailsDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }        

        public async Task<GroupEditDTO?> GetEditGroup(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groups/{id}?view=edit&include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupEditDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<List<SelectItem>> GetGroupSelect()
        {
            var s = await GetAllEditGroup();
            return [.. s.Select(s => new SelectItem { Text = s.Title, Value = s.Id })];
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
