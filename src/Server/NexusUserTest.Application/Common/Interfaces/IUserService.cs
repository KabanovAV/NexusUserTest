using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Common
{
    /// <summary>
    /// Интерфейс сервиса операциями для обьекта пользователь
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получение всех пользователей из набора данных
        /// </summary>
        /// <returns>Возвращает список пользователей из набора данных</returns>
        Task<IEnumerable<User>> GetAllUserAsync();

        /// <summary>
        /// Получение пользователя из набора данных по Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Возвращает пользователя из набора данных</returns>
        Task<User?> GetUserByIdAsync(int id);

        /// <summary>
        /// Добавить пользователя в набор данных
        /// </summary>
        /// <param name="entity">Пользователь</param>
        /// <returns>Возвращает пользователя после добавления в БД</returns>
        Task<User> AddUserAsync(User entity);

        /// <summary>
        /// Изменить пользователя в наборе данных
        /// </summary>
        /// <param name="entity">Пользователь</param>
        Task<User> UpdateUserAsync(User entity);

        /// <summary>
        /// Удалить пользователя из набора данных
        /// </summary>
        /// <param name="id">Id пользователя</param>
        Task DeleteUserAsync(int id);
    }
}
