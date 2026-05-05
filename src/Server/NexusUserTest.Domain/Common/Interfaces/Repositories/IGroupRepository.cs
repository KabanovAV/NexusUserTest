using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Domain.Common
{
    /// <summary>
    /// Интерфейс с операциями для репозитория групп
    /// </summary>
    public interface IGroupRepository : IRepositoryOperations<Group>
    {
        /// <summary>
        /// Получение всех групп из набора данных
        /// </summary>
        /// <returns>Возвращает список всех групп из набора данных</returns>
        Task<IEnumerable<Group>> GetAllGroupAsync();

        /// <summary>
        /// Получение одной группы из набора данных
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <returns>Возвращает одной группы из набора данных</returns>
        Task<Group?> GetGroupByIdAsync(int id);

        /// <summary>
        /// Получение одной группы из набора данных со связями
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <returns>Возвращает группу из набора данных</returns>
        Task<Group?> GetGroupByIdWithChildrenAsync(int id);
    }
}
