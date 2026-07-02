using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Entities;

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
            IsActive = true
        },
        new Book
        {
            Id = 2,
            Title = "Aura",
            Author = "Carlos Fuentes",
            Year = 1962,
            IsActive = true
        },
        new Book
        {
            Id = 3,
            Title = "Ficciones",
            Author = "Jorge Luis Borges",
            Year = 1944,
            IsActive = true
        },
        new Book
        {
            Id = 4,
            Title = "Deleted Example",
            Author = "Unknown",
            Year = 2000,
            IsActive = false
        }
    };

    public IEnumerable<Book> GetAll()
    {
        return _books;
    }
}