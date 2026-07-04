using ReadFlow.Domain.Enums;

namespace ReadFlow.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string? Isbn { get; set; }
    public int? Year { get; set; }
    public bool IsActive { get; set; } = true;

    public ReadingStatus Status { get; set; } = ReadingStatus.WantToRead;


}
