using AuditTrailRepository.Interface;
using AuditTrailRepository.Repositories;
using AuditTrailService.Interface;
using AuditTrailService.Services;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<Program>(); ;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuditTrail API", Version = "v1" });
});

builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddSingleton<IAuditRepository, AuditRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
