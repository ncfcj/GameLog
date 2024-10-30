using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.GameLogs;
using Domain.GameLogs.DomainEvents;
using SharedKernel;

namespace Application.GameLogs.Commands.Create;

internal sealed class CreateGameLogCommandHandler(IApplicationDbContext context) 
    : ICommandHandler<CreateGameLogCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateGameLogCommand command, CancellationToken cancellationToken)
    {
        var gameLogBuilder = new GameLogBuilder(command.UserId);

        GameLog gameLog = gameLogBuilder
            .WithBasicInformation(command.GameName, command.Platform, command.Genres)
            .WithStatus(command.Status)
            .WithDates(command.StartDate, command.EndDate)
            .WithReviewAndRating(command.Review, command.Rating)
            .Build();
        
        gameLog.Raise(new GameLogCreatedDomainEvent(gameLog.Id));
        
        context.GameLogs.Add(gameLog);

        await context.SaveChangesAsync(cancellationToken);

        return gameLog.Id;
    }
}
