using System.Diagnostics;

namespace SimJudge.WebAPI.Middleware
{
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformanceMiddleware> _logger;
        private readonly long _slowRequestThresholdMs;

        public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _slowRequestThresholdMs = configuration.GetValue<long>("Performance:SlowRequestThresholdMs", 1000);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var requestId = context.TraceIdentifier;

            try
            {
                await _next(context);
            }
            finally
            {
                stopwatch.Stop();
                var elapsedMs = stopwatch.ElapsedMilliseconds;

                // Log performance metrics
                _logger.LogInformation("Request performance. Method: {Method}, Path: {Path}, " +
                    "StatusCode: {StatusCode}, Duration: {Duration}ms, TraceId: {TraceId}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    elapsedMs,
                    requestId);

                // Log slow requests as warnings
                if (elapsedMs > _slowRequestThresholdMs)
                {
                    _logger.LogWarning("Slow request detected. Method: {Method}, Path: {Path}, " +
                        "Duration: {Duration}ms (threshold: {Threshold}ms), TraceId: {TraceId}",
                        context.Request.Method,
                        context.Request.Path,
                        elapsedMs,
                        _slowRequestThresholdMs,
                        requestId);
                }

                // Add performance headers
                context.Response.Headers.Add("X-Response-Time", $"{elapsedMs}ms");
                context.Response.Headers.Add("X-Request-ID", requestId);
            }
        }
    }
}
