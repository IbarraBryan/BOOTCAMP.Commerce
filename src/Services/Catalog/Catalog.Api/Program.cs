using Catalog.Persistence.Database;
using Catalog.Service.Queries.Interfaces;
using Catalog.Service.Queries.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Common.Logging;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<CatalogDbContext>(
options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Catalog")
    )
);

// Health check
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy())
    .AddDbContextCheck<CatalogDbContext>(typeof(CatalogDbContext).Name);

// Query Services
builder.Services.AddTransient<IProductQueryService, ProductQueryService>();
//builder.Services.AddTransient<IProductInStockQueryService, ProductInStockQueryService>();

// Event Handlers
builder.Services.AddMediatR(Assembly.Load("Catalog.Service.EventHandlers"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddSyslog(
                    builder.Configuration.GetValue<string>("Papertrail:host"),
                    builder.Configuration.GetValue<int>("Papertrail:port"));

app.UseAuthorization();

app.UseRouting();

app.UseEndpoints(endpoints => { 
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
});

app.Run();
