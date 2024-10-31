using SharedKernel.Events;

namespace Domain.GameLogs.DomainEvents;

public sealed class GameLogStatusChangeDomainEvent(String message)
    : DomainEvent(nameof(GameLogStatusChangeDomainEvent))
{
    public String Message { get; set; } = message;
}

