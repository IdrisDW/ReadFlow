using ReadFlow.Domain.Enums;

namespace ReadFlow.Application.Requests;

public class UpdateBookStatusRequest
{
    public ReadingStatus Status { get; set; }
}
