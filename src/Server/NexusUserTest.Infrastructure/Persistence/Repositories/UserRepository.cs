using Microsoft.EntityFrameworkCore;
using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    /// <summary>
    /// Базовые операциии доступные для репозитория пользователь
    /// </summary>
    public class UserRepository(ApplicationDbContext db) : RepositoryOperations<User>(db), IUserRepository
    {
        /// <summary>
        /// Получение всех пользователей из набора данных
        /// </summary>
        /// <returns>Возвращает список всех пользователей из набора данных</returns>
        public async Task<IEnumerable<User>> GetAllUserAsync()
            => await PlainData.ToListAsync();

        /// <summary>
        /// Получение одного пользователя из набора данных
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Возвращает одного пользователя из набора данных</returns>
        public async Task<User?> GetUserByIdAsync(int id)
            => await GetByIdAsync(id);
    }
}
