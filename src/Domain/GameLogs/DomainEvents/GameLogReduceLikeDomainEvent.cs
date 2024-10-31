using SharedKernel.Events;

namespace Domain.GameLogs.DomainEvents;

public sealed class GameLogReduceLikeDomainEvent(String message)
    : DomainEvent(nameof(GameLogCompletedDomainEvent))
{
    public String Message { get; set; } = message;
}

