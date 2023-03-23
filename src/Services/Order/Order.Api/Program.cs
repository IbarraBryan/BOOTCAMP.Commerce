using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Order.Persistence.Database;
using System.Reflection;
using Common.Logging;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Order.Service.Queries.Interfaces;
using Order.Service.Queries.Services;
using Order.Service.Proxies;
using Order.Service.Proxies.Catalog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<OrderDbContext>(
options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Order")
    )
);

// Health check
builder.Services.AddHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy())
    .AddDbContextCheck<OrderDbContext>(typeof(OrderDbContext).Name);

// Query Services
builder.Services.AddTransient<IOrderQueryService, OrderQueryService>();

// Event Handlers
builder.Services.AddMediatR(Assembly.Load("Order.Service.EventHandlers"));

// ApiUrls
builder.Services.Configure<ApiUrls>(opts => builder.Configuration.GetSection("ApiUrls").Bind(opts));

// Proxies
builder.Services.AddHttpClient<ICatalogProxy, CatalogProxy>();

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
