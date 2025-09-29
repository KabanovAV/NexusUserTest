using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Infrastructure
{
    public class AnswerRepository(DbDataContext db) : RepositoryBase<Answer>(db), IAnswerRepository
    {
        public async Task<IEnumerable<Answer>> GetAllAnswerAsync(Expression<Func<Answer, bool>>? expression = null, string? includeProperties = null)
            => await GetAllAsync(expression, includeProperties);

        public async Task<Answer> GetAnswerAsync(Expression<Func<Answer, bool>> expression, string? includeProperties = null)
            => await GetAsync(expression, includeProperties);

        public async Task AddAnswerAsync(Answer entity) => await AddAsync(entity);
        public async Task AddRangeAnswerAsync(List<Answer> entities) => await AddRangeAsync(entities);
        public void UpdateAnswer(Answer entity) => Update(entity);
        public void DeleteAnswer(Answer entity) => Delete(entity);
    }
}
