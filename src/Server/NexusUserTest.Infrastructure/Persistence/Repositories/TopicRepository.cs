using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Infrastructure.Persistence
{
    internal class TopicRepository : RepositoryBase<Topic>, ITopicRepository
    {
        public TopicRepository(DbDataContext db)
            : base(db) { }

        public async Task<IEnumerable<Topic>> GetAllTopicAsync(Expression<Func<Topic, bool>>? expression = null, string? includeProperties = null)
            => await GetAllAsync(expression, includeProperties);

        public async Task<Topic> GetTopicAsync(Expression<Func<Topic, bool>> expression, string? includeProperties = null)
            => await GetAsync(expression, includeProperties);

        public async Task AddTopicAsync(Topic entity) => await AddAsync(entity);
        public void UpdateTopic(Topic entity) => Update(entity);
        public void DeleteTopic(Topic entity) => Delete(entity);
    }
}
