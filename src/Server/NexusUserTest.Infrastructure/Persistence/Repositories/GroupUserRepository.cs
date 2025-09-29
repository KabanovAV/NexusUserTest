using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Infrastructure
{
    public class GroupUserRepository(DbDataContext db) : RepositoryBase<GroupUser>(db), IGroupUserRepository
    {
        public async Task<IEnumerable<GroupUser>> GetAllGroupUserAsync(Expression<Func<GroupUser, bool>>? expression = null, string? includeProperties = null)
            => await GetAllAsync(expression, includeProperties);

        public async Task<GroupUser> GetGroupUserAsync(Expression<Func<GroupUser, bool>> expression, string? includeProperties = null)
            => await GetAsync(expression, includeProperties);

        public void UpdateGroupUser(GroupUser entity) => Update(entity);
    }
}
