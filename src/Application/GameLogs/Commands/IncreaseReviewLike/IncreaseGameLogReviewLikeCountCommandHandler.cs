using Application.Abstractions.Data;
using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Commands;

namespace Application.GameLogs.Commands.IncreaseReviewLike;

internal sealed class IncreaseGameLogReviewLikeCountCommandHandler(IApplicationDbContext context)
    : ICommandHandler<IncreaseGameLogReviewLikeCountCommand, bool>
{
    public async Task<Result<bool>> Handle(IncreaseGameLogReviewLikeCountCommand command, CancellationToken cancellationToken)
    {
        GameLog gameLog = await context.GameLogs.FirstOrDefaultAsync(x => x.Id == command.GameLogId, cancellationToken);
        
        if (gameLog is null)
        {
            return Result.Failure<bool>(GameLogErrors.NotFound(command.GameLogId));
        }
        
        gameLog.Raise(new GameLogIncreaseLikeDomainEvent(gameLog.Id));
        
        gameLog.LikeReview();
        
        context.GameLogs.Update(gameLog);

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
