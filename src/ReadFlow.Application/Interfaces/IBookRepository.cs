using ReadFlow.Domain.Entities;

namespace ReadFlow.Application.Interfaces;

public interface IBookRepository
{
    IEnumerable<Book> GetAll();
}