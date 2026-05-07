using NexusUserTest.Application.Common;
using NexusUserTest.Application.Common.Errors;
using NexusUserTest.Application.Common.Interfaces;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Common;
using NexusUserTest.Common.DTOs.Commands;
using NexusUserTest.Common.DTOs.Queries;
using NexusUserTest.Domain.Common.Interfaces;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Services
{
    /// <summary>
    /// Сервис с операциями для обьекта ответ
    /// </summary>
    public class GroupService : IGroupService
    {
        private readonly IRepository _repository;
        private readonly IValidationService _validationService;

        public GroupService(IRepository repository, IValidationService validationService)
        {
            _repository = repository;
            _validationService = validationService;
        }

        /// <summary>
        /// Получение всех групп из набора данных
        /// </summary>
        /// <returns>Возвращает список групп из набора данных</returns>
        public async Task<Result<IEnumerable<GroupDTO>>> GetAllGroupAsync()
        {
            var groups = await _repository.Group.GetAllGroupAsync();
            return groups.ToDto();
        }

        /// <summary>
        /// Получение группы из набора данных по Id
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <returns>Возвращает группу из набора данных</returns>
        public async Task<Result<GroupDTO>> GetGroupByIdAsync(int id)
        {
            var group = await _repository.Group.GetGroupByIdAsync(id);
            if (group == null)
                return Result.Failure<GroupDTO>(GroupErrors.NotFound(id));
            return group.ToDto();
        }

        /// <summary>
        /// Получение выпадающего списка групп из набора данных
        /// </summary>
        /// <returns>Возвращает список групп из набора данных</returns>
        public async Task<Result<IEnumerable<SelectItem>>> GetSelectGroupAsync()
        {
            var groups = await _repository.Group.GetAllGroupAsync();
            return groups.ToSelect();
        }

        /// <summary>
        /// Добавить группу в набор данных
        /// </summary>
        /// <param name="createDto">Группа</param>
        /// <returns>Возвращает группу после добавления в БД</returns>
        public async Task<Result<GroupDTO>> CreateGroupAsync(CreateGroupDTO createDto)
        {
            var validation = await _validationService.ValidateAsync(createDto);
            if (validation.IsSuccess)
            {
                var group = new Group()
                {
                    Title = createDto.Title,
                    SpecializationId = createDto.SpecializationId,
                    Begin = createDto.Begin,
                    End = createDto.End
                };
                await _repository.Group.AddAsync(group);
                return group.ToDto();
            }
            return Result.Failure<GroupDTO>(validation.Error);
        }

        /// <summary>
        /// Изменить группу в наборе данных
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <param name="updateDto">Группа</param>
        public async Task<Result> UpdateGroupAsync(int id, UpdateGroupDTO updateDto)
        {
            if (id != updateDto.Id)
                return Result.Failure(GroupErrors.Conflict(id, updateDto.Id));

            var validation = await _validationService.ValidateAsync(updateDto);
            if (validation.IsSuccess)
            {
                var group = await _repository.Group.GetGroupByIdAsync(id);
                if (group == null)
                    return Result.Failure(GroupErrors.NotFound(id));
                group.UpdateFromDto(updateDto);
                await _repository.Group.Update(group);
                return Result.Success();
            }
            return Result.Failure(validation.Error);
        }

        /// <summary>
        /// Удалить группу из набора данных
        /// </summary>
        /// <param name="id">Id группы</param>
        public async Task<Result> DeleteGroupAsync(int id)
        {
            var group = await _repository.Group.GetGroupByIdWithChildrenAsync(id);
            if (group == null)
                return Result.Failure(GroupErrors.NotFound(id));
            if (group.GroupUsers != null && group.GroupUsers.Count == 0)
            {
                await _repository.Group.Remove(id);
            }
            return Result.Failure(GroupErrors.Connection(id));
        }
    }
}
