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
        Task<Topic> UpdateTopicAsync(Topic entity, string? includeProperties = null);
        Task DeleteTopicAsync(Topic entity);
    }

    public class TopicRepoService(IRepositoryManager repository) : ITopicRepoService
    {
        private readonly IRepositoryManager _repository = repository;

        public async Task<IEnumerable<Topic>> GetAllTopicAsync(Expression<Func<Topic, bool>>? expression = null, string? includeProperties = null)
            => await _repository.Topic.GetAllTopicAsync(expression, includeProperties);

        public async Task<Topic> GetTopicAsync(Expression<Func<Topic, bool>> expression, string? includeProperties = null)
            => await _repository.Topic.GetTopicAsync(expression, includeProperties);

        public async Task<Topic> AddTopicAsync(Topic entity, string? includeProperties = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;
            await _repository.Topic.AddTopicAsync(entity);
            await _repository.SaveAsync();
            return await _repository.Topic.GetTopicAsync(t => t.Id == entity.Id, includeProperties);
        }

        public async Task<Topic> UpdateTopicAsync(Topic entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.Topic.UpdateTopic(entity);
            await _repository.SaveAsync();
            return await _repository.Topic.GetTopicAsync(t => t.Id == entity.Id, includeProperties);
        }

        public async Task DeleteTopicAsync(Topic entity)
        {
            _repository.Topic.DeleteTopic(entity);
            await _repository.SaveAsync();
        }
    }
}
