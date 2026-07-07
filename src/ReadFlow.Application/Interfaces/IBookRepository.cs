using ReadFlow.Domain.Entities;

namespace ReadFlow.Application.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();

    Task<Book?> GetByIdAsync(int id);

    Task AddAsync(Book book);

    Task AddNoteAsync(ReadingNote note);

    Task<List<ReadingNote>> GetNotesAsync(int bookId);

    Task AddStatusHistoryAsync(ReadingStatusHistory history);

    Task<List<ReadingStatusHistory>> GetStatusHistoryAsync(int bookId);

    Task SaveChangesAsync();
}
