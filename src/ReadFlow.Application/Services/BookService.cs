using ReadFlow.Application.DTOs;
using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Entities;
using ReadFlow.Domain.Enums;

namespace ReadFlow.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IReadingStatusTransitionValidator _statusValidator;

    public BookService(
        IBookRepository bookRepository,
        IReadingStatusTransitionValidator statusValidator)
    {
        _bookRepository = bookRepository;
        _statusValidator = statusValidator;
    }

    public async Task<List<BookDto>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllAsync();

        return books
            .Select(MapToDto)
            .ToList();
    }

    public async Task<BookDto?> GetByIdAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book == null)
        {
            return null;
        }

        return MapToDto(book);
    }

    public async Task<BookDto> CreateAsync(string title, string author)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.");
        }

        if (string.IsNullOrWhiteSpace(author))
        {
            throw new ArgumentException("Author is required.");
        }

        var book = new Book
        {
            Title = title.Trim(),
            Author = author.Trim(),
            Status = ReadingStatus.WantToRead,
            Rating = null,
            IsActive = true,
            ReadingNotes = new List<ReadingNote>()
        };

        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();

        return MapToDto(book);
    }

    public async Task<BookDto?> UpdateStatusAsync(int id, ReadingStatus newStatus)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book == null)
        {
            return null;
        }

        var canTransition = _statusValidator.CanTransition(book.Status, newStatus);

        if (!canTransition)
        {
            throw new ArgumentException($"Cannot change status from {book.Status} to {newStatus}.");
        }

        book.Status = newStatus;

        await _bookRepository.SaveChangesAsync();

        return MapToDto(book);
    }

    public async Task<ReadingNoteDto?> AddNoteAsync(int bookId, string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Note content is required.");
        }

        var book = await _bookRepository.GetByIdAsync(bookId);

        if (book == null)
        {
            return null;
        }

        var note = new ReadingNote
        {
            BookId = book.Id,
            Content = content.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        book.ReadingNotes.Add(note);

        await _bookRepository.SaveChangesAsync();

        return MapToNoteDto(note);
    }

    public async Task<List<ReadingNoteDto>?> GetNotesAsync(int bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);

        if (book == null)
        {
            return null;
        }

        return book.ReadingNotes
            .Select(MapToNoteDto)
            .ToList();
    }

    public async Task<BookDto?> UpdateRatingAsync(int id, int? rating)
    {
        if (rating.HasValue && (rating < 1 || rating > 5))
        {
            throw new ArgumentException("Rating must be between 1 and 5.");
        }

        var book = await _bookRepository.GetByIdAsync(id);

        if (book == null)
        {
            return null;
        }

        book.Rating = rating;

        await _bookRepository.SaveChangesAsync();

        return MapToDto(book);
    }

    private static BookDto MapToDto(Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Status = book.Status,
            Rating = book.Rating
        };
    }

    private static ReadingNoteDto MapToNoteDto(ReadingNote note)
    {
        return new ReadingNoteDto
        {
            Id = note.Id,
            BookId = note.BookId,
            Content = note.Content,
            CreatedAt = note.CreatedAt
        };
    }
}