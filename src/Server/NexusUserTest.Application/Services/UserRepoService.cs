using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Application.Services
{
    public interface IUserRepoService
    {
        Task<IEnumerable<User>> GetAllUserAsync(Expression<Func<User, bool>>? expression = null, string? includeProperties = null);
        Task<User> GetUserAsync(Expression<Func<User, bool>> expression, string? includeProperties = null);
        Task<User> AddUserAsync(User entity, string? includeProperties = null);
        Task<User> UpdateUser(User entity, string? includeProperties = null);
        void DeleteUser(User entity);
    }

    public class UserRepoService : IUserRepoService
    {
        private readonly IRepositoryManager _repository;

        public UserRepoService(IRepositoryManager repository)
            => _repository = repository;

        public async Task<IEnumerable<User>> GetAllUserAsync(Expression<Func<User, bool>>? expression = null, string? includeProperties = null)
            => await _repository.User.GetAllUserAsync(expression, includeProperties);

        public async Task<User> GetUserAsync(Expression<Func<User, bool>> expression, string? includeProperties = null)
            => await _repository.User.GetUserAsync(expression, includeProperties);

        public async Task<User> AddUserAsync(User entity, string? includeProperties = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;

            if (entity.GroupUser != null && entity.GroupUser.Count != 0)
            {
                foreach (var group in entity.GroupUser)
                {
                    group.CreatedDate = DateTime.Now;
                    group.ChangedDate = DateTime.Now;
                }
            }

            await _repository.User.AddUserAsync(entity);
            _repository.Save();
            return await _repository.User.GetUserAsync(g => g.Id == entity.Id, includeProperties);
        }

        public async Task<User> UpdateUser(User entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;

            if (entity.GroupUser != null && entity.GroupUser.Count != 0)
            {
                foreach (var group in entity.GroupUser)
                {
                    if (group.Id == 0)
                    {
                        group.CreatedDate = DateTime.Now;
                        group.ChangedDate = DateTime.Now;
                    }
                    else
                        group.ChangedDate = DateTime.Now;
                }
            }

            _repository.User.UpdateUser(entity);
            _repository.Save();
            return await _repository.User.GetUserAsync(g => g.Id == entity.Id, includeProperties);
        }

        public void DeleteUser(User entity)
        {
            _repository.User.DeleteUser(entity);
            _repository.Save();
        }
    }
}
