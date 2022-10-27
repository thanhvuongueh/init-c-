using InitalWebAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;

namespace InitalWebAPI.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        /// <inheritdoc/>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var response = context.Response;
            response.ContentType = MediaTypeNames.Application.Json;

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (ex is BadRequestException || ex is InvalidCastException || ex is ArgumentException || ex is InvalidOperationException)
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                }
                else if (ex is DBRecordNotFoundException)
                {
                    response.StatusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                var result = ProblemDetails(ex.Message, response.StatusCode);
                context.Items["ErrorMessage"] = result.Detail;
                context.Items["StackTrace"] = ex.StackTrace;
                context.Items["InnerException"] = ex.InnerException?.Message;
                await response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }

        private static ProblemDetails ProblemDetails(string message, int statusCode)
        {
            return new ProblemDetails
            {
                Status = statusCode,
                Title = GetErrorFromStatusCode(statusCode),
                Detail = message,
            };
        }

        private static string GetErrorFromStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case StatusCodes.Status400BadRequest:
                    return "Bad Request";
                case StatusCodes.Status404NotFound:
                    return "Not Found";
                case StatusCodes.Status500InternalServerError:
                    return "Internal Server Error";
                default:
                    return "Internal Server Error";
            }
        }
    }
}
