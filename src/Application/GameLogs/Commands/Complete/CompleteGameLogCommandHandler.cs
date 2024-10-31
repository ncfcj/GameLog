using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using Domain.GameLogs.Entities;
using SharedKernel;
using SharedKernel.Commands;

namespace Application.GameLogs.Commands.Complete;

internal sealed class CompleteGameLogCommandHandler(IGameLogRepository repository)
    : ICommandHandler<CompleteGameLogCommand, GameLogDto>
{
    public async Task<Result<GameLogDto>> Handle(CompleteGameLogCommand command, CancellationToken cancellationToken)
    {
        GameLog gameLog = await repository.GetGameLogByIdAsync(command.GameLogId, cancellationToken);

        if (gameLog is null)
            return Result.Failure<GameLogDto>(GameLogErrors.NotFound(command.GameLogId));

        if (gameLog.LogStatus == LogStatus.Complete)
            return Result.Failure<GameLogDto>(GameLogErrors.GameIsAlreadyComplete(command.GameLogId));

        if (gameLog.LogStatus != LogStatus.Playing)
            return Result.Failure<GameLogDto>(GameLogErrors.GameWasNotBeingPlayed(command.GameLogId));
        
        gameLog.Raise(new GameLogCompletedDomainEvent($"GameLog with the Id {gameLog.Id} was completed"));
        
        gameLog.Complete(command.Review, command.Rating);

        repository.Update(gameLog);
        await repository.CommitAsync(cancellationToken);

        var gameLogDto = new GameLogDto(gameLog);

        return gameLogDto;
    }
}
