namespace ReadFlow.Domain.Entities;

public class ReadingNote
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Book? Book { get; set; }
}
