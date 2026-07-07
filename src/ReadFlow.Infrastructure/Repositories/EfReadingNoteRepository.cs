using Microsoft.EntityFrameworkCore;
using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Entities;
using ReadFlow.Infrastructure.Data;

namespace ReadFlow.Infrastructure.Repositories;

public class EfReadingNoteRepository : IReadingNoteRepository
{
    private readonly AppDbContext _context;

    public EfReadingNoteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReadingNote>> GetByBookIdAsync(int bookId)
    {
        return await _context.ReadingNotes
            .Where(n => n.BookId == bookId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(ReadingNote note)
    {
        await _context.ReadingNotes.AddAsync(note);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
