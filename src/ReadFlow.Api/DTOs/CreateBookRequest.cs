namespace ReadFlow.Api.DTOs;

public class CreateBookRequest
{
    public string Title { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string? Isbn { get; set; }
    public int? Year { get; set; }
}
