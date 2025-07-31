using NexusUserTest.Domain.Entities;
using System.Linq.Expressions;

namespace NexusUserTest.Domain.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<IEnumerable<User>> GetAllUserAsync(Expression<Func<User, bool>>? expression = null, string? includeProperties = null);
        Task<User> GetUserAsync(Expression<Func<User, bool>> expression, string? includeProperties = null);
        Task AddUserAsync(User entity);
        void UpdateUser(User entity);
        void DeleteUser(User entity);
    }
}
