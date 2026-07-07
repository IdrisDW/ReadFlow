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
            .Include(b => b.StatusHistory)
            .Where(b => b.IsActive)
            .OrderBy(b => b.Id)
            .ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books
            .Include(b => b.ReadingNotes)
            .Include(b => b.StatusHistory)
            .FirstOrDefaultAsync(b => b.Id == id && b.IsActive);
    }

    public async Task AddAsync(Book book)
    {
        await _context.Books.AddAsync(book);
    }

    public async Task AddNoteAsync(ReadingNote note)
    {
        await _context.ReadingNotes.AddAsync(note);
    }

    public async Task<List<ReadingNote>> GetNotesAsync(int bookId)
    {
        return await _context.ReadingNotes
            .Where(note => note.BookId == bookId)
            .OrderByDescending(note => note.CreatedAt)
            .ToListAsync();
    }

    public async Task AddStatusHistoryAsync(ReadingStatusHistory history)
    {
        await _context.ReadingStatusHistories.AddAsync(history);
    }

    public async Task<List<ReadingStatusHistory>> GetStatusHistoryAsync(int bookId)
    {
        return await _context.ReadingStatusHistories
            .Where(history => history.BookId == bookId)
            .OrderByDescending(history => history.ChangedAt)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
