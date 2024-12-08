using Microsoft.Extensions.Options;
using SimpleCrudApi.Models;
using SimpleCrudApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register DatabaseSettings to load configuration from appsettings.json
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

// Register ItemService as a singleton
builder.Services.AddSingleton<ItemService>();

// Optional: Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Update this if the frontend origin changes
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable the defined CORS policy
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

// Map controllers to handle API requests
app.MapControllers();

app.Run();
