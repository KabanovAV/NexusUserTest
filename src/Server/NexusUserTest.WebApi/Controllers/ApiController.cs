using Microsoft.AspNetCore.Mvc;
using NexusUserTest.Application.Common;

namespace NexusUserTest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : Controller
    {
        /// <summary>
        /// Обработка <c>Ok</c> результата запроса
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <returns>Возвращает обьект</returns>
        protected ActionResult<T> HandleOkResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);

            return result.Error.Type switch
            {
                ErrorType.NotFound => NotFound(result.Error),
                ErrorType.Validation => BadRequest(result.Error),
                ErrorType.Conflict => Conflict(result.Error),
                ErrorType.Problem => UnprocessableEntity(result.Error),
                _ => StatusCode(500, result.Error)
            };
        }

        /// <summary>
        /// Обработка <c>NoContent</c> результата запроса
        /// </summary>
        protected IActionResult HandleNoContentResult(Result result)
        {
            if (result.IsSuccess)
                return NoContent();

            return result.Error.Type switch
            {
                ErrorType.NotFound => NotFound(result.Error),
                ErrorType.Validation => BadRequest(result.Error),
                ErrorType.Conflict => Conflict(result.Error),
                ErrorType.Problem => UnprocessableEntity(result.Error),
                _ => StatusCode(500, result.Error)
            };
        }

        /// <summary>
        /// Обработка <c>CreatedAtAction</c> результата запроса
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <returns>Возвращает обьект</returns>
        protected ActionResult<T> HandleCreatedResult<T>(Result<T> result, string actionName, object routeValues)
        {
            if (result.IsSuccess)
                return CreatedAtAction(actionName, routeValues, result.Value);

            return result.Error.Type switch
            {
                ErrorType.Validation => BadRequest(result.Error),
                _ => StatusCode(500, result.Error)
            };
        }
    }
}
