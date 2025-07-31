using NexusUserTest.Domain.Entities;
using System.Linq.Expressions;

namespace NexusUserTest.Domain.Repositories
{
    public interface ITopicRepository : IRepositoryBase<Topic>
    {
        Task<IEnumerable<Topic>> GetAllTopicAsync(Expression<Func<Topic, bool>>? expression = null, string? includeProperties = null);
        Task<Topic> GetTopicAsync(Expression<Func<Topic, bool>> expression, string? includeProperties = null);
        Task AddTopicAsync(Topic entity);
        void UpdateTopic(Topic entity);
        void DeleteTopic(Topic entity);
    }
}
