using ReadFlow.Application.Interfaces;
using ReadFlow.Domain.Enums;

namespace ReadFlow.Application.Services;

public class ReadingStatusTransitionValidator : IReadingStatusTransitionValidator
{
    public bool CanTransition(ReadingStatus currentStatus, ReadingStatus newStatus)
    {
        return currentStatus switch
        {
            ReadingStatus.WantToRead => newStatus == ReadingStatus.Reading,

            ReadingStatus.Reading => newStatus == ReadingStatus.Finished
                                    || newStatus == ReadingStatus.Paused
                                    || newStatus == ReadingStatus.DNF,

            ReadingStatus.Paused => newStatus == ReadingStatus.Reading
                                    || newStatus == ReadingStatus.DNF,

            ReadingStatus.Finished => false,

            ReadingStatus.DNF => false,

            _ => false
        };
    }
}