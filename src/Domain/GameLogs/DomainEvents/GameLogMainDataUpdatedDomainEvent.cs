
using SharedKernel.Events;

namespace Domain.GameLogs.DomainEvents;

public sealed class GameLogMainDataUpdatedDomainEvent(String message)
    : DomainEvent(nameof(GameLogCompletedDomainEvent))
{
    public String Message { get; set; } = message;
}
