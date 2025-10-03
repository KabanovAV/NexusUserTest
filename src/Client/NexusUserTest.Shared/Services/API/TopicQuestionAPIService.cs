using Microsoft.AspNetCore.WebUtilities;
using NexusUserTest.Common;

namespace NexusUserTest.Shared.Services
{
    public interface ITopicQuestionAPIService
    {
        Task<ApiResponse<List<QuestionTestDTO>>> GetAllQuestionsBySpecializationId(int specializationId, string? include = null);
    }

    public class TopicQuestionAPIService(IHttpClientFactory httpClienFactory, IApiResponseHandler responseHandler) : ITopicQuestionAPIService
    {
        private readonly HttpClient _httpClient = httpClienFactory.CreateClient("HttpClient");
        private readonly IApiResponseHandler _responseHandler = responseHandler;

        public async Task<ApiResponse<List<QuestionTestDTO>>> GetAllQuestionsBySpecializationId(int specializationId, string? include = null)
        {
            if (include != null)
            {
                var url = QueryHelpers.AddQueryString($"api/topicquestions/specialization/{specializationId}", "include", include);
                return await _responseHandler.ExecuteHttpAsync<List<QuestionTestDTO>>(() =>
                    _httpClient.GetAsync(url), "GetAllQuestionsBySpecializationId");
            }
            return await _responseHandler.ExecuteHttpAsync<List<QuestionTestDTO>>(() =>
                    _httpClient.GetAsync($"api/topicquestions/specialization/{specializationId}"), "GetAllQuestionsBySpecializationId");
        }
    }
}
