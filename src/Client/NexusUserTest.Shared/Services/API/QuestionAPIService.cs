using NexusUserTest.Common.DTOs;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface IQuestionAPIService
    {
        Task<List<QuestionDTO>> GetAllQuestion(string? include = null);
        Task<QuestionDTO?> GetQuestion(int id, string? include = null);
        Task<QuestionDTO?> AddQuestion(QuestionDTO item, string? include = null);
        Task<QuestionDTO?> UpdateQuestion(QuestionDTO item, string? include = null);
        Task DeleteQuestion(int id);
    }

    public class QuestionAPIService(IHttpClientFactory httpClienFactory) : IQuestionAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");

        public async Task<List<QuestionDTO>> GetAllQuestion(string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/questions?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<QuestionDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return [];
            }
        }

        public async Task<QuestionDTO?> GetQuestion(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/questions/{id}?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<QuestionDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<QuestionDTO?> AddQuestion(QuestionDTO item, string? include = null)
        {
            try
            {
                using var response = await _httpClient.PostAsJsonAsync($"api/questions?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<QuestionDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<QuestionDTO?> UpdateQuestion(QuestionDTO item, string? include = null)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/questions?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<QuestionDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task DeleteQuestion(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/questions/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
            }
        }
    }
}
