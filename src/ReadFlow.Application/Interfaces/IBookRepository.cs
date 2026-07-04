using ReadFlow.Domain.Entities;

namespace ReadFlow.Application.Interfaces;

public interface IBookRepository
{
    List<Book> GetAll();

    Book? GetById(int id);

    Book Create(Book book);

    Book? Update(Book book);
}