# Logging and Exception Handling in SimJudge

## Overview
This document describes the comprehensive logging and exception handling system implemented in the SimJudge application.

## Exception Handling

### Custom Exceptions
The application uses custom exceptions for better error handling and user experience:

- **`SimJudgeException`**: Base exception class with HTTP status codes and error codes
- **`NotFoundException`**: Thrown when a requested resource is not found (404)
- **`ValidationException`**: Thrown when validation fails (400)
- **`UnauthorizedException`**: Thrown when authentication is required (401)
- **`ForbiddenException`**: Thrown when access is denied (403)

### Global Exception Middleware
The `GlobalExceptionMiddleware` catches all unhandled exceptions and:
- Maps exceptions to appropriate HTTP status codes
- Returns structured error responses
- Logs exceptions with appropriate severity levels
- Provides different error details for development vs production

### Error Response Format
All errors are returned in a consistent format:
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "Error Title",
  "status": 400,
  "detail": "Detailed error message",
  "errorCode": "VALIDATION_ERROR",
  "errors": {
    "fieldName": ["Error message 1", "Error message 2"]
  },
  "instance": "/api/problems",
  "traceId": "0HMQ8VQKJQJQJ",
  "timestamp": "2024-01-15T10:30:00Z"
}
```

## Logging System

### Serilog Configuration
The application uses Serilog for structured logging with:
- Console output for development
- File output with daily rotation
- Different log levels for different environments
- Enriched with application context

### Log Levels
- **Debug**: Detailed information for debugging
- **Information**: General application flow
- **Warning**: Potentially harmful situations
- **Error**: Error events that might still allow the application to continue
- **Fatal**: Very severe errors that might cause the application to terminate

### Middleware Components

#### 1. Request Logging Middleware
- Logs all incoming requests with method, path, query string, user agent, and IP
- Logs request/response bodies for debugging (with size limits)
- Captures response status codes and execution time

#### 2. Performance Middleware
- Tracks request execution time
- Identifies slow requests (configurable threshold)
- Adds performance headers to responses
- Logs performance metrics

#### 3. Global Exception Middleware
- Catches and handles all unhandled exceptions
- Maps exceptions to appropriate HTTP responses
- Logs exceptions with proper severity levels

### Business Logging Service
The `ILoggingService` provides structured logging for business events:
```csharp
// Example usage in services
_loggingService.LogBusinessEvent("ContestCreated", new { ContestId = contest.Id }, userId);
_loggingService.LogInformation("User {UserId} submitted solution for problem {ProblemId}", userId, problemId);
```

## Configuration

### Development Settings
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Performance": {
    "SlowRequestThresholdMs": 1000
  }
}
```

### Production Settings
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Performance": {
    "SlowRequestThresholdMs": 2000
  }
}
```

## Health Checks

### Endpoints
- **`GET /api/health`**: Comprehensive health check including database connectivity
- **`GET /api/health/ready`**: Readiness check for load balancers
- **`GET /api/health/live`**: Liveness check for container orchestration

### Health Check Response
```json
{
  "status": "Healthy",
  "timestamp": "2024-01-15T10:30:00Z",
  "database": "Connected",
  "version": "1.0.0"
}
```

## Log Files

### File Structure
- **Location**: `logs/simjudge-{date}.txt`
- **Rotation**: Daily
- **Retention**: 30 days
- **Size Limit**: 10MB per file

### Log Format
```
2024-01-15 10:30:00.123 +00:00 [INF] Request started. Method: GET, Path: /api/problems, TraceId: 0HMQ8VQKJQJQJ
2024-01-15 10:30:00.456 +00:00 [INF] Request completed. StatusCode: 200, Duration: 333ms, TraceId: 0HMQ8VQKJQJQJ
```

## Best Practices

### Exception Handling
1. Use specific exception types for different error scenarios
2. Always include meaningful error messages
3. Don't expose sensitive information in error responses
4. Log exceptions with appropriate context

### Logging
1. Use structured logging with consistent property names
2. Include correlation IDs (TraceId) for request tracking
3. Log business events for audit trails
4. Use appropriate log levels
5. Avoid logging sensitive data (passwords, tokens, etc.)

### Performance Monitoring
1. Monitor slow request thresholds
2. Track database query performance
3. Log performance metrics for analysis
4. Set up alerts for performance degradation

## Monitoring and Alerting

### Key Metrics to Monitor
- Request response times
- Error rates by endpoint
- Database connection health
- Memory and CPU usage
- Log error patterns

### Recommended Alerts
- High error rate (>5% of requests)
- Slow response times (>2 seconds)
- Database connectivity issues
- High memory usage
- Unhandled exceptions

## Troubleshooting

### Common Issues
1. **High log volume**: Adjust log levels in configuration
2. **Large log files**: Reduce request/response body logging
3. **Missing logs**: Check file permissions and disk space
4. **Performance issues**: Review slow request thresholds

### Debug Mode
Enable debug logging by setting:
```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Debug"
    }
  }
}
```

This will provide detailed request/response logging for debugging purposes.
