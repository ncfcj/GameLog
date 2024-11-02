using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using SharedKernel;
using SharedKernel.Commands;

namespace Application.GameLogs.Commands.Update;

internal sealed class UpdateMainGameLogDataCommandHandler(IGameLogRepository repository)
    : ICommandHandler<UpdateMainGameLogDataCommand, GameLogDto>
{
    public async Task<Result<GameLogDto>> Handle(UpdateMainGameLogDataCommand command, CancellationToken cancellationToken)
    {
        if (command.GameLogId is null)
            return Result.Failure<GameLogDto>(GameLogErrors.GameLogIdIsNecessaryForUpdate);
        
        GameLog gameLog = await repository.GetGameLogByIdAsync(command.GameLogId.Value, cancellationToken);

        if (gameLog is null)
            return Result.Failure<GameLogDto>(GameLogErrors.NotFound(command.GameLogId.Value));
        
        gameLog.Raise(new GameLogMainDataUpdatedDomainEvent($"GameLog with Id {gameLog.Id} has been updated"));
        
        gameLog.UpdateMainData(command.GameName, command.StartDate, command.EndDate, command.Rating, command.Genres);

        repository.Update(gameLog);
        
        await repository.CommitAsync(cancellationToken);

        var gameLogDto = new GameLogDto(gameLog);
        
        return gameLogDto;
    }
}
