using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;

namespace NexusUserTest.Application.Services
{
    public interface ITopicQuestionRepoService
    {
        Task AddTopicQuestionAsync(TopicQuestion entity);
        void UpdateTopicQuestion(TopicQuestion entity);
        void DeleteTopicQuestion(TopicQuestion entity);
    }

    public class TopicQuestionRepoService : ITopicQuestionRepoService
    {
        private readonly IRepositoryManager _repository;

        public TopicQuestionRepoService(IRepositoryManager repository)
            => _repository = repository;

        public async Task AddTopicQuestionAsync(TopicQuestion entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;
            await _repository.TopicQuestion.AddTopicQuestionAsync(entity);
            _repository.Save();
        }

        public void UpdateTopicQuestion(TopicQuestion entity)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.TopicQuestion.UpdateTopicQuestion(entity);
            _repository.Save();
        }

        public void DeleteTopicQuestion(TopicQuestion entity)
        {
            _repository.TopicQuestion.DeleteTopicQuestion(entity);
            _repository.Save();
        }
    }
}
