using Domain.GameLogs.Entities;
using SharedKernel.Commands;

namespace Application.GameLogs.Commands.ChangeStatus;

public sealed record ChangeGameLogStatusCommand(Guid GameLogId, LogStatus Status)
    : ICommand<GameLogDto>;
