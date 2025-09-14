using System.Diagnostics;
using System.Text;

namespace SimJudge.WebAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var requestId = context.TraceIdentifier;

            // Log request details
            _logger.LogInformation("Request started. Method: {Method}, Path: {Path}, QueryString: {QueryString}, " +
                "UserAgent: {UserAgent}, RemoteIP: {RemoteIP}, TraceId: {TraceId}",
                context.Request.Method,
                context.Request.Path,
                context.Request.QueryString,
                context.Request.Headers.UserAgent.ToString(),
                context.Connection.RemoteIpAddress?.ToString(),
                requestId);

            // Capture request body for logging (if needed)
            if (ShouldLogRequestBody(context.Request))
            {
                context.Request.EnableBuffering();
                var requestBody = await ReadRequestBodyAsync(context.Request);
                if (!string.IsNullOrEmpty(requestBody))
                {
                    _logger.LogDebug("Request body for {TraceId}: {RequestBody}", requestId, requestBody);
                }
            }

            // Capture response body for logging
            var originalResponseBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();

                // Log response details
                _logger.LogInformation("Request completed. StatusCode: {StatusCode}, Duration: {Duration}ms, " +
                    "ContentType: {ContentType}, TraceId: {TraceId}",
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds,
                    context.Response.ContentType,
                    requestId);

                // Log response body for debugging (if needed)
                if (ShouldLogResponseBody(context.Response))
                {
                    var responseBody = await ReadResponseBodyAsync(responseBodyStream);
                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        _logger.LogDebug("Response body for {TraceId}: {ResponseBody}", requestId, responseBody);
                    }
                }

                // Copy response back to original stream
                await responseBodyStream.CopyToAsync(originalResponseBodyStream);
                context.Response.Body = originalResponseBodyStream;
            }
        }

        private static bool ShouldLogRequestBody(HttpRequest request)
        {
            // Only log request body for POST, PUT, PATCH requests
            return request.Method is "POST" or "PUT" or "PATCH" &&
                   request.ContentType?.Contains("application/json") == true &&
                   request.ContentLength > 0 &&
                   request.ContentLength < 10000; // Limit size to avoid logging large files
        }

        private static bool ShouldLogResponseBody(HttpResponse response)
        {
            // Only log response body for JSON responses and successful status codes
            return response.ContentType?.Contains("application/json") == true &&
                   response.StatusCode >= 200 &&
                   response.StatusCode < 300;
        }

        private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            try
            {
                request.Body.Position = 0;
                using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                return body;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static async Task<string> ReadResponseBodyAsync(Stream responseBodyStream)
        {
            try
            {
                responseBodyStream.Position = 0;
                using var reader = new StreamReader(responseBodyStream, Encoding.UTF8, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                responseBodyStream.Position = 0;
                return body;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
