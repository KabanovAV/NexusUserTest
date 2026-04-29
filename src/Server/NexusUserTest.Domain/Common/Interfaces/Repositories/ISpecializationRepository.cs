using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Domain.Common
{
    /// <summary>
    /// Интерфейс с операциями для репозитория специализации
    /// </summary>
    public interface ISpecializationRepository : IRepositoryOperations<Specialization>
    {
        /// <summary>
        /// Получение всех специализаций из набора данных
        /// </summary>
        /// <returns>Возвращает список специализаций из набора данных</returns>
        Task<IEnumerable<Specialization>> GetAllSpecializationAsync();

        /// <summary>
        /// Получение одной специализации из набора данных
        /// </summary>
        /// <param name="id">Id специализации</param>
        /// <returns>Возвращает специализацию из набора данных</returns>
        Task<Specialization?> GetSpecializationByIdAsync(int id);

        /// <summary>
        /// Получение одной специализации из набора данных со связями
        /// </summary>
        /// <param name="id">Id специализации</param>
        /// <returns>Возвращает специализацию из набора данных</returns>
        Task<Specialization?> GetSpecializationByIdWithChildrenAsync(int id);
    }
}
