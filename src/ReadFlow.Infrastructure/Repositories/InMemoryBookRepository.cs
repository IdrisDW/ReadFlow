using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Entities;
using ReadFlow.Domain.Enums;

namespace ReadFlow.Infrastructure.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = new();
    private readonly List<ReadingStatusHistory> _statusHistory = new();
    private int _nextBookId = 1;
    private int _nextNoteId = 1;
    private int _nextStatusHistoryId = 1;

    public InMemoryBookRepository()
    {
        Book kafka = new()
        {
            Id = _nextBookId++,
            Title = "The Metamorphosis",
            Author = "Franz Kafka",
            Genre = "Fiction",
            Status = ReadingStatus.Finished,
            Rating = 5,
            IsActive = true,
            ReadingNotes = new List<ReadingNote>
            {
                new ReadingNote
                {
                    Id = _nextNoteId++,
                    BookId = 1,
                    Content = "Kafka was easier to read than expected.",
                    CreatedAt = DateTime.UtcNow
                }
            }
        };

        Book camus = new()
        {
            Id = _nextBookId++,
            Title = "The Stranger",
            Author = "Albert Camus",
            Genre = "Fiction",
            Status = ReadingStatus.Reading,
            Rating = null,
            IsActive = true,
            ReadingNotes = new List<ReadingNote>()
        };

        _books.Add(kafka);
        _books.Add(camus);
    }

    public Task<List<Book>> GetAllAsync()
    {
        List<Book> activeBooks = _books
            .Where(book => book.IsActive)
            .ToList();

        return Task.FromResult(activeBooks);
    }

    public Task<Book?> GetByIdAsync(int id)
    {
        Book? book = _books
            .FirstOrDefault(book => book.Id == id && book.IsActive);

        return Task.FromResult(book);
    }

    public Task AddAsync(Book book)
    {
        book.Id = _nextBookId++;
        book.IsActive = true;

        foreach (ReadingNote note in book.ReadingNotes)
        {
            note.Id = _nextNoteId++;
            note.BookId = book.Id;
        }

        _books.Add(book);

        return Task.CompletedTask;
    }

    public Task AddNoteAsync(ReadingNote note)
    {
        note.Id = _nextNoteId++;

        Book? book = _books.FirstOrDefault(book => book.Id == note.BookId && book.IsActive);

        if (book is not null)
        {
            book.ReadingNotes.Add(note);
        }

        return Task.CompletedTask;
    }

    public Task<List<ReadingNote>> GetNotesAsync(int bookId)
    {
        Book? book = _books.FirstOrDefault(book => book.Id == bookId && book.IsActive);

        List<ReadingNote> notes = book?.ReadingNotes
            .OrderByDescending(note => note.CreatedAt)
            .ToList() ?? new List<ReadingNote>();

        return Task.FromResult(notes);
    }

    public Task AddStatusHistoryAsync(ReadingStatusHistory history)
    {
        history.Id = _nextStatusHistoryId++;
        _statusHistory.Add(history);

        Book? book = _books.FirstOrDefault(book => book.Id == history.BookId && book.IsActive);

        if (book is not null)
        {
            book.StatusHistory.Add(history);
        }

        return Task.CompletedTask;
    }

    public Task<List<ReadingStatusHistory>> GetStatusHistoryAsync(int bookId)
    {
        List<ReadingStatusHistory> history = _statusHistory
            .Where(item => item.BookId == bookId)
            .OrderByDescending(item => item.ChangedAt)
            .ToList();

        return Task.FromResult(history);
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }

    public Task<int> CountAsync()
    {
        int count = _books.Count(book => book.IsActive);

        return Task.FromResult(count);
    }

    public Task<List<Book>> GetPagedAsync(int pageNumber, int pageSize)
    {
        List<Book> books = _books
            .Where(book => book.IsActive)
            .OrderBy(book => book.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Task.FromResult(books);
    }

}
