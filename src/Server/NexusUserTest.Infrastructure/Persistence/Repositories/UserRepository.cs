using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Infrastructure
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbDataContext db)
            : base(db) { }

        public async Task<IEnumerable<User>> GetAllUserAsync(Expression<Func<User, bool>>? expression = null, string? includeProperties = null)
            => await GetAllAsync(expression, includeProperties);

        public async Task<User> GetUserAsync(Expression<Func<User, bool>> expression, string? includeProperties = null)
            => await GetAsync(expression, includeProperties);

        public async Task AddUserAsync(User entity) => await AddAsync(entity);
        public void UpdateUser(User entity) => Update(entity);
        public void DeleteUser(User entity) => Delete(entity);
    }
}
