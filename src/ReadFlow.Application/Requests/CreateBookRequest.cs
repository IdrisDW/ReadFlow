using ReadFlow.Domain.Enums;

namespace ReadFlow.Application.Requests;

public class CreateBookRequest
{
    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public ReadingStatus Status { get; set; } = ReadingStatus.WantToRead;
}
