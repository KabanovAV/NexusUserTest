using Microsoft.EntityFrameworkCore;
using NexusUserTest.Domain.Common.Interfaces;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    /// <summary>
    /// Базовые операциии доступные для репозитория группа пользователя
    /// </summary>
    public class GroupUserRepository(ApplicationDbContext db) : RepositoryOperations<GroupUser>(db), IGroupUserRepository
    {
        /// <summary>
        /// Получение всех групп пользователей из набора данных
        /// </summary>
        /// <returns>Возвращает список групп пользователей из набора данных</returns>
        public async Task<IEnumerable<GroupUser>> GetAllGroupUserAsync()
            => await PlainData.ToListAsync();

        /// <summary>
        /// Получение группы пользователя из набора данных
        /// </summary>
        /// <param name="id">Id группа пользователя</param>
        /// <returns>Возвращает группу пользователя из набора данных</returns>
        public async Task<GroupUser?> GetGroupUserByIdAsync(int id)
            => await GetByIdAsync(id);
    }
}
