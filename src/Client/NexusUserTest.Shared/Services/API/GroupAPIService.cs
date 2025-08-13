using NexusUserTest.Common.DTOs;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IGroupAPIService
    {
        Task<IEnumerable<GroupDTO>> GetAllGroup(string? include = null);
        Task<IEnumerable<SelectItem>> GetGroupSelect();
        Task<GroupDTO?> GetGroup(int id, string? include = null);
        Task<GroupDTO?> AddGroup(GroupDTO item, string? include = null);
        Task<GroupDTO?> UpdateGroup(GroupDTO item, string? include = null);
        Task DeleteGroup(int id);
    }

    public class GroupAPIService : IGroupAPIService
    {
        private readonly HttpClient _httpClient;

        public GroupAPIService(IHttpClientFactory httpClienFactory)
        {
            _httpClient = httpClienFactory.CreateClient("HttpClient");
        }

        public async Task<IEnumerable<GroupDTO>> GetAllGroup(string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groups?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<GroupDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return new List<GroupDTO>();
            }
        }

        public async Task<IEnumerable<SelectItem>> GetGroupSelect()
        {
            var s = await GetAllGroup();
            return s.Select(s => new SelectItem { Text = s.Title, Value = s.Id }).ToList();
        }

        public async Task<GroupDTO?> GetGroup(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/groups/{id}?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<GroupDTO?> AddGroup(GroupDTO item, string? include = null)
        {
            try
            {
                using var response = await _httpClient.PostAsJsonAsync($"api/groups?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<GroupDTO?> UpdateGroup(GroupDTO item, string? include = null)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/groups?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GroupDTO>();
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
