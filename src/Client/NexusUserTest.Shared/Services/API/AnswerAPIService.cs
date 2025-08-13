using NexusUserTest.Common.DTOs;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IAnswerAPIService
    {
        Task<IEnumerable<AnswerDTO>> GetAllAnswer(string? include = null);
        Task<AnswerDTO?> GetAnswer(int id, string? include = null);
        Task<AnswerDTO?> AddAnswer(AnswerDTO item, string? include = null);
        Task<IEnumerable<AnswerDTO>?> AddRangeAnswer(List<AnswerDTO> items, string? include = null);
        Task<AnswerDTO?> UpdateAnswer(AnswerDTO item, string? include = null);
        Task DeleteAnswer(int id);
    }

    public class AnswerAPIService : IAnswerAPIService
    {
        private readonly HttpClient _httpClient;

        public AnswerAPIService(IHttpClientFactory httpClienFactory)
        {
            _httpClient = httpClienFactory.CreateClient("HttpClient");
        }

        public async Task<IEnumerable<AnswerDTO>> GetAllAnswer(string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/answers?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<AnswerDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return new List<AnswerDTO>();
            }
        }

        public async Task<AnswerDTO?> GetAnswer(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/answers/{id}?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AnswerDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<AnswerDTO?> AddAnswer(AnswerDTO item, string? include = null)
        {
            try
            {
                using var response = await _httpClient.PostAsJsonAsync($"api/answers?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AnswerDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<AnswerDTO>?> AddRangeAnswer(List<AnswerDTO> items, string? include = null)
        {
            try
            {
                using var response = await _httpClient.PostAsJsonAsync($"api/answers/batch?include={include}", items);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<AnswerDTO>>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<AnswerDTO?> UpdateAnswer(AnswerDTO item, string? include = null)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/answers?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<AnswerDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task DeleteAnswer(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync("api/answers");
                response.EnsureSuccessStatusCode();
                await _httpClient.DeleteAsync($"api/answers/{id}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
            }
        }
    }
}
