using Domain.GameLogs.Entities;
using SharedKernel.Commands;

namespace Application.GameLogs.Commands.Update;

public sealed record UpdateMainGameLogDataCommand(
    Guid? GameLogId,
    string? GameName,
    DateTime? StartDate, 
    DateTime? EndDate,
    Rating? Rating,
    List<Genre>? Genres) : ICommand<GameLogDto>;
