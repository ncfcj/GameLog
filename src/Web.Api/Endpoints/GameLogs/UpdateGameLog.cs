using Application.GameLogs.Commands.Update;
using Domain.GameLogs.Entities;
using MediatR;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.GameLogs;

internal sealed class UpdateGameLog : IEndpoint
{
    public sealed record Request(
        Guid GameLogId,
        string GameName, 
        Platform Platform, 
        List<Genre> Genres, 
        LogStatus? Status, 
        string? Review, 
        Rating? Rating,
        DateTime? StartDate,
        DateTime? EndDate,
        Guid UserId);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut($"GameLogs", async (Request request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new UpdateMainGameLogDataCommand(
                    request.GameLogId,
                    request.GameName,
                    request.StartDate,
                    request.EndDate,
                    request.Rating,
                    request.Genres);

                var result = await sender.Send(command, cancellationToken);
                
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(Permissions.GameLogAccess)
            .WithTags(Tags.GameLogs);
    }
}
