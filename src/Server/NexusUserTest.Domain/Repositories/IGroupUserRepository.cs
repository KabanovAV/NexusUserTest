using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Domain.Repositories
{
    public interface IGroupUserRepository : IRepositoryBase<GroupUser>
    {        
        Task AddGroupUserAsync(GroupUser entity);
        void UpdateGroupUser(GroupUser entity);
        void DeleteGroupUser(GroupUser entity);
    }
}
