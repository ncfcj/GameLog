using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using Domain.GameLogs.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.GameLogs.Commands.Complete;

internal sealed class CompleteGameLogCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CompleteGameLogCommand, bool>
{
    public async Task<Result<bool>> Handle(CompleteGameLogCommand command, CancellationToken cancellationToken)
    {
        GameLog gameLog = await context.GameLogs.FirstOrDefaultAsync(x => x.Id == command.GameLogId, cancellationToken);

        if (gameLog is null)
        {
            return Result.Failure<bool>(GameLogErrors.NotFound(command.GameLogId));
        }

        if (gameLog.LogStatus == LogStatus.Complete)
        {
            return Result.Failure<bool>(GameLogErrors.GameIsAlreadyComplete(command.GameLogId));
        }

        if (gameLog.LogStatus != LogStatus.Playing)
        {
            return Result.Failure<bool>(GameLogErrors.GameWasNotBeingPlayed(command.GameLogId));
        }
        
        gameLog.Raise(new GameLogCompletedDomainEvent(gameLog.Id));
        
        gameLog.Complete(command.Review, command.Rating);

        context.GameLogs.Update(gameLog);
        
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
