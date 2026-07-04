using ReadFlow.Domain.Enums;

namespace ReadFlow.Api.Requests
{
    public class UpdateBookStatusRequest
    {
        public ReadingStatus NewStatus { get; set; }
    }
}
