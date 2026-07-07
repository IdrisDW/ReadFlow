using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Entities;
using ReadFlow.Domain.Enums;

namespace ReadFlow.Infrastructure.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = new()
    {
        new Book
        {
            Id = 1,
            Title = "The Metamorphosis",
            Author = "Franz Kafka",
            Year = 1915,
            Status = ReadingStatus.Finished,
            IsActive = true
        },
        new Book
        {
            Id = 2,
            Title = "The Stranger",
            Author = "Albert Camus",
            Year = 1942,
            Status = ReadingStatus.Reading,
            IsActive = true
        },
        new Book
        {
            Id = 3,
            Title = "Chronicle of a Death Foretold",
            Author = "Gabriel García Márquez",
            Year = 1981,
            Status = ReadingStatus.WantToRead,
            IsActive = true
        }
    };

    public Task<List<Book>> GetAllAsync()
    {
        return Task.FromResult(_books);
    }

    public Task<Book?> GetByIdAsync(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(book);
    }

    public Task AddAsync(Book book)
    {
        book.Id = _books.Max(b => b.Id) + 1;
        _books.Add(book);

        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}