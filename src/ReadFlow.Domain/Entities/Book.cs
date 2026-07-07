using ReadFlow.Domain.Enums;

namespace ReadFlow.Domain.Entities;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public ReadingStatus Status { get; set; } = ReadingStatus.WantToRead;

    public int? Rating { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<ReadingNote> ReadingNotes { get; set; } = new List<ReadingNote>();

    public ICollection<ReadingStatusHistory> StatusHistory { get; set; } = new List<ReadingStatusHistory>();
}
