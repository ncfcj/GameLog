using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using SharedKernel;
using SharedKernel.Commands;

namespace Application.GameLogs.Commands.ReduceReviewLike;

internal sealed class ReduceGameLogReviewLikeCommandHandler(IGameLogRepository repository)
    : ICommandHandler<ReduceGameLogReviewLikeCommand, GameLogDto>
{
    public async Task<Result<GameLogDto>> Handle(ReduceGameLogReviewLikeCommand command, CancellationToken cancellationToken)
    {
        GameLog gameLog = await repository.GetGameLogByIdAsync(command.GameLogId, cancellationToken);
        
        if (gameLog is null)
            return Result.Failure<GameLogDto>(GameLogErrors.NotFound(command.GameLogId));
        
        gameLog.Raise(new GameLogReduceLikeDomainEvent($"GameLog with Id {gameLog.Id} like count has been decreased"));
        
        gameLog.RemoveLikeFromReview();
        
        repository.Update(gameLog);
        
        await repository.CommitAsync(cancellationToken);

        var gameLogDto = new GameLogDto(gameLog);
        
        return gameLogDto;
    }
}
