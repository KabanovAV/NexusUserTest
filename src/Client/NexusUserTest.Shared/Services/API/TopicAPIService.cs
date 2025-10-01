using NexusUserTest.Common;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface ITopicAPIService
    {
        Task<List<TopicDTO>> GetAllTopic(string? include = null);
        Task<List<SelectItem>> GetTopicSelect();
        Task<TopicDTO?> GetTopic(int id, string? include = null);
        Task<TopicDTO?> AddTopic(TopicDTO item, string? include = null);
        Task<TopicDTO?> UpdateTopic(TopicDTO item, string? include = null);
        Task DeleteTopic(int id);
    }

    public class TopicAPIService(IHttpClientFactory httpClienFactory) : ITopicAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");

        public async Task<List<TopicDTO>> GetAllTopic(string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/topics?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<TopicDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return [];
            }
        }

        public async Task<List<SelectItem>> GetTopicSelect()
        {
            var s = await GetAllTopic();
            return [.. s.Select(s => new SelectItem { Text = s.Title, Value = s.Id })];
        }

        public async Task<TopicDTO?> GetTopic(int id, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/topics/{id}?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TopicDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<TopicDTO?> AddTopic(TopicDTO item, string? include = null)
        {
            try
            {
                using var response = await _httpClient.PostAsJsonAsync($"api/topics?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TopicDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task<TopicDTO?> UpdateTopic(TopicDTO item, string? include = null)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/topics?include={include}", item);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TopicDTO>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return null;
            }
        }

        public async Task DeleteTopic(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/topics/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
            }
        }
    }
}
