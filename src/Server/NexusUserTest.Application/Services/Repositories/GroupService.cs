using NexusUserTest.Application.Common;
using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Services
{
    /// <summary>
    /// Сервис с операциями для обьекта ответ
    /// </summary>
    public class GroupService : IGroupService
    {
        private readonly IRepository _repository;

        public GroupService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получение всех групп из набора данных
        /// </summary>
        /// <returns>Возвращает список групп из набора данных</returns>
        public async Task<IEnumerable<Group>> GetAllGroupAsync()
            => await _repository.Group.GetAllGroupAsync();

        /// <summary>
        /// Получение группы из набора данных по Id
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <returns>Возвращает группу из набора данных</returns>
        public async Task<Group?> GetGroupByIdAsync(int id)
            => await _repository.Group.GetGroupByIdAsync(id);

        /// <summary>
        /// Добавить группу в набор данных
        /// </summary>
        /// <param name="entity">Группа</param>
        /// <returns>Возвращает группу после добавления в БД</returns>
        public async Task<Group> AddGroupAsync(Group entity)
        {
            await _repository.Group.AddAsync(entity);
            return await _repository.Group.GetGroupByIdAsync(entity.Id);
        }

        /// <summary>
        /// Изменить группу в наборе данных
        /// </summary>
        /// <param name="entity">Группа</param>
        public async Task<Group> UpdateGroupAsync(Group entity)
        {
            await _repository.Group.Update(entity);
            return await _repository.Group.GetGroupByIdAsync(entity.Id);
        }

        /// <summary>
        /// Удалить группу из набора данных
        /// </summary>
        /// <param name="id">Id группы</param>
        public async Task DeleteGroupAsync(int id)
            => await _repository.Group.Delete(id);
    }
}
