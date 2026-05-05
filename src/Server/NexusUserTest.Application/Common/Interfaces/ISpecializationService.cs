using NexusUserTest.Common;
using NexusUserTest.Common.DTOs;

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
        Task<Result<IEnumerable<SpecializationDTO>>> GetAllSpecializationAsync();        

        /// <summary>
        /// Получение специализации из набора данных по Id
        /// </summary>
        /// <param name="id">Id специализации</param>
        /// <returns>Возвращает специализацию из набора данных</returns>
        Task<Result<SpecializationDTO>> GetSpecializationByIdAsync(int id);

        /// <summary>
        /// Получение выпадающего списка специализаций из набора данных
        /// </summary>
        /// <returns>Возвращает список специализаций из набора данных</returns>
        Task<Result<IEnumerable<SelectItem>>> GetSelectSpecializationAsync();

        /// <summary>
        /// Добавить специализацию в набор данных
        /// </summary>
        /// <param name="createDto">Специализация</param>
        /// <returns>Возвращает специализацию после добавления в БД</returns>
        Task<Result<SpecializationDTO>> CreateSpecializationAsync(CreateSpecializationDTO createDto);

        /// <summary>
        /// Изменить специализацию в наборе данных
        /// </summary>
        /// <param name="id">Id специализации</param>
        /// <param name="updateDto">Специализация</param>
        Task<Result> UpdateSpecializationAsync(int id, UpdateSpecializationDTO updateDto);

        /// <summary>
        /// Удалить специализацию из набора данных
        /// </summary>
        /// <param name="id">Id специализации</param>
        Task<Result<bool>> DeleteSpecializationAsync(int id);
    }
}
