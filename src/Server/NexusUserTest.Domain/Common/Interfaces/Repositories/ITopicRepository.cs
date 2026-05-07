using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Domain.Common.Interfaces
{
    /// <summary>
    /// Интерфейс с операциями для репозитория темы
    /// </summary>
    public interface ITopicRepository : IRepositoryOperations<Topic>
    {
        /// <summary>
        /// Получение всех тем из набора данных
        /// </summary>
        /// <returns>Возвращает список всех тем из набора данных</returns>
        Task<IEnumerable<Topic>> GetAllTopicAsync();

        /// <summary>
        /// Получение одной темы из набора данных
        /// </summary>
        /// <param name="id">Id темы</param>
        /// <returns>Возвращает одной темы из набора данных</returns>
        Task<Topic?> GetTopicByIdAsync(int id);
    }
}
