using SharedKernel;

namespace Domain.GameLogs.DomainEvents;

public sealed record GameLogCompletedDomainEvent(Guid GameLogId) : IDomainEvent;
