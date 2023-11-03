using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TraderApi.Data;
using TraderApi.Controllers;
using TraderApi.Models;
using Microsoft.OpenApi.Models;
using TraderApi.Filters;
using TraderApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TraderApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TraderApiContext") ?? throw new InvalidOperationException("Connection string 'TraderApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "Trader API", Version = "V1" });
    config.OperationFilter<HeaderFilter>();
});
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), applicationBuilder =>
{
    applicationBuilder.UseMiddleware<ApiKeyMiddleware>();
});
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
