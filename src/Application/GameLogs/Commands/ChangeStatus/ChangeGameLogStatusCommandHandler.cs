using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using Domain.GameLogs.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.GameLogs.Commands.ChangeStatus;

internal sealed class ChangeGameLogStatusCommandHandler(IApplicationDbContext context) 
    : ICommandHandler<ChangeGameLogStatusCommand, bool>
{
    public async Task<Result<bool>> Handle(ChangeGameLogStatusCommand command, CancellationToken cancellationToken)
    {
        GameLog gameLog = await context.GameLogs.FirstOrDefaultAsync(x => x.Id == command.GameLogId, cancellationToken);

        if (gameLog is null)
        {
            return Result.Failure<bool>(GameLogErrors.NotFound(command.GameLogId));
        }
        
        if (command.Status == LogStatus.Complete)
        {
            return Result.Failure<bool>(GameLogErrors.CannotCompleteTheGameInThisRoute(command.GameLogId));
        }
        
        gameLog.Raise(new GameLogStatusChangeDomainEvent(gameLog.Id));
        
        gameLog.ChangeStatus(command.Status);
        
        context.GameLogs.Update(gameLog);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}
