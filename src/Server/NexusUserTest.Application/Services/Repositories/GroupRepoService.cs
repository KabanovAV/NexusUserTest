using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Application.Services
{
    public interface IGroupRepoService
    {
        Task<IEnumerable<Group>> GetAllGroupAsync(Expression<Func<Group, bool>>? expression = null, string? includeProperties = null);
        Task<Group> GetGroupAsync(Expression<Func<Group, bool>> expression, string? includeProperties = null);
        Task<Group> AddGroupAsync(Group entity, string? includeProperties = null);
        Task<Group> UpdateGroupAsync(Group entity, string? includeProperties = null);
        Task DeleteGroupAsync(Group entity);
    }

    public class GroupRepoService(IRepositoryManager repository) : IGroupRepoService
    {
        private readonly IRepositoryManager _repository = repository;

        public async Task<IEnumerable<Group>> GetAllGroupAsync(Expression<Func<Group, bool>>? expression = null, string? includeProperties = null)
            => await _repository.Group.GetAllGroupAsync(expression, includeProperties);

        public async Task<Group> GetGroupAsync(Expression<Func<Group, bool>> expression, string? includeProperties = null)
            => await _repository.Group.GetGroupAsync(expression, includeProperties);

        public async Task<Group> AddGroupAsync(Group entity, string? includeProperties = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;
            await _repository.Group.AddGroupAsync(entity);
            await _repository.SaveAsync();
            return await _repository.Group.GetGroupAsync(g => g.Id == entity.Id, includeProperties);
        }

        public async Task<Group> UpdateGroupAsync(Group entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.Group.UpdateGroup(entity);
            await _repository.SaveAsync();
            return await _repository.Group.GetGroupAsync(g => g.Id == entity.Id, includeProperties);
        }

        public async Task DeleteGroupAsync(Group entity)
        {
            _repository.Group.DeleteGroup(entity);
            await _repository.SaveAsync();
        }
    }
}
