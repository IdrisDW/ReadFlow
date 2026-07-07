using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Entities;
using ReadFlow.Domain.Enums;

namespace ReadFlow.Infrastructure.Repositories;

public class InMemoryBookRepository : IBookRepository
{
    private readonly List<Book> _books = new();
    private int _nextBookId = 1;
    private int _nextNoteId = 1;

    public InMemoryBookRepository()
    {
        _books.Add(new Book
        {
            Id = _nextBookId++,
            Title = "The Metamorphosis",
            Author = "Franz Kafka",
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
        });

        _books.Add(new Book
        {
            Id = _nextBookId++,
            Title = "The Stranger",
            Author = "Albert Camus",
            Status = ReadingStatus.Reading,
            Rating = null,
            IsActive = true,
            ReadingNotes = new List<ReadingNote>()
        });
    }

    public Task<List<Book>> GetAllAsync()
    {
        var activeBooks = _books
            .Where(book => book.IsActive)
            .ToList();

        return Task.FromResult(activeBooks);
    }

    public Task<Book?> GetByIdAsync(int id)
    {
        var book = _books
            .FirstOrDefault(book => book.Id == id && book.IsActive);

        return Task.FromResult(book);
    }

    public Task AddAsync(Book book)
    {
        book.Id = _nextBookId++;
        book.IsActive = true;

        foreach (var note in book.ReadingNotes)
        {
            note.Id = _nextNoteId++;
            note.BookId = book.Id;
        }

        _books.Add(book);

        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
    {
        foreach (var book in _books)
        {
            foreach (var note in book.ReadingNotes.Where(note => note.Id == 0))
            {
                note.Id = _nextNoteId++;
                note.BookId = book.Id;
                note.CreatedAt = DateTime.UtcNow;
            }
        }

        return Task.CompletedTask;
    }
}