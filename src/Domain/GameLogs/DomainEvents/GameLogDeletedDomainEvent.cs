using SharedKernel;

namespace Domain.GameLogs.DomainEvents;

public sealed record GameLogDeletedDomainEvent(Guid GameLogId) : IDomainEvent;
