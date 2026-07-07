using ReadFlow.Domain.Entities;

namespace ReadFlow.Application.Interfaces;

public interface IReadingNoteRepository
{
    Task<List<ReadingNote>> GetByBookIdAsync(int bookId);
    Task AddAsync(ReadingNote note);
    Task SaveChangesAsync();
}
