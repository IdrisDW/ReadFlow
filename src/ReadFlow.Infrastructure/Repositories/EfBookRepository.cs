using Microsoft.EntityFrameworkCore;
using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Entities;
using ReadFlow.Infrastructure.Data;

namespace ReadFlow.Infrastructure.Repositories;

public class EfBookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public EfBookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books
            .Include(b => b.ReadingNotes)
            .Where(b => b.IsActive)
            .OrderBy(b => b.Id)
            .ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books
            .Include(b => b.ReadingNotes)
            .FirstOrDefaultAsync(b => b.Id == id && b.IsActive);
    }

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}