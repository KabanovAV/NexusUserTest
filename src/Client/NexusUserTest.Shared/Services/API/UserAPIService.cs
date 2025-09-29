using NexusUserTest.Common.DTOs;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IUserAPIService
    {
        Task<List<UserDTO>> GetAllUser(string? include = null);
        Task<UserDTO?> GetUser(int id, string? include = null);
        Task<UserInfoTestDTO?> GetUserTestInfo(int id, string? include = null);
        Task<UserDTO?> AddUser(UserDTO item, string? include = null);
        Task<UserDTO?> UpdateUser(UserDTO item, string? include = null);
        Task DeleteUser(int id);
    }

    public class UserAPIService(IHttpClientFactory httpClienFactory) : IUserAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");

        public async Task<List<UserDTO>> GetAllUser(string? include = null)
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
                return [];
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

        public async Task<UserInfoTestDTO?> GetUserTestInfo(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/users/{id}/test?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<UserInfoTestDTO>();
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
