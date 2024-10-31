using SharedKernel.Commands;
using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using Domain.GameLogs.Entities;
using SharedKernel;

namespace Application.GameLogs.Commands.ChangeStatus;

internal sealed class ChangeGameLogStatusCommandHandler(IGameLogRepository repository) 
    : ICommandHandler<ChangeGameLogStatusCommand, GameLogDto>
{
    public async Task<Result<GameLogDto>> Handle(ChangeGameLogStatusCommand command, CancellationToken cancellationToken)
    {
        GameLog gameLog = await repository.GetGameLogByIdAsync(command.GameLogId, cancellationToken);

        if (gameLog is null)
            return Result.Failure<GameLogDto>(GameLogErrors.NotFound(command.GameLogId));
        
        if (command.Status == LogStatus.Complete)
            return Result.Failure<GameLogDto>(GameLogErrors.CannotCompleteTheGameInThisRoute(command.GameLogId));
        
        gameLog.Raise(new GameLogStatusChangeDomainEvent($"GameLog with Id {gameLog.Id} has been changed to {command.Status}"));
        
        gameLog.ChangeStatus(command.Status);
        
        repository.Update(gameLog);
        await repository.CommitAsync(cancellationToken);

        var gameLogDto = new GameLogDto(gameLog);
        
        return gameLogDto;
    }
}
