using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Infrastructure
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(DbDataContext db)
            : base(db) { }

        public async Task<IEnumerable<Group>> GetAllGroupAsync(Expression<Func<Group, bool>>? expression = null, string? includeProperties = null)
            => await GetAllAsync(expression, includeProperties);

        public async Task<Group> GetGroupAsync(Expression<Func<Group, bool>> expression, string? includeProperties = null)
            => await GetAsync(expression, includeProperties);

        public async Task AddGroupAsync(Group entity) => await AddAsync(entity);
        public void UpdateGroup(Group entity) => Update(entity);
        public void DeleteGroup(Group entity) => Delete(entity);
    }
}
