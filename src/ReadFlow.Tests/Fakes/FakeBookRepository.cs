using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Entities;

namespace ReadFlow.Tests.Fakes;

public class FakeBookRepository : IBookRepository
{
    private readonly List<Book> _books = new();
    private readonly List<ReadingNote> _notes = new();
    private readonly List<ReadingStatusHistory> _statusHistory = new();

    private int _nextBookId = 1;
    private int _nextNoteId = 1;
    private int _nextStatusHistoryId = 1;

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
        if (book.Id == 0)
        {
            book.Id = _nextBookId++;
        }

        _books.Add(book);

        return Task.CompletedTask;
    }

    public Task AddNoteAsync(ReadingNote note)
    {
        if (note.Id == 0)
        {
            note.Id = _nextNoteId++;
        }

        _notes.Add(note);

        return Task.CompletedTask;
    }

    public Task<List<ReadingNote>> GetNotesAsync(int bookId)
    {
        var notes = _notes
            .Where(n => n.BookId == bookId)
            .ToList();

        return Task.FromResult(notes);
    }

    public Task AddStatusHistoryAsync(ReadingStatusHistory statusHistory)
    {
        if (statusHistory.Id == 0)
        {
            statusHistory.Id = _nextStatusHistoryId++;
        }

        _statusHistory.Add(statusHistory);

        return Task.CompletedTask;
    }

    public Task<List<ReadingStatusHistory>> GetStatusHistoryAsync(int bookId)
    {
        var history = _statusHistory
            .Where(h => h.BookId == bookId)
            .ToList();

        return Task.FromResult(history);
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }

    public void AddExisting(Book book)
    {
        if (book.Id == 0)
        {
            book.Id = _nextBookId++;
        }

        _books.Add(book);
    }
}