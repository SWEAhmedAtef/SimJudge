using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimJudge.Infrastructure.Data;

namespace SimJudge.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly SimJudgeDbContext _context;
        private readonly ILogger<HealthController> _logger;

        public HealthController(SimJudgeDbContext context, ILogger<HealthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                // Check database connectivity
                var canConnect = await _context.Database.CanConnectAsync();
                
                var health = new
                {
                    Status = "Healthy",
                    Timestamp = DateTime.UtcNow,
                    Database = canConnect ? "Connected" : "Disconnected",
                    Version = "1.0.0"
                };

                _logger.LogInformation("Health check completed successfully");
                return Ok(health);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Health check failed");
                
                var health = new
                {
                    Status = "Unhealthy",
                    Timestamp = DateTime.UtcNow,
                    Database = "Error",
                    Version = "1.0.0",
                    Error = ex.Message
                };

                return StatusCode(503, health);
            }
        }

        [HttpGet("ready")]
        public async Task<IActionResult> Ready()
        {
            try
            {
                // Check if the application is ready to serve requests
                var canConnect = await _context.Database.CanConnectAsync();
                
                if (!canConnect)
                {
                    return StatusCode(503, new { Status = "Not Ready", Reason = "Database not available" });
                }

                return Ok(new { Status = "Ready" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Readiness check failed");
                return StatusCode(503, new { Status = "Not Ready", Reason = ex.Message });
            }
        }

        [HttpGet("live")]
        public IActionResult Live()
        {
            // Simple liveness check - just return OK if the application is running
            return Ok(new { Status = "Alive", Timestamp = DateTime.UtcNow });
        }
    }
}
