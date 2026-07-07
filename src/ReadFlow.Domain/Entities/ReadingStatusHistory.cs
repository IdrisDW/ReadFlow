using ReadFlow.Domain.Enums;

namespace ReadFlow.Domain.Entities;

public class ReadingStatusHistory
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public ReadingStatus OldStatus { get; set; }

    public ReadingStatus NewStatus { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public Book? Book { get; set; }
}
