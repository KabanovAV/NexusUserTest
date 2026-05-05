using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Common;
using NexusUserTest.Common;
using NexusUserTest.Common.DTOs;
using NexusUserTest.WebApi.Controllers;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IGroupService service) : ApiController
    {
        /// <summary>
        /// Получение списка групп
        /// </summary>
        /// <returns>Возвращает список групп</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetAll()
        {
            var groups = await service.GetAllGroupAsync();
            return HandleOkResult(groups);
        }

        /// <summary>
        /// Получение группы по Id
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <returns>Возвращает группу</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Группа не найдена</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GroupDTO>> GetById([FromRoute] int id)
        {
            var group = await service.GetGroupByIdAsync(id);
            return HandleOkResult(group);
        }

        /// <summary>
        /// Получение выпадающего списка групп
        /// </summary>
        /// <returns>Возвращает список групп</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet("select")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SelectItem>>> GetSelect()
        {
            var groups = await service.GetSelectGroupAsync();
            return HandleOkResult(groups);
        }

        /// <summary>
        /// Добавить новую группу
        /// </summary>
        /// <param name="cGroup">Группа</param>
        /// <returns>Возвращает новую группу</returns>
        /// <response code="201">Успешное выполнение запроса</response>
        /// <response code="400">Ошибка валидации данных</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GroupDTO>> Create([FromBody] CreateGroupDTO cGroup)
        {
            var result = await service.CreateGroupAsync(cGroup);
            return HandleCreatedResult(result, nameof(GetById), new { id = result.Value.Id });
        }

        /// <summary>
        /// Обновление данных группы
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <param name="uGroup">Измененные данные группы</param>
        /// <response code="204">Успешное выполнение запроса</response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="404">Группа не найдена</response>
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateGroupDTO uGroup)
        {
            var result = await service.UpdateGroupAsync(id, uGroup);
            return HandleNoContentResult(result);
        }

        /// <summary>
        /// Удаление данных о группе
        /// </summary>
        /// <param name="id">Id группы</param>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Группа не найдена</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await service.DeleteGroupAsync(id);
            return HandleNoContentResult(result);
        }
    }
}
