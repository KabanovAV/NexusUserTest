using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Common
{
    /// <summary>
    /// Интерфейс сервиса операциями для обьекта специализации
    /// </summary>
    public interface ISpecializationService
    {
        /// <summary>
        /// Получение всех специализаций из набора данных
        /// </summary>
        /// <returns>Возвращает список специализаций из набора данных</returns>
        Task<IEnumerable<Specialization>> GetAllSpecializationAsync();

        /// <summary>
        /// Получение специализации из набора данных по Id
        /// </summary>
        /// <param name="id">Id специализации</param>
        /// <returns>Возвращает специализацию из набора данных</returns>
        Task<Specialization?> GetSpecializationByIdAsync(int id);

        /// <summary>
        /// Добавить специализацию в набор данных
        /// </summary>
        /// <param name="entity">Специализация</param>
        /// <returns>Возвращает специализацию после добавления в БД</returns>
        Task<Specialization> AddSpecializationAsync(Specialization entity);

        /// <summary>
        /// Изменить специализацию в наборе данных
        /// </summary>
        /// <param name="entity">Специализация</param>
        Task<Specialization> UpdateSpecializationAsync(Specialization entity);

        /// <summary>
        /// Удалить специализацию из набора данных
        /// </summary>
        /// <param name="id">Id специализации</param>
        Task DeleteSpecializationAsync(int id);
    }
}
