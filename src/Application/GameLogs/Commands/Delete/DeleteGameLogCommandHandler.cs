using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using SharedKernel;
using SharedKernel.Commands;

namespace Application.GameLogs.Commands.Delete;

internal sealed class DeleteGameLogCommandHandler(IGameLogRepository repository)
    : ICommandHandler<DeleteGameLogCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteGameLogCommand command, CancellationToken cancellationToken)
    {
        GameLog gameLog = await repository.GetGameLogByIdAsync(command.GameLogId, cancellationToken);

        if (gameLog is null)
            return Result.Failure<bool>(GameLogErrors.NotFound(command.GameLogId));
        
        gameLog.Raise(new GameLogDeletedDomainEvent($"GameLog with Id {gameLog.Id} has been deleted"));
        
        repository.Delete(gameLog);
        await repository.CommitAsync(cancellationToken);

        var gameLogDto = new GameLogDto(gameLog);

        return true;
    }
}
