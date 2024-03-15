using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Crosscuting.Exceptions;

using Microsoft.AspNetCore.Http;

namespace Application.Crosscuting.Middlewares
{
    public class UserCreationExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public UserCreationExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UserCreationException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (RepositoryException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                context.Response.StatusCode,
                ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
