using Microsoft.EntityFrameworkCore;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Infrastructure.Persistence
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbDataContext _db;
        internal DbSet<T> dbSet;

        public RepositoryBase(DbDataContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, string? includeProperties = null)
        {
            IQueryable<T> query = BuildQuery(expression, includeProperties);
            return await query.ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression, string? includeProperties = null)
        {
            IQueryable<T> query = BuildQuery(expression, includeProperties);
            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task AddAsync(T entity) => await dbSet.AddAsync(entity);
        public async Task AddRangeAsync(List<T> entities) => await dbSet.AddRangeAsync(entities);
        public void Update(T entity) => dbSet.Update(entity);
        public void Delete(T entity) => dbSet.Remove(entity);

        private IQueryable<T> BuildQuery(Expression<Func<T, bool>>? expression = null, string? includeProperties = null)
        {
            IQueryable<T> query = expression == null ? dbSet : dbSet.Where(expression);
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var property in includeProperties
                    .Split([','], StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query;
        }
    }
}
