using ReadFlow.Domain.Enums;

namespace ReadFlow.Domain.Entities;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public int Year { get; set; }

    public ReadingStatus Status { get; set; } = ReadingStatus.WantToRead;

    public bool IsActive { get; set; } = true;

    public List<ReadingNote> ReadingNotes { get; set; } = new();
}