using ReadFlow.Application.DTOs;

namespace ReadFlow.Application.Interfaces;

public interface IReadingNoteService
{
    Task<List<ReadingNoteDto>?> GetByBookIdAsync(int bookId);
    Task<ReadingNoteDto?> CreateAsync(int bookId, string content);
}
