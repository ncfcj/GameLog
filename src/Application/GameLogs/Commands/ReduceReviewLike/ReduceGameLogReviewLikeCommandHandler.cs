using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.GameLogs.Commands.ReduceReviewLike;

internal sealed class ReduceGameLogReviewLikeCommandHandler(IApplicationDbContext context)
    : ICommandHandler<ReduceGameLogReviewLikeCommand, bool>
{
    public async Task<Result<bool>> Handle(ReduceGameLogReviewLikeCommand command, CancellationToken cancellationToken)
    {
        GameLog gameLog = await context.GameLogs.FirstOrDefaultAsync(x => x.Id == command.GameLogId, cancellationToken);
        
        if (gameLog is null)
        {
            return Result.Failure<bool>(GameLogErrors.NotFound(command.GameLogId));
        }
        
        gameLog.Raise(new GameLogReduceLikeDomainEvent(gameLog.Id));
        
        gameLog.RemoveLikeFromReview();
        
        context.GameLogs.Update(gameLog);
        
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
