using ReadFlow.Application.DTOs;

namespace ReadFlow.Application.Services;

public interface IBookService
{
    IEnumerable<BookDto> GetAll();

    BookDto? GetById(int id);
}