using ReadFlow.Domain.Enums;

namespace ReadFlow.Api.Requests;

public class UpdateBookStatusRequest
{
    public ReadingStatus Status { get; set; }
}