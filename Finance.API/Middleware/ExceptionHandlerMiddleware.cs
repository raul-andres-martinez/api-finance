using Finance.Domain.Errors;
using System.Net;

namespace Finance.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string contentType = "application/json";

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = contentType;
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var error = ExceptionError.InternalError((HttpStatusCode)context.Response.StatusCode, ex.Message);

            return context.Response.WriteAsJsonAsync(error);
        }
    }
}