using SharedKernel;
using SharedKernel.Events;

namespace Domain.GameLogs.DomainEvents;

public sealed class GameLogDeletedDomainEvent(String message)
    : DomainEvent(nameof(GameLogDeletedDomainEvent))
{
    public String Message { get; set; } = message;
}
