using OrderManagement.Application.Services;
using OrderManagement.Domain.Repositories;
using OrderManagement.Infrastructure.Data;
using OrderManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddSingleton<DapperContext>();
// Configure Dependency Injection for services and repositories
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Configure Dapper with SQL Server
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
