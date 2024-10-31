using SharedKernel.Events;

namespace Domain.GameLogs.DomainEvents;

public sealed class GameLogCreatedDomainEvent(String message)
    : DomainEvent(nameof(GameLogCreatedDomainEvent))
{
    public String Message { get; set; } = message;
}
