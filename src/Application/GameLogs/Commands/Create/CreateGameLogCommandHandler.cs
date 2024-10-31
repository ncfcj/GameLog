using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using SharedKernel;
using SharedKernel.Commands;

namespace Application.GameLogs.Commands.Create;

internal sealed class CreateGameLogCommandHandler(IGameLogRepository repository) 
    : ICommandHandler<CreateGameLogCommand, GameLogDto>
{
    public async Task<Result<GameLogDto>> Handle(CreateGameLogCommand command, CancellationToken cancellationToken)
    {
        var gameLogBuilder = new GameLogBuilder(command.UserId);

        GameLog gameLog = gameLogBuilder
            .WithBasicInformation(command.GameName, command.Platform, command.Genres)
            .WithStatus(command.Status)
            .WithDates(command.StartDate, command.EndDate)
            .WithReviewAndRating(command.Review, command.Rating)
            .Build();
        
        gameLog.Raise(new GameLogCreatedDomainEvent($"GameLog with Id {gameLog.Id} has been created."));
        
        await repository.AddAsync(gameLog, cancellationToken);
        await repository.CommitAsync(cancellationToken);

        var gameLogDto = new GameLogDto(gameLog);
        
        return gameLogDto;
    }
}
