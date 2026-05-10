using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Suitability.Api.Extensions;
using Suitability.Application;
using Suitability.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", cfg =>
    {
        cfg.Window = TimeSpan.FromSeconds(10);
        cfg.PermitLimit = 20;
        cfg.QueueLimit = 0;
    });
});

builder.Services.AddOpenTelemetry().WithTracing(tracer => tracer
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Suitability.Api"))
    .AddAspNetCoreInstrumentation());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = "suitability-api",
            ValidAudience = "suitability-client",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key-super-secret-key"))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuitabilityReader", p => p.RequireClaim("scope", "suitability.read"));
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddValidatorsFromAssemblyContaining<ISuitabilityMarker>();

var app = builder.Build();
app.UseExceptionHandler();
app.UseSerilogRequestLogging();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.MapSuitabilityEndpoints();
app.Run();

public partial class Program;
