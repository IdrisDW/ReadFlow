namespace ReadFlow.Application.DTOs;

public class ReadingStatusHistoryDto
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public string OldStatus { get; set; } = string.Empty;

    public string NewStatus { get; set; } = string.Empty;

    public DateTime ChangedAt { get; set; }
}
