using ReadFlow.Domain.Entities;

namespace ReadFlow.Application.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();

    Task<Book?> GetByIdAsync(int id);

    Task AddAsync(Book book);

    Task SaveChangesAsync();
}