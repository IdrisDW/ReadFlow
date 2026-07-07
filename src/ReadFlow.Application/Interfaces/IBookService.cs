using ReadFlow.Application.DTOs;
using ReadFlow.Domain.Enums;

namespace ReadFlow.Application.Interfaces;

public interface IBookService
{
    Task<List<BookDto>> GetAllAsync();

    Task<BookDto?> GetByIdAsync(int id);

    Task<BookDto> CreateAsync(string title, string author);

    Task<BookDto?> UpdateStatusAsync(int id, ReadingStatus newStatus);

    Task<ReadingNoteDto?> AddNoteAsync(int bookId, string content);

    Task<List<ReadingNoteDto>?> GetNotesAsync(int bookId);

    Task<BookDto?> UpdateRatingAsync(int id, int? rating);
}