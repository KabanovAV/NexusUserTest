using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace NexusUserTest.WebApi
{
    internal sealed class DomainExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            Log.Warning(exception, "Ошибка домена: {Path} метод {Method}", httpContext.Request.Path, httpContext.Request.Method);

            var problemDatails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Ошибка бизнес-логики",
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = problemDatails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDatails, cancellationToken);

            return true;
        }
    }
}
