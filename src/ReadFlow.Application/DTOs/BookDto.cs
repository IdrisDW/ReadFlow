using ReadFlow.Domain.Enums;

namespace ReadFlow.Application.DTOs;

public class BookDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public ReadingStatus Status { get; set; }

    public int? Rating { get; set; }
}