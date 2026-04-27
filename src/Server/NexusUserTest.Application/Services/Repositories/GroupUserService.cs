using NexusUserTest.Application.Common;
using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;
using System.Linq.Expressions;

namespace NexusUserTest.Application.Services
{
    /// <summary>
    /// Сервис с операциями для обьекта ответ
    /// </summary>
    public class GroupUserService : IGroupUserService
    {
        private readonly IRepository _repository;

        public GroupUserService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получение всех групп пользователей из набора данных
        /// </summary>
        /// <returns>Возвращает список групп пользователей из набора данных</returns>
        public async Task<IEnumerable<GroupUser>> GetAllGroupUserAsync()
            => await _repository.GroupUser.GetAllGroupUserAsync();

        /// <summary>
        /// Получение группы пользователя из набора данных по Id
        /// </summary>
        /// <param name="id">Id группы пользователя</param>
        /// <returns>Возвращает группу пользователя из набора данных</returns>
        public async Task<GroupUser?> GetGroupUserByIdAsync(int id)
            => await _repository.GroupUser.GetGroupUserByIdAsync(id);

        /// <summary>
        /// Изменить группы пользователя в наборе данных
        /// </summary>
        /// <param name="entity">Группа пользователя</param>
        public async Task<GroupUser> UpdateGroupUserAsync(GroupUser entity)
        {
            await _repository.GroupUser.Update(entity);
            return await _repository.GroupUser.GetGroupUserByIdAsync(entity.Id);
        }
    }
}
