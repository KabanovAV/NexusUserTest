using Microsoft.EntityFrameworkCore;
using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    /// <summary>
    /// Базовые операциии доступные для репозитория специализация
    /// </summary>
    public class SpecializationRepository(ApplicationDbContext db) : RepositoryOperations<Specialization>(db), ISpecializationRepository
    {
        public override IQueryable<Specialization> Data => Context.Specializations.Include(s => s.Groups).Include(s => s.Topics);

        /// <summary>
        /// Получение всех специализаций из набора данных
        /// </summary>
        /// <returns>Возвращает список специализаций из набора данных</returns>
        public async Task<IEnumerable<Specialization>> GetAllSpecializationAsync()
            => await PlainData.ToListAsync() ?? [];

        /// <summary>
        /// Получение одной специализации из набора данных
        /// </summary>
        /// <param name="id">Id специализации</param>
        /// <returns>Возвращает специализацию из набора данных</returns>
        public async Task<Specialization?> GetSpecializationByIdAsync(int id)
            => await GetAsync(id);

        /// <summary>
        /// Получение одной специализации из набора данных со связями
        /// </summary>
        /// <param name="id">Id специализации</param>
        /// <returns>Возвращает специализацию из набора данных</returns>
        public async Task<Specialization?> GetSpecializationByIdWithChildrenAsync(int id)
            => await Data.FirstAsync(s => s.Id == id);
    }
}
