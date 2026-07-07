using ReadFlow.Application.DTOs;

namespace ReadFlow.Application.Interfaces;

public interface IBookService
{
    Task<List<BookDto>> GetAllAsync();
    Task<BookDto?> GetByIdAsync(int id);
    Task<BookDto> CreateAsync(string title, string author, int year);
    Task<BookDto?> UpdateStatusAsync(int id, string status);
}
