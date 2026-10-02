using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Taller
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is BadHttpRequestException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = exception.Message
                }, cancellationToken);
                return true;
            }
            if (exception is ArgumentException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = exception.Message
                }, cancellationToken);
                return true;
            }
            if(exception is DbUpdateException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Database Error",
                    Detail = exception.Message
                }, cancellationToken);
                return true;
            }
            if(exception is Exception)
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "Something went wrong in the server side."
                }, cancellationToken);
                return true;
            }
            return false;
        }
    }
}
