#nullable disable

using Application.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Application.Shared.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                var errorTypeName = error.GetType().Name;
                response.ContentType = "application/json";

                switch (errorTypeName)
                {
                    case nameof(KeyNotFoundException):
                        await HandleKeyNotFoundException(error as KeyNotFoundException, response);
                        break;
                    case nameof(ValidationException):
                        await HandleValidationException(error as ValidationException, response);
                        break;
                    case nameof(ArgumentException):
                        await HandleArgumentException(error as ArgumentException, response);
                        break;
                    case nameof(DbUpdateException):
                        await HandleDbException(error as DbUpdateException, response);
                        break;
                    default:
                        await HandleUnknownException(error, response);
                        break;
                }
            }
        }

        private async Task HandleValidationException(ValidationException exception, HttpResponse response)
        {
            await HandleException(response, HttpStatusCode.BadRequest, exception.Failures.SelectMany(failure => failure.Value), "Validation error");
        }

        private async Task HandleKeyNotFoundException(KeyNotFoundException exception, HttpResponse response)
        {
            await HandleException(response, HttpStatusCode.NotFound, new List<string> { exception.Message }, "Key not found error");
        }

        private async Task HandleArgumentException(ArgumentException exception, HttpResponse response)
        {
            await HandleException(response, HttpStatusCode.BadRequest, new List<string> { exception.Message });
        }

        private async Task HandleDbException(DbUpdateException exception, HttpResponse response)
        {
            await HandleException(response, HttpStatusCode.BadRequest, new List<string> { exception.InnerException.Message });
        }

        private async Task HandleUnknownException(Exception exception, HttpResponse response)
        {
            await HandleException(response, HttpStatusCode.InternalServerError, new List<string> { exception.Message });
        }

        private async Task HandleException(
            HttpResponse response,
            HttpStatusCode httpStatusCode,
            IEnumerable<string> errors,
            string message = "Internal System Error")
        {
            var responseModel = new BaseResponse(message, httpStatusCode) { Errors = errors?.ToList() };
            response.StatusCode = (int)httpStatusCode;

            var result = JsonSerializer.Serialize(responseModel);
            await response.WriteAsync(result);
        }
    }
}
