using Application.Abstractions.Messaging;
using Domain.GameLogs.Entities;

namespace Application.GameLogs.Commands.ChangeStatus;

public sealed record ChangeGameLogStatusCommand(Guid GameLogId, LogStatus Status) 
    : ICommand<bool>;
