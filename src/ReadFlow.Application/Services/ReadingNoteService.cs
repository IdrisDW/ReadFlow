using ReadFlow.Application.DTOs;
using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Entities;

namespace ReadFlow.Application.Services;

public class ReadingNoteService : IReadingNoteService
{
    private readonly IBookRepository _bookRepository;
    private readonly IReadingNoteRepository _readingNoteRepository;

    public ReadingNoteService(
        IBookRepository bookRepository,
        IReadingNoteRepository readingNoteRepository)
    {
        _bookRepository = bookRepository;
        _readingNoteRepository = readingNoteRepository;
    }

    public async Task<List<ReadingNoteDto>?> GetByBookIdAsync(int bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);

        if (book == null || !book.IsActive)
        {
            return null;
        }

        var notes = await _readingNoteRepository.GetByBookIdAsync(bookId);

        return notes.Select(ToDto).ToList();
    }

    public async Task<ReadingNoteDto?> CreateAsync(int bookId, string content)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);

        if (book == null || !book.IsActive)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Note content is required.");
        }

        var note = new ReadingNote
        {
            BookId = bookId,
            Content = content,
            CreatedAt = DateTime.UtcNow
        };

        await _readingNoteRepository.AddAsync(note);
        await _readingNoteRepository.SaveChangesAsync();

        return ToDto(note);
    }

    private static ReadingNoteDto ToDto(ReadingNote note)
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
