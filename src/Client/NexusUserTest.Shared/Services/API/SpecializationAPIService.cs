using NexusUserTest.Common.DTOs;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface ISpecializationAPIService
    {
        Task<List<SpecializationDTO>> GetAllSpecialization(string? include = null);
        Task<List<SelectItem>> GetSpecializationSelect();
        Task<SpecializationDTO?> GetSpecialization(int id, string? include = null);
        Task<SpecializationDTO?> AddSpecialization(SpecializationDTO item, string? include = null);
        Task<SpecializationDTO?> UpdateSpecialization(SpecializationDTO item, string? include = null);
        Task<bool> DeleteSpecialization(int id);
    }

    public class SpecializationAPIService(IHttpClientFactory httpClienFactory) : ISpecializationAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");

        public async Task<List<SpecializationDTO>> GetAllSpecialization(string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/specializations?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<SpecializationDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return [];
            }
        }

        public async Task<List<SelectItem>> GetSpecializationSelect()
        {
            var s = await GetAllSpecialization();
            return [.. s.Select(s => new SelectItem { Text = s.Title, Value = s.Id })];
        }

        public async Task<SpecializationDTO?> GetSpecialization(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/specializations/{id}?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<SpecializationDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<SpecializationDTO?> AddSpecialization(SpecializationDTO item, string? include = null)
        {
            try
            {
                using var response = await _httpClient.PostAsJsonAsync($"api/specializations?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<SpecializationDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<SpecializationDTO?> UpdateSpecialization(SpecializationDTO item, string? include = null)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/specializations?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<SpecializationDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteSpecialization(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/specializations/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return false;
            }
        }
    }
}
