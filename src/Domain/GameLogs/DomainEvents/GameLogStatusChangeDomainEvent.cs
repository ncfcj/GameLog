using SharedKernel;

namespace Domain.GameLogs.DomainEvents;

public record GameLogStatusChangeDomainEvent(Guid GameLogId) : IDomainEvent;
