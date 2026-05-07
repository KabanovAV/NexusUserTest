using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Domain.Common.Interfaces
{
    /// <summary>
    /// Интерфейс с операциями для репозитория группа пользователь
    /// </summary>
    public interface IGroupUserRepository : IRepositoryOperations<GroupUser>
    {
        /// <summary>
        /// Получение всех группа пользователей из набора данных
        /// </summary>
        /// <returns>Возвращает список всех группа пользователей из набора данных</returns>
        Task<IEnumerable<GroupUser>> GetAllGroupUserAsync();

        /// <summary>
        /// Получение одной группа пользователя из набора данных
        /// </summary>
        /// <param name="id">Id группы пользователя</param>
        /// <returns>Возвращает одну группу пользователь из набора данных</returns>
        Task<GroupUser?> GetGroupUserByIdAsync(int id);
    }
}
