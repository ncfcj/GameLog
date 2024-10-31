using SharedKernel.Events;

namespace Domain.GameLogs.DomainEvents;

public sealed class GameLogCompletedDomainEvent(String message) 
    : DomainEvent(nameof(GameLogCompletedDomainEvent))
{
    public String Message { get; set; } = message;
}
