using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Common
{
    /// <summary>
    /// Интерфейс сервиса операциями для обьекта группы
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// Получение всех групп из набора данных
        /// </summary>
        /// <returns>Возвращает список групп из набора данных</returns>
        Task<IEnumerable<Group>> GetAllGroupAsync();

        /// <summary>
        /// Получение группы из набора данных по Id
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <returns>Возвращает группу из набора данных</returns>
        Task<Group?> GetGroupByIdAsync(int id);

        /// <summary>
        /// Добавить группу в набор данных
        /// </summary>
        /// <param name="entity">Группа</param>
        /// <returns>Возвращает группу после добавления в БД</returns>
        Task<Group> AddGroupAsync(Group entity);

        /// <summary>
        /// Изменить группу в наборе данных
        /// </summary>
        /// <param name="entity">Группа</param>
        Task<Group> UpdateGroupAsync(Group entity);

        /// <summary>
        /// Удалить группу из набора данных
        /// </summary>
        /// <param name="id">Id группы</param>
        Task DeleteGroupAsync(int id);
    }
}
