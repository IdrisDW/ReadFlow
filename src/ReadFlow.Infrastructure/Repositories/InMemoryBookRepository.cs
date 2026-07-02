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
            AuthorName = "Franz Kafka",
            Genre = "Fiction",
            Isbn = "9780553213690",
            Year = 1915,
            IsActive = true
        },
        new Book
        {
            Id = 2,
            Title = "Aura",
            AuthorName = "Carlos Fuentes",
            Genre = "Fiction",
            Isbn = "9786073131417",
            Year = 1962,
            IsActive = true
        },
        new Book
        {
            Id = 3,
            Title = "The Stranger",
            AuthorName = "Albert Camus",
            Genre = "Fiction",
            Isbn = "9780679720201",
            Year = 1942,
            IsActive = true
        }
    };

    public List<Book> GetAll()
    {
        return _books
            .Where(book => book.IsActive)
            .ToList();
    }

    public Book? GetById(int id)
    {
        return _books
            .FirstOrDefault(book => book.Id == id && book.IsActive);
    }

    public Book Create(Book book)
    {
        book.Id = _books.Any()
            ? _books.Max(book => book.Id) + 1
            : 1;

        book.IsActive = true;

        _books.Add(book);

        return book;
    }

    public bool ExistsByIsbn(string isbn)
    {
        return _books.Any(book =>
            book.IsActive &&
            book.Isbn != null &&
            book.Isbn == isbn);
    }
}
