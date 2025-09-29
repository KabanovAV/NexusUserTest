using NexusUserTest.Common.DTOs;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface ISettingAPIService
    {
        Task<SettingDTO?> GetSetting(int id, string? include = null);
        Task<SettingDTO?> AddSetting(SettingDTO item, string? include = null);
        Task<SettingDTO?> UpdateSetting(SettingDTO item, string? include = null);
        Task DeleteSetting(int id);
    }

    public class SettingAPIService(IHttpClientFactory httpClienFactory) : ISettingAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");

        public async Task<SettingDTO?> GetSetting(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/settings/{id}?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<SettingDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<SettingDTO?> AddSetting(SettingDTO item, string? include = null)
        {
            try
            {
                using var response = await _httpClient.PostAsJsonAsync($"api/settings?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<SettingDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<SettingDTO?> UpdateSetting(SettingDTO item, string? include = null)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/settings?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<SettingDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task DeleteSetting(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/settings/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
            }
        }
    }
}
