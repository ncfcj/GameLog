using SharedKernel;

namespace Domain.GameLogs.DomainEvents;

public sealed record GameLogReduceLikeDomainEvent(Guid GameLogId) : IDomainEvent;
