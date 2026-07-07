namespace ReadFlow.Application.DTOs;

public class ReadingNoteDto
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}