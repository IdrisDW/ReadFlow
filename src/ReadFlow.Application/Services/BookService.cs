using ReadFlow.Application.DTOs;
using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Entities;
using ReadFlow.Domain.Enums;

namespace ReadFlow.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<BookDto>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllAsync();

        return books
            .Where(b => b.IsActive)
            .Select(ToDto)
            .ToList();
    }

    public async Task<BookDto?> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book == null || !book.IsActive)
        {
            return null;
        }

        return ToDto(book);
    }

    public async Task<BookDto> CreateAsync(string title, string author, int year)
    {
        var book = new Book
        {
            Title = title,
            Author = author,
            Year = year,
            Status = ReadingStatus.WantToRead,
            IsActive = true
        };

        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();

        return ToDto(book);
    }

    public async Task<BookDto?> UpdateStatusAsync(int id, string status)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book == null || !book.IsActive)
        {
            return null;
        }

        if (!Enum.TryParse<ReadingStatus>(status, ignoreCase: true, out var parsedStatus))
        {
            throw new ArgumentException("Invalid status.");
        }

        book.Status = parsedStatus;
        await _bookRepository.SaveChangesAsync();

        return ToDto(book);
    }

    private static BookDto ToDto(Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Year = book.Year,
            Status = book.Status.ToString()
        };
    }
}
