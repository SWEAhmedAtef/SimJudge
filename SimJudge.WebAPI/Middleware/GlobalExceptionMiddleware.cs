using SimJudge.Application.Common.Exceptions;
using SimJudge.Application.Common.Models;
using System.Net;
using System.Text.Json;

namespace SimJudge.WebAPI.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Instance = context.Request.Path,
                TraceId = context.TraceIdentifier
            };

            switch (exception)
            {
                case SimJudgeException simJudgeEx:
                    errorResponse.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    errorResponse.Title = "Application Error";
                    errorResponse.Status = (int)simJudgeEx.StatusCode;
                    errorResponse.Detail = simJudgeEx.Message;
                    errorResponse.ErrorCode = simJudgeEx.ErrorCode ?? "APPLICATION_ERROR";
                    response.StatusCode = (int)simJudgeEx.StatusCode;
                    break;

                case ValidationException validationEx:
                    errorResponse.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    errorResponse.Title = "Validation Error";
                    errorResponse.Status = (int)HttpStatusCode.BadRequest;
                    errorResponse.Detail = validationEx.Message;
                    errorResponse.ErrorCode = "VALIDATION_ERROR";
                    errorResponse.Errors = validationEx.Errors;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    errorResponse.Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
                    errorResponse.Title = "Internal Server Error";
                    errorResponse.Status = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Detail = _environment.IsDevelopment()
                        ? exception.ToString()
                        : "An error occurred while processing your request.";
                    errorResponse.ErrorCode = "INTERNAL_SERVER_ERROR";
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            // Log the exception with appropriate level
            if (response.StatusCode >= 500)
            {
                _logger.LogError(exception, "Internal server error occurred. TraceId: {TraceId}", errorResponse.TraceId);
            }
            else if (response.StatusCode >= 400)
            {
                _logger.LogWarning(exception, "Client error occurred. TraceId: {TraceId}, StatusCode: {StatusCode}",
                    errorResponse.TraceId, response.StatusCode);
            }

            var jsonResponse = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await response.WriteAsync(jsonResponse);
        }
    }
}
