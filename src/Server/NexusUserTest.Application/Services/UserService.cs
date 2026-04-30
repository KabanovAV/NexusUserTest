using NexusUserTest.Application.Common;
using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Services
{
    /// <summary>
    /// Сервис с операциями для обьекта ответ
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository _repository;

        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получение всех пользователей из набора данных
        /// </summary>
        /// <returns>Возвращает список пользователей из набора данных</returns>
        public async Task<IEnumerable<User>> GetAllUserAsync()
            => await _repository.User.GetAllUserAsync();

        /// <summary>
        /// Получение пользователя из набора данных по Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Возвращает пользователя из набора данных</returns>
        public async Task<User?> GetUserByIdAsync(int id)
            => await _repository.User.GetUserByIdAsync(id);

        /// <summary>
        /// Добавить пользователя в набор данных
        /// </summary>
        /// <param name="entity">Пользователь</param>
        /// <returns>Возвращает пользователя после добавления в БД</returns>
        public async Task<User> AddUserAsync(User entity)
        {
            await _repository.User.AddAsync(entity);
            return await _repository.User.GetUserByIdAsync(entity.Id);
        }

        /// <summary>
        /// Изменить пользователя в наборе данных
        /// </summary>
        /// <param name="entity">Пользователь</param>
        public async Task<User> UpdateUserAsync(User entity)
        {
            await _repository.User.Update(entity);
            return await _repository.User.GetUserByIdAsync(entity.Id);
        }

        /// <summary>
        /// Удалить пользователя из набора данных
        /// </summary>
        /// <param name="id">Id пользователя</param>
        public async Task DeleteUserAsync(int id)
            => await _repository.User.Remove(id);
    }
}
