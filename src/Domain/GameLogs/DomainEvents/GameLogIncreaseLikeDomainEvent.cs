using SharedKernel;

namespace Domain.GameLogs.DomainEvents;

public sealed record GameLogIncreaseLikeDomainEvent(Guid GameLogId) : IDomainEvent;
