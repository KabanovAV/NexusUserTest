using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Common;
using NexusUserTest.Common;
using NexusUserTest.Common.DTOs;
using NexusUserTest.WebApi.Controllers;

namespace SibCCSPETest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController(ISpecializationService service) : ApiController
    {
        /// <summary>
        /// Получение списка специализаций
        /// </summary>
        /// <returns>Возвращает список специализаций</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SpecializationDTO>>> GetAll()
        {
            var specializations = await service.GetAllSpecializationAsync();
            return HandleOkResult(specializations);
        }

        /// <summary>
        /// Получение специализации по Id
        /// </summary>
        /// <param name="id">Id специализации</param>
        /// <returns>Возвращает специализацию</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        /// <response code="404">Специализация не найдена</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpecializationDTO>> GetById([FromRoute] int id)
        {
            var specialization = await service.GetSpecializationByIdAsync(id);
            return HandleOkResult(specialization);
        }

        /// <summary>
        /// Получение выпадающего списка специализаций
        /// </summary>
        /// <returns>Возвращает список специализаций</returns>
        /// <response code="200">Успешное выполнение запроса</response>
        [HttpGet("select")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SelectItem>>> GetSelect()
        {
            var specializations = await service.GetSelectSpecializationAsync();
            return HandleOkResult(specializations);
        }

        /// <summary>
        /// Добавить новую специализацию
        /// </summary>
        /// <param name="cSpecialization">Специализация</param>
        /// <returns>Возвращает новую специализацию</returns>
        /// <response code="201">Успешное выполнение запроса</response>
        /// <response code="400">Ошибка валидации данных</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SpecializationDTO>> Create([FromBody] CreateSpecializationDTO cSpecialization)
        {
            var result = await service.CreateSpecializationAsync(cSpecialization);
            return HandleCreatedResult(nameof(GetById), () => new { id = result.Value.Id }, result);
        }

        /// <summary>
        /// Обновление данных специализации
        /// </summary>
        /// <param name="id">Id специализации</param>
        /// <param name="uSpecialization">Измененные данные специализации</param>
        /// <response code="204">Успешное выполнение запроса</response>
        /// <response code="400">Некорректный запрос</response>
        /// <response code="404">Специализация не найдена</response>
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSpecializationDTO uSpecialization)
        {
            var result = await service.UpdateSpecializationAsync(id, uSpecialization);
            return HandleNoContentResult(result);
        }

        /// <summary>
        /// Удаление данных о специализации
        /// </summary>
        /// <param name="id">Id специализации</param>
        /// <response code="204">Успешное выполнение запроса</response>
        /// <response code="404">Специализация не найдена</response>
        /// <response code="409">Специализация имеет связи с таблицами</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await service.DeleteSpecializationAsync(id);
            return HandleNoContentResult(result);
        }
    }
}
