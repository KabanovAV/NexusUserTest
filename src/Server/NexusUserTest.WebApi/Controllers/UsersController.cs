using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Common;
using NexusUserTest.Common.DTOs;
using NexusUserTest.WebApi.Controllers;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService service) : ApiController
    {
        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <returns>Возвращает список пользователей</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await service.GetAllUserAsync();
            return HandleOkResult(users);
        }

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Возвращает пользователя</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDTO>> GetById([FromRoute] int id)
        {
            var user = await service.GetUserByIdAsync(id);
            return HandleOkResult(user);
        }

        /// <summary>
        /// Добавить нового пользователя
        /// </summary>
        /// <param name="cUser">Пользователь</param>
        /// <returns>Возвращает нового пользователя</returns>
        /// <response code="201">Успешное выполнение запроса</response>
        /// <response code="400">Ошибка валидации данных</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Create([FromBody] CreateUserDTO cUser)
        {
            var result = await service.CreateUserAsync(cUser);
            return HandleCreatedResult(nameof(GetById), () => new { id = result.Value.Id }, result);
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="uUSer">Измененные данные пользователя</param>
        /// <response code="204">Успешное выполнение запроса</response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserDTO uUSer)
        {
            var result = await service.UpdateUserAsync(id, uUSer);
            return HandleNoContentResult(result);
        }

        /// <summary>
        /// Удаление данных о пользователе
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <response code="204">Успешное выполнение запроса</response>
        /// <response code="404">Пользователь не найдена</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.DeleteUserAsync(id);
            return HandleNoContentResult(result);
        }
    }
}
