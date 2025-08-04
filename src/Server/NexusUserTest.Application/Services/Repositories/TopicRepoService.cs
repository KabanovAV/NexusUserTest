using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Application.Services
{
    public interface ITopicRepoService
    {
        Task<IEnumerable<Topic>> GetAllTopicAsync(Expression<Func<Topic, bool>>? expression = null, string? includeProperties = null);
        Task<Topic> GetTopicAsync(Expression<Func<Topic, bool>> expression, string? includeProperties = null);
        Task<Topic> AddTopicAsync(Topic entity, string? includeProperties = null);
        Task<Topic> UpdateTopic(Topic entity, string? includeProperties = null);
        void DeleteTopic(Topic entity);
    }

    public class TopicRepoService : ITopicRepoService
    {
        private readonly IRepositoryManager _repository;

        public TopicRepoService(IRepositoryManager repository)
            => _repository = repository;

        public async Task<IEnumerable<Topic>> GetAllTopicAsync(Expression<Func<Topic, bool>>? expression = null, string? includeProperties = null)
            => await _repository.Topic.GetAllTopicAsync(expression, includeProperties);

        public async Task<Topic> GetTopicAsync(Expression<Func<Topic, bool>> expression, string? includeProperties = null)
            => await _repository.Topic.GetTopicAsync(expression, includeProperties);

        public async Task<Topic> AddTopicAsync(Topic entity, string? includeProperties = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;
            await _repository.Topic.AddTopicAsync(entity);
            _repository.Save();
            return await _repository.Topic.GetTopicAsync(t => t.Id == entity.Id, includeProperties);
        }

        public async Task<Topic> UpdateTopic(Topic entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.Topic.UpdateTopic(entity);
            _repository.Save();
            return await _repository.Topic.GetTopicAsync(t => t.Id == entity.Id, includeProperties);
        }

        public void DeleteTopic(Topic entity)
        {
            _repository.Topic.DeleteTopic(entity);
            _repository.Save();
        }
    }
}
