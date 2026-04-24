using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Domain.Common
{
    /// <summary>
    /// Интерфейс с операциями для репозитория пользователь
    /// </summary>
    public interface IUserRepository : IRepositoryOperations<User>
    {
        /// <summary>
        /// Получение всех пользователей из набора данных
        /// </summary>
        /// <returns>Возвращает список всех пользователей из набора данных</returns>
        Task<IEnumerable<User>> GetAllUserAsync();

        /// <summary>
        /// Получение одного пользователя из набора данных
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Возвращает одного пользователя из набора данных</returns>
        Task<User?> GetUserByIdAsync(int id);
    }
}
