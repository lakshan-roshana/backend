using Microsoft.Extensions.Options;
using SimpleCrudApi.Models;
using SimpleCrudApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register DatabaseSettings to load configuration from appsettings.json
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

// Register ItemService as a singleton (according to request if needed)
builder.Services.AddSingleton<ItemService>();

// Optional: Add Swagger for API documentation (if needed)
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

// Map controllers (to handle API requests)
app.MapControllers();

app.Run();
