using Microsoft.EntityFrameworkCore;
using ReadFlow.Domain.Entities;

namespace ReadFlow.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();

    public DbSet<ReadingNote> ReadingNotes => Set<ReadingNote>();

    public DbSet<ReadingStatusHistory> ReadingStatusHistories => Set<ReadingStatusHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(b => b.Genre)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(b => b.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(30);

            entity.HasIndex(b => b.Status);

            entity.HasIndex(b => b.Genre);
        });

        modelBuilder.Entity<ReadingNote>(entity =>
        {
            entity.Property(n => n.Content)
                .IsRequired()
                .HasMaxLength(4000);

            entity.Property(n => n.CreatedAt)
                .IsRequired();

            entity.HasOne(n => n.Book)
                .WithMany(b => b.ReadingNotes)
                .HasForeignKey(n => n.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ReadingStatusHistory>(entity =>
        {
            entity.ToTable("ReadingStatusHistory");

            entity.Property(h => h.OldStatus)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(h => h.NewStatus)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(h => h.ChangedAt)
                .IsRequired();

            entity.HasOne(h => h.Book)
                .WithMany(b => b.StatusHistory)
                .HasForeignKey(h => h.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(h => h.BookId);

            entity.HasIndex(h => h.ChangedAt);

            entity.HasIndex(h => new { h.BookId, h.ChangedAt });
        });
    }
}
