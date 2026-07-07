using Microsoft.EntityFrameworkCore;
using ReadFlow.Domain.Entities;
using ReadFlow.Domain.Enums;

namespace ReadFlow.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }

    public DbSet<ReadingNote> ReadingNotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>()
            .Property(b => b.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Book>()
            .HasMany(b => b.ReadingNotes)
            .WithOne(n => n.Book)
            .HasForeignKey(n => n.BookId);
    }
}