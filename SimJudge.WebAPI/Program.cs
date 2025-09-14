using Microsoft.EntityFrameworkCore;
using SimJudge.Application.Mappings;
using SimJudge.Application.Services;
using SimJudge.Domain.Interfaces.Services;
using SimJudge.Infrastructure;
using SimJudge.Infrastructure.Data;
using SimJudge.WebAPI.Middleware;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "SimJudge")
    .WriteTo.Console()
    .WriteTo.File("logs/simjudge-.txt", 
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        fileSizeLimitBytes: 10 * 1024 * 1024) // 10MB
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(SimJudge.Application.Mappings.MappingProfile));

// Add Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// Add Application Services
builder.Services.AddScoped<IProblemService, ProblemService>();
builder.Services.AddScoped<IContestService, ContestService>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<SimJudge.Application.Common.Interfaces.ILoggingService, SimJudge.Application.Common.Services.LoggingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add middleware in correct order
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SimJudgeDbContext>();
    context.Database.EnsureCreated();
    await SimJudge.Infrastructure.Data.SeedData.SeedAsync(context);
}

try
{
    Log.Information("Starting SimJudge Web API");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
