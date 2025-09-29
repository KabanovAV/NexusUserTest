using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;

namespace NexusUserTest.Application.Services
{
    public interface ITopicQuestionRepoService
    {
        Task AddTopicQuestionAsync(TopicQuestion entity);
        Task UpdateTopicQuestionAsync(TopicQuestion entity);
        Task DeleteTopicQuestionAsync(TopicQuestion entity);
    }

    public class TopicQuestionRepoService(IRepositoryManager repository) : ITopicQuestionRepoService
    {
        private readonly IRepositoryManager _repository = repository;

        public async Task AddTopicQuestionAsync(TopicQuestion entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;
            await _repository.TopicQuestion.AddTopicQuestionAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task UpdateTopicQuestionAsync(TopicQuestion entity)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.TopicQuestion.UpdateTopicQuestion(entity);
            await _repository.SaveAsync();
        }

        public async Task DeleteTopicQuestionAsync(TopicQuestion entity)
        {
            _repository.TopicQuestion.DeleteTopicQuestion(entity);
            await _repository.SaveAsync();
        }
    }
}
