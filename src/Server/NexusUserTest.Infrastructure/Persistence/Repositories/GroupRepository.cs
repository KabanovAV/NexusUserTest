using Microsoft.EntityFrameworkCore;
using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    /// <summary>
    /// Базовые операциии доступные для репозитория группы
    /// </summary>
    public class GroupRepository(ApplicationDbContext db) : RepositoryOperations<Group>(db), IGroupRepository
    {
        /// <summary>
        /// Получение всех групп из набора данных
        /// </summary>
        /// <returns>Возвращает список групп из набора данных</returns>
        public async Task<IEnumerable<Group>> GetAllGroupAsync()
            => await PlainData.ToListAsync();

        /// <summary>
        /// Получение группы из набора данных
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <returns>Возвращает группу из набора данных</returns>
        public async Task<Group?> GetGroupByIdAsync(int id)
            => await GetAsync(id);
    }
}
