
using System.Text.Json;
using FluentValidation;
using ShoesShop.Application.Common.Exceptions;

namespace ShoesShop.WebApi.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        public RequestDelegate next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) => this.next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            int code;
            string? result;
            switch (ex)
            {
                case ValidationException validationException:
                    code = StatusCodes.Status400BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;
                case NotFoundException notFoundException:
                    code = StatusCodes.Status404NotFound;
                    result = JsonSerializer.Serialize(notFoundException.Message);
                    break;
                case AlreadyExistsException alreadyExistsException:
                    code = StatusCodes.Status409Conflict;
                    result = JsonSerializer.Serialize(alreadyExistsException.Message);
                    break;
                case AuthenticationException authenticationException:
                    code = StatusCodes.Status400BadRequest;
                    result = JsonSerializer.Serialize(authenticationException.Message);
                    break;
                default:
                    code = StatusCodes.Status500InternalServerError;
                    result = JsonSerializer.Serialize(ex.Message);
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            return context.Response.WriteAsync(result);
        }
    }
}
