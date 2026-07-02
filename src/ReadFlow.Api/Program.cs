using ReadFlow.Application.Interfaces;
using ReadFlow.Application.Services;
using ReadFlow.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// OpenAPI / Swagger docs
builder.Services.AddOpenApi();

// Dependency Injection
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();