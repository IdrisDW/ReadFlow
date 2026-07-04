using ReadFlow.Domain.Enums;

namespace ReadFlow.Application.Interfaces;

public interface IReadingStatusTransitionValidator
{
    bool CanTransition(ReadingStatus currentStatus, ReadingStatus newStatus);
}