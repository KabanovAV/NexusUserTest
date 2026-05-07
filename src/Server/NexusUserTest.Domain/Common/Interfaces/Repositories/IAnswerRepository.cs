using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Domain.Common.Interfaces
{
    /// <summary>
    /// Интерфейс с операциями для репозитория ответы
    /// </summary>
    public interface IAnswerRepository : IRepositoryOperations<Answer>
    {
        /// <summary>
        /// Получение всех ответов из набора данных
        /// </summary>
        /// <returns>Возвращает список всех ответов из набора данных</returns>
        Task<IEnumerable<Answer>> GetAllAnswerAsync();

        /// <summary>
        /// Получение одного ответы из набора данных
        /// </summary>
        /// <param name="id">Id ответа</param>
        /// <returns>Возвращает один ответ из набора данных</returns>
        Task<Answer?> GetAnswerByIdAsync(int id);
    }
}
