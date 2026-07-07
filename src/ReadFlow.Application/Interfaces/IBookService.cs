using ReadFlow.Application.DTOs;
using ReadFlow.Application.Requests;

namespace ReadFlow.Application.Interfaces;

public interface IBookService
{
    Task<List<BookDto>> GetAllAsync();

    Task<BookDto?> GetByIdAsync(int id);

    Task<BookDto> CreateAsync(CreateBookRequest request);

    Task<BookDto?> UpdateStatusAsync(int id, UpdateBookStatusRequest request);

    Task<BookDto?> UpdateRatingAsync(int id, UpdateBookRatingRequest request);

    Task<ReadingNoteDto?> AddNoteAsync(int bookId, CreateReadingNoteRequest request);

    Task<List<ReadingNoteDto>?> GetNotesAsync(int bookId);

    Task<List<ReadingStatusHistoryDto>?> GetStatusHistoryAsync(int bookId);
}
