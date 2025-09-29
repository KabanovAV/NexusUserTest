using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Application.Services
{
    public interface IGroupUserRepoService
    {
        Task<IEnumerable<GroupUser>> GetAllGroupUserAsync(Expression<Func<GroupUser, bool>>? expression = null, string? includeProperties = null);
        Task<GroupUser> GetGroupUserAsync(Expression<Func<GroupUser, bool>> expression, string? includeProperties = null);
        Task<GroupUser> UpdateGroupUserAsync(GroupUser entity, string? includeProperties = null);
    }

    public class GroupUserRepoService(IRepositoryManager repository) : IGroupUserRepoService
    {
        private readonly IRepositoryManager _repository = repository;

        public async Task<IEnumerable<GroupUser>> GetAllGroupUserAsync(Expression<Func<GroupUser, bool>>? expression = null, string? includeProperties = null)
            => await _repository.GroupUser.GetAllGroupUserAsync(expression, includeProperties);

        public async Task<GroupUser> GetGroupUserAsync(Expression<Func<GroupUser, bool>> expression, string? includeProperties = null)
            => await _repository.GroupUser.GetGroupUserAsync(expression, includeProperties);

        public async Task<GroupUser> UpdateGroupUserAsync(GroupUser entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.GroupUser.UpdateGroupUser(entity);
            await _repository.SaveAsync();
            return await _repository.GroupUser.GetGroupUserAsync(gu => gu.Id == entity.Id, includeProperties);
        }
    }
}
