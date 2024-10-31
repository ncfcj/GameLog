using Domain.GameLogs.Entities;
using SharedKernel.Commands;

namespace Application.GameLogs.Commands.Create;

public sealed record CreateGameLogCommand(
    string GameName, 
    Platform Platform, 
    List<Genre> Genres, 
    LogStatus? Status, 
    string? Review, 
    Rating? Rating, 
    DateTime? StartDate, 
    DateTime? EndDate,
    Guid UserId) : ICommand<GameLogDto>;
