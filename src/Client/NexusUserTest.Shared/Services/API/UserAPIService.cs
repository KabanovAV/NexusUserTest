using NexusUserTest.Common.DTOs;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IUserAPIService
    {
        Task<IEnumerable<UserDTO>> GetAllUser(string? include = null);
        Task<UserDTO?> GetUser(int id, string? include = null);
        Task<UserDTO?> AddUser(UserDTO item, string? include = null);
        Task<UserDTO?> UpdateUser(UserDTO item, string? include = null);
        Task DeleteUser(int id);
    }

    public class UserAPIService : IUserAPIService
    {
        private readonly HttpClient _httpClient;

        public UserAPIService(IHttpClientFactory httpClienFactory)
        {
            _httpClient = httpClienFactory.CreateClient("HttpClient");
        }

        public async Task<IEnumerable<UserDTO>> GetAllUser(string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/users?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<UserDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return new List<UserDTO>();
            }
        }

        public async Task<UserDTO?> GetUser(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/users/{id}?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<UserDTO?> AddUser(UserDTO item, string? include = null)
        {
            try
            {
                using var response = await _httpClient.PostAsJsonAsync($"api/users?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<UserDTO>(); ;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<UserDTO?> UpdateUser(UserDTO item, string? include = null)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/users?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<UserDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task DeleteUser(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/users/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
            }
        }
    }
}
