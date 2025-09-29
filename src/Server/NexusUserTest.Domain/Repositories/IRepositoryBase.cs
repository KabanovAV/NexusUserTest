using System.Linq.Expressions;

namespace NexusUserTest.Domain.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, string? includeProperties = null);
        Task<T?> GetAsync(Expression<Func<T, bool>> expression, string? includeProperties = null);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entity);
    }
}
