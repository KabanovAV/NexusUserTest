using NexusUserTest.Common;
using NexusUserTest.Common.DTOs;

namespace NexusUserTest.Application.Common
{
    /// <summary>
    /// Интерфейс сервиса операциями для обьекта группы
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// Получение всех групп из набора данных
        /// </summary>
        /// <returns>Возвращает список групп из набора данных</returns>
        Task<Result<IEnumerable<GroupDTO>>> GetAllGroupAsync();

        /// <summary>
        /// Получение группы из набора данных по Id
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <returns>Возвращает группу из набора данных</returns>
        Task<Result<GroupDTO>> GetGroupByIdAsync(int id);

        /// <summary>
        /// Получение выпадающего списка групп из набора данных
        /// </summary>
        /// <returns>Возвращает список групп из набора данных</returns>
        Task<Result<IEnumerable<SelectItem>>> GetSelectGroupAsync();

        /// <summary>
        /// Добавить группу в набор данных
        /// </summary>
        /// <param name="createDto">Группа</param>
        /// <returns>Возвращает группу после добавления в БД</returns>
        Task<Result<GroupDTO>> CreateGroupAsync(CreateGroupDTO createDto);

        /// <summary>
        /// Изменить группу в наборе данных
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <param name="updateDto">Группа</param>
        Task<Result> UpdateGroupAsync(int id, UpdateGroupDTO updateDto);

        /// <summary>
        /// Удалить группу из набора данных
        /// </summary>
        /// <param name="id">Id группы</param>
        Task<Result> DeleteGroupAsync(int id);
    }
}
