using SharedKernel.Events;

namespace Domain.GameLogs.DomainEvents;

public sealed class GameLogIncreaseLikeDomainEvent(String message)
    : DomainEvent(nameof(GameLogIncreaseLikeDomainEvent))
{
    public String Message { get; set; } = message;
}
