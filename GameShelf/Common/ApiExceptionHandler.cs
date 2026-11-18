using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GameShelf.Common
{
    public class ApiExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct)
        {
            if (exception is not ApiException apiException)
                return false;
            context.Response.StatusCode = apiException.StatusCode;

            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = apiException.Message,
                Status = apiException.StatusCode
            }, ct);

            return true;
        }
    }
}
