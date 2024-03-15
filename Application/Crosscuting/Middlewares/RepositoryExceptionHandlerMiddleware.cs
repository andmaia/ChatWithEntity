using Application.Crosscuting.Exceptions;
using System.Net;
using System.Text.Json;

namespace Application.Crosscuting.Middlewares
{
    public class RepositoryExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public RepositoryExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (RepositoryException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
