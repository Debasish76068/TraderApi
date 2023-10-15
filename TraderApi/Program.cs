using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TraderApi.Data;
using TraderApi.Controllers;
using TraderApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TraderApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TraderApiContext") ?? throw new InvalidOperationException("Connection string 'TraderApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

//app.MapPurchaserEndpoints();

app.Run();
