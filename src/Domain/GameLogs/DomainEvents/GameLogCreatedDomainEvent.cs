using SharedKernel;

namespace Domain.GameLogs.DomainEvents;

public sealed record GameLogCreatedDomainEvent(Guid GameLogId) : IDomainEvent;
