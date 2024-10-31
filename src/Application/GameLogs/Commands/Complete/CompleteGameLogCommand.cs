using Domain.GameLogs.Entities;
using SharedKernel.Commands;

namespace Application.GameLogs.Commands.Complete;

public sealed record CompleteGameLogCommand(
    Guid GameLogId,
    Rating Rating,
    string? Review) 
    : ICommand<GameLogDto>;
