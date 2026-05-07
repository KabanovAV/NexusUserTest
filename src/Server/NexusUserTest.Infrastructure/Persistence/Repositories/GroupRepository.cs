using Microsoft.EntityFrameworkCore;
using NexusUserTest.Domain.Common.Interfaces;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    /// <summary>
    /// Базовые операциии доступные для репозитория группы
    /// </summary>
    public class GroupRepository(ApplicationDbContext db) : RepositoryOperations<Group>(db), IGroupRepository
    {
        public override IQueryable<Group> Data => Context.Groups.Include(g => g.Specialization).Include(g => g.GroupUsers).Include(g => g.Setting);

        /// <summary>
        /// Получение всех групп из набора данных
        /// </summary>
        /// <returns>Возвращает список групп из набора данных</returns>
        public async Task<IEnumerable<Group>> GetAllGroupAsync()
            => await PlainData.Include(g => g.Specialization).Include(g => g.GroupUsers).ToListAsync();

        /// <summary>
        /// Получение группы из набора данных
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <returns>Возвращает группу из набора данных</returns>
        public async Task<Group?> GetGroupByIdAsync(int id)
            => await PlainData.Include(g => g.Specialization).Include(g => g.GroupUsers).FirstOrDefaultAsync(g => g.Id == id);

        /// <summary>
        /// Получение одной группы из набора данных со связями
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <returns>Возвращает группу из набора данных</returns>
        public async Task<Group?> GetGroupByIdWithChildrenAsync(int id)
            => await Data.FirstAsync(g => g.Id == id);
    }
}
