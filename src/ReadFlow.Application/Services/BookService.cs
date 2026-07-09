using ReadFlow.Application.DTOs;
using ReadFlow.Application.Interfaces;
using ReadFlow.Application.Requests;
using ReadFlow.Domain.Entities;
using ReadFlow.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace ReadFlow.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IReadingStatusTransitionValidator _statusValidator;
    private readonly ILogger<BookService> _logger;

    public BookService(
        IBookRepository bookRepository,
        IReadingStatusTransitionValidator statusValidator,
        ILogger<BookService> logger)
    {
        _bookRepository = bookRepository;
        _statusValidator = statusValidator;
        _logger = logger;
    }

    public async Task<List<BookDto>> GetAllAsync()
    {
        List<Book> books = await _bookRepository.GetAllAsync();

        return books
            .Select(MapToDto)
            .ToList();
    }
    public async Task<ReadingSummaryDto> GetReadingSummaryAsync()
    {
        List<Book> books = await _bookRepository.GetAllAsync();

        return new ReadingSummaryDto
        {
            TotalBooks = books.Count,

            CurrentlyReading = books.Count(book => book.Status == ReadingStatus.Reading),

            FinishedBooks = books.Count(book => book.Status == ReadingStatus.Finished),

            WantToReadBooks = books.Count(book => book.Status == ReadingStatus.WantToRead),

            DnfBooks = books.Count(book => book.Status == ReadingStatus.DNF),

            BooksByGenre = books
                .GroupBy(book => book.Genre)
                .Select(group => new GenreCountDto
                {
                    Genre = group.Key,
                    Count = group.Count()
                })
                .OrderBy(item => item.Genre)
                .ToList()
        };
    }
    public async Task<BookDto?> GetByIdAsync(int id)
    {
        Book? book = await _bookRepository.GetByIdAsync(id);

        if (book is null)
        {
            return null;
        }

        return MapToDto(book);
    }

    public async Task<BookDto> CreateAsync(CreateBookRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException("Title is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Author))
        {
            throw new ArgumentException("Author is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Genre))
        {
            throw new ArgumentException("Genre is required.");
        }

        Book book = new()
        {
            Title = request.Title.Trim(),
            Author = request.Author.Trim(),
            Genre = request.Genre.Trim(),
            Status = request.Status,
            Rating = null,
            IsActive = true
        };

        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();

        return MapToDto(book);
    }

    public async Task<BookDto?> UpdateStatusAsync(int id, UpdateBookStatusRequest request)
    {
        _logger.LogInformation("Trying to update status for book {BookId}", id);

        Book? book = await _bookRepository.GetByIdAsync(id);

        if (book is null)
        {
            _logger.LogWarning("Book {BookId} was not found", id);
            return null;
        }

        ReadingStatus oldStatus = book.Status;
        ReadingStatus newStatus = request.Status;

        _logger.LogInformation(
            "Book {BookId} status transition requested: {OldStatus} -> {NewStatus}",
            id,
            oldStatus,
            newStatus);

        bool canTransition = _statusValidator.CanTransition(oldStatus, newStatus);

        if (!canTransition)
        {
            _logger.LogWarning(
                "Invalid status transition for book {BookId}: {OldStatus} -> {NewStatus}",
                id,
                oldStatus,
                newStatus);

            throw new ArgumentException($"Cannot change status from {oldStatus} to {newStatus}.");
        }

        book.Status = newStatus;

        ReadingStatusHistory history = new()
        {
            BookId = book.Id,
            OldStatus = oldStatus,
            NewStatus = newStatus,
            ChangedAt = DateTime.UtcNow
        };

        await _bookRepository.AddStatusHistoryAsync(history);
        await _bookRepository.SaveChangesAsync();

        _logger.LogInformation(
            "Book {BookId} status updated successfully to {NewStatus}",
            id,
            newStatus);

        return MapToDto(book);
    }
    public async Task<BookDto?> UpdateRatingAsync(int id, UpdateBookRatingRequest request)
    {
        if (request.Rating.HasValue && (request.Rating < 1 || request.Rating > 5))
        {
            throw new ArgumentException("Rating must be between 1 and 5.");
        }

        Book? book = await _bookRepository.GetByIdAsync(id);

        if (book is null)
        {
            return null;
        }

        book.Rating = request.Rating;

        await _bookRepository.SaveChangesAsync();

        return MapToDto(book);
    }

    public async Task<ReadingNoteDto?> AddNoteAsync(int bookId, CreateReadingNoteRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Content))
        {
            throw new ArgumentException("Note content is required.");
        }

        Book? book = await _bookRepository.GetByIdAsync(bookId);

        if (book is null)
        {
            return null;
        }

        ReadingNote note = new()
        {
            BookId = book.Id,
            Content = request.Content.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        await _bookRepository.AddNoteAsync(note);
        await _bookRepository.SaveChangesAsync();

        return MapToNoteDto(note);
    }

    public async Task<List<ReadingNoteDto>?> GetNotesAsync(int bookId)
    {
        Book? book = await _bookRepository.GetByIdAsync(bookId);

        if (book is null)
        {
            return null;
        }

        List<ReadingNote> notes = await _bookRepository.GetNotesAsync(bookId);

        return notes
            .Select(MapToNoteDto)
            .ToList();
    }

    public async Task<List<ReadingStatusHistoryDto>?> GetStatusHistoryAsync(int bookId)
    {
        Book? book = await _bookRepository.GetByIdAsync(bookId);

        if (book is null)
        {
            return null;
        }

        List<ReadingStatusHistory> history = await _bookRepository.GetStatusHistoryAsync(bookId);

        return history
            .Select(MapToStatusHistoryDto)
            .ToList();
    }

    private static BookDto MapToDto(Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Genre = book.Genre,
            Status = book.Status.ToString(),
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

    private static ReadingStatusHistoryDto MapToStatusHistoryDto(ReadingStatusHistory history)
    {
        return new ReadingStatusHistoryDto
        {
            Id = history.Id,
            BookId = history.BookId,
            OldStatus = history.OldStatus.ToString(),
            NewStatus = history.NewStatus.ToString(),
            ChangedAt = history.ChangedAt
        };
    }

    public async Task<PagedResult<BookDto>> GetPagedAsync(BookQueryParameters queryParameters)
    {
        int totalCount = await _bookRepository.CountAsync();

        List<Book> books = await _bookRepository.GetPagedAsync(
            queryParameters.PageNumber,
            queryParameters.PageSize);

        return new PagedResult<BookDto>
        {
            Items = books
                .Select(MapToDto)
                .ToList(),

            PageNumber = queryParameters.PageNumber,

            PageSize = queryParameters.PageSize,

            TotalCount = totalCount
        };
    }
}
