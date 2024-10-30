using SharedKernel;

namespace Domain.GameLogs.DomainEvents;

public sealed record GameLogMainDataUpdatedDomainEvent(Guid GameLogId) : IDomainEvent;
