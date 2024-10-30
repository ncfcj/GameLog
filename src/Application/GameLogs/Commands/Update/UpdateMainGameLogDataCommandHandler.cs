using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.GameLogs.Commands.Update;

internal sealed class UpdateMainGameLogDataCommandHandler(IApplicationDbContext context)
    : ICommandHandler<UpdateMainGameLogDataCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateMainGameLogDataCommand command, CancellationToken cancellationToken)
    {
        if (command.GameLogId is null)
        {
            return Result.Failure<Guid>(GameLogErrors.GameLogIdIsNecessaryForUpdate);
        }

        GameLog gameLog = await context.GameLogs.FirstOrDefaultAsync(x => x.Id == command.GameLogId, cancellationToken);

        if (gameLog is null)
        {
            return Result.Failure<Guid>(GameLogErrors.NotFound(command.GameLogId.Value));
        }
        
        gameLog.Raise(new GameLogMainDataUpdatedDomainEvent(gameLog.Id));
        
        gameLog.UpdateMainData(command.GameName, command.StartDate, command.EndDate, command.Rating, command.Genres);

        context.GameLogs.Update(gameLog);
        
        await context.SaveChangesAsync(cancellationToken);

        return gameLog.Id;
    }
}
