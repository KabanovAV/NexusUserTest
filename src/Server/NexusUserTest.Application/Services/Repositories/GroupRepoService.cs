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
        Task<Group> UpdateGroup(Group entity, string? includeProperties = null);
        void DeleteGroup(Group entity);
    }

    public class GroupRepoService : IGroupRepoService
    {
        private readonly IRepositoryManager _repository;

        public GroupRepoService(IRepositoryManager repository)
            => _repository = repository;

        public async Task<IEnumerable<Group>> GetAllGroupAsync(Expression<Func<Group, bool>>? expression = null, string? includeProperties = null)
            => await _repository.Group.GetAllGroupAsync(expression, includeProperties);

        public async Task<Group> GetGroupAsync(Expression<Func<Group, bool>> expression, string? includeProperties = null)
            => await _repository.Group.GetGroupAsync(expression, includeProperties);

        public async Task<Group> AddGroupAsync(Group entity, string? includeProperties = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;
            await _repository.Group.AddGroupAsync(entity);
            _repository.Save();
            return await _repository.Group.GetGroupAsync(g =>  g.Id == entity.Id, includeProperties);
        }

        public async Task<Group> UpdateGroup(Group entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.Group.UpdateGroup(entity);
            _repository.Save();
            return await _repository.Group.GetGroupAsync(g => g.Id == entity.Id, includeProperties);
        }

        public void DeleteGroup(Group entity)
        {
            _repository.Group.DeleteGroup(entity);
            _repository.Save();
        }
    }
}
