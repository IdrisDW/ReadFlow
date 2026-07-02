using ReadFlow.Application.DTOs;
using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Entities;

namespace ReadFlow.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public IEnumerable<BookDto> GetAll()
    {
        return _bookRepository
            .GetAll()
            .Where(book => book.IsActive)
            .Select(book => ToDto(book))
            .ToList();
    }

    public BookDto? GetById(int id)
    {
        return _bookRepository
            .GetAll()
            .Where(book => book.IsActive)
            .Select(book => ToDto(book))
            .FirstOrDefault(book => book.Id == id);
    }

    private static BookDto ToDto(Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Year = book.Year
        };
    }
}