using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using SharedKernel;
using SharedKernel.Commands;

namespace Application.GameLogs.Commands.IncreaseReviewLike;

internal sealed class IncreaseGameLogReviewLikeCountCommandHandler(IGameLogRepository repository)
    : ICommandHandler<IncreaseGameLogReviewLikeCountCommand, GameLogDto>
{
    public async Task<Result<GameLogDto>> Handle(IncreaseGameLogReviewLikeCountCommand command, CancellationToken cancellationToken)
    {
        GameLog gameLog = await repository.GetGameLogByIdAsync(command.GameLogId, cancellationToken);
        
        if (gameLog is null)
            return Result.Failure<GameLogDto>(GameLogErrors.NotFound(command.GameLogId));
        
        gameLog.Raise(new GameLogIncreaseLikeDomainEvent($"GameLog with id {gameLog.Id} like count has been increased"));
        gameLog.LikeReview();
        
        repository.Update(gameLog);
        await repository.CommitAsync(cancellationToken);

        var gameLogDto = new GameLogDto(gameLog);
        return gameLogDto;
    }
}
