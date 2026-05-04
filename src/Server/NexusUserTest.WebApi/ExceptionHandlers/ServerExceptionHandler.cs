using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace NexusUserTest.WebApi
{
    internal sealed class ServerExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            Log.Error(exception, "Необработанная ошибка: {Path} метод {Method}", httpContext.Request.Path, httpContext.Request.Method);

            var problemDatails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Внутренняя ошибка сервера",
                Detail = "Произошла внутренняя ошибка. Пожалуйста, попробуйте позже.",
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = problemDatails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDatails, cancellationToken);

            return true;
        }
    }
}
