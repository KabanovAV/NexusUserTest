using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Application.Services
{
    public interface IGroupUserRepoService
    {
        Task<IEnumerable<GroupUser>> GetAllGroupUserAsync(Expression<Func<GroupUser, bool>>? expression = null, string? includeProperties = null);
        Task<GroupUser> GetGroupUserAsync(Expression<Func<GroupUser, bool>> expression, string? includeProperties = null);
        Task<GroupUser> UpdateGroupUser(GroupUser entity, string? includeProperties = null);
    }

    public class GroupUserRepoService : IGroupUserRepoService
    {
        private readonly IRepositoryManager _repository;

        public GroupUserRepoService(IRepositoryManager repository)
            => _repository = repository;

        public async Task<IEnumerable<GroupUser>> GetAllGroupUserAsync(Expression<Func<GroupUser, bool>>? expression = null, string? includeProperties = null)
            => await _repository.GroupUser.GetAllGroupUserAsync(expression, includeProperties);

        public async Task<GroupUser> GetGroupUserAsync(Expression<Func<GroupUser, bool>> expression, string? includeProperties = null)
            => await _repository.GroupUser.GetGroupUserAsync(expression, includeProperties);

        public async Task<GroupUser> UpdateGroupUser(GroupUser entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.GroupUser.UpdateGroupUser(entity);
            _repository.Save();
            return await _repository.GroupUser.GetGroupUserAsync(gu => gu.Id == entity.Id, includeProperties);
        }
    }
}
