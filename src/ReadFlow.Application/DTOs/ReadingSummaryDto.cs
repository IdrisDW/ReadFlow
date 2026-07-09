namespace ReadFlow.Application.DTOs;

public class ReadingSummaryDto
{
    public int TotalBooks { get; set; }

    public int CurrentlyReading { get; set; }

    public int FinishedBooks { get; set; }

    public int WantToReadBooks { get; set; }

    public int DnfBooks { get; set; }

    public List<GenreCountDto> BooksByGenre { get; set; } = new();
}