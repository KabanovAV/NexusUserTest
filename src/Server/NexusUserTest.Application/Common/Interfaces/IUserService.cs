using NexusUserTest.Common.DTOs;
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
        Task<Result<IEnumerable<UserDTO>>> GetAllUserAsync();

        /// <summary>
        /// Получение пользователя из набора данных по Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Возвращает пользователя из набора данных</returns>
        Task<Result<UserDTO>> GetUserByIdAsync(int id);

        /// <summary>
        /// Добавить пользователя в набор данных
        /// </summary>
        /// <param name="createUser">Пользователь</param>
        /// <returns>Возвращает пользователя после добавления в БД</returns>
        Task<Result<UserDTO>> CreateUserAsync(CreateUserDTO createDto);

        /// <summary>
        /// Изменить пользователя в наборе данных
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="updateDto">Пользователь</param>
        Task<Result> UpdateUserAsync(int id, UpdateUserDTO updateDto);

        /// <summary>
        /// Удалить пользователя из набора данных
        /// </summary>
        /// <param name="id">Id пользователя</param>
        Task<Result> DeleteUserAsync(int id);
    }
}
