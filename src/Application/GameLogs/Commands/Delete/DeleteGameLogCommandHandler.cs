using Application.Abstractions.Data;
using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Commands;

namespace Application.GameLogs.Commands.Delete;

internal sealed class DeleteGameLogCommandHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteGameLogCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteGameLogCommand command, CancellationToken cancellationToken)
    {
        GameLog gameLog = await context.GameLogs.FirstOrDefaultAsync(x => x.Id == command.GameLogId, cancellationToken);

        if (gameLog is null)
        {
            return Result.Failure<bool>(GameLogErrors.NotFound(command.GameLogId));
        }
        
        gameLog.Raise(new GameLogDeletedDomainEvent(gameLog.Id));
        
        context.GameLogs.Remove(gameLog);
        
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
