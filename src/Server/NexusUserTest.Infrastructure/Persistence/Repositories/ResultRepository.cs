using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Infrastructure
{
    public class ResultRepository(DbDataContext db) : RepositoryBase<Result>(db), IResultRepository
    {
        public async Task<IEnumerable<Result>> GetAllResultAsync(Expression<Func<Result, bool>>? expression = null, string? includeProperties = null)
            => await GetAllAsync(expression, includeProperties);

        public async Task<Result> GetResultAsync(Expression<Func<Result, bool>> expression, string? includeProperties = null)
            => await GetAsync(expression, includeProperties);
        public async Task AddResultAsync(Result entity) => await AddAsync(entity);
        public async Task AddRangeResultAsync(List<Result> entities) => await AddRangeAsync(entities);
        public void UpdateResult(Result entity) => Update(entity);
        public void DeleteResult(IEnumerable<Result> entity) => DeleteRange(entity);
    }
}
