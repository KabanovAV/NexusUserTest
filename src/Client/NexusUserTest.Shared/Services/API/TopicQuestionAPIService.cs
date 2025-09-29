using NexusUserTest.Common.DTOs;
using System.Net.Http.Json;

namespace NexusUserTest.Shared.Services
{
    public interface ITopicQuestionAPIService
    {
        Task<List<QuestionTestDTO>> GetAllQuestionsBySpecializationId(int specializationId, string? include = null);
    }

    public class TopicQuestionAPIService(IHttpClientFactory httpClienFactory) : ITopicQuestionAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");

        public async Task<List<QuestionTestDTO>> GetAllQuestionsBySpecializationId(int specializationId, string? include = null)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/topicquestions/specialization/{specializationId}?include={include}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<QuestionTestDTO>>() ?? [];
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return [];
            }
        }
    }
}
