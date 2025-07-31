using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace NexusUserTest.Domain.Repositories
{
    public interface IGroupRepository : IRepositoryBase<Group>
    {
        Task<IEnumerable<Group>> GetAllGroupAsync(Expression<Func<Group, bool>>? expression = null, string? includeProperties = null);
        Task<Group> GetGroupAsync(Expression<Func<Group, bool>> expression, string? includeProperties = null);
        Task AddGroupAsync(Group entity);
        void UpdateGroup(Group entity);
        void DeleteGroup(Group entity);
    }
}
