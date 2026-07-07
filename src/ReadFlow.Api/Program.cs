using Microsoft.EntityFrameworkCore;
using ReadFlow.Application.Interfaces;
using ReadFlow.Application.Services;
using ReadFlow.Infrastructure.Data;
using ReadFlow.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReadingNoteService, ReadingNoteService>();
builder.Services.AddScoped<IBookRepository, EfBookRepository>();
builder.Services.AddScoped<IReadingNoteRepository, EfReadingNoteRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
