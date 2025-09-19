using NexusUserTest.Domain.Entities;
using System.Linq.Expressions;

namespace NexusUserTest.Domain.Repositories
{
    public interface IGroupUserRepository : IRepositoryBase<GroupUser>
    {
        Task<IEnumerable<GroupUser>> GetAllGroupUserAsync(Expression<Func<GroupUser, bool>>? expression = null, string? includeProperties = null);
        Task<GroupUser> GetGroupUserAsync(Expression<Func<GroupUser, bool>> expression, string? includeProperties = null);
        void UpdateGroupUser(GroupUser entity);
    }
}
