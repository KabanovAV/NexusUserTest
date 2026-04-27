using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Common
{
    /// <summary>
    /// Интерфейс сервиса операциями для обьекта группа пользователя
    /// </summary>
    public interface IGroupUserService
    {
        /// <summary>
        /// Получение всех групп пользователей из набора данных
        /// </summary>
        /// <returns>Возвращает список групп пользователей из набора данных</returns>
        Task<IEnumerable<GroupUser>> GetAllGroupUserAsync();

        /// <summary>
        /// Получение группы пользователя из набора данных по Id
        /// </summary>
        /// <param name="id">Id группы пользователя</param>
        /// <returns>Возвращает группу пользователя из набора данных</returns>
        Task<GroupUser?> GetGroupUserByIdAsync(int id);

        /// <summary>
        /// Изменить группы пользователя в наборе данных
        /// </summary>
        /// <param name="entity">Группа пользователя</param>
        Task<GroupUser> UpdateGroupUserAsync(GroupUser entity);
    }
}
