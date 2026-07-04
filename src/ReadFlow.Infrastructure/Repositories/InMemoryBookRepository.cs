using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Entities;

namespace ReadFlow.Infrastructure.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = new();
    private int _nextId = 1;

    public List<Book> GetAll()
    {
        return _books;
    }

    public Book? GetById(int id)
    {
        return _books.FirstOrDefault(book => book.Id == id);
    }

    public Book Create(Book book)
    {
        book.Id = _nextId++;
        _books.Add(book);

        return book;
    }

    public Book? Update(Book book)
    {
        var existingBook = _books.FirstOrDefault(existing => existing.Id == book.Id);

        if (existingBook is null)
        {
            return null;
        }

        existingBook.Title = book.Title;
        existingBook.AuthorName = book.AuthorName;
        existingBook.Year = book.Year;
        existingBook.Status = book.Status;

        return existingBook;
    }
}