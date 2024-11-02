using Application.GameLogs.Commands.Create;
using Domain.GameLogs.Entities;
using MediatR;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.GameLogs;

internal sealed class CreateGameLog : IEndpoint
{
    public sealed record Request(
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
        app.MapPost($"GameLogs", async (Request request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CreateGameLogCommand(
                    request.GameName,
                    request.Platform,
                    request.Genres,
                    request.Status,
                    request.Review,
                    request.Rating,
                    request.StartDate,
                    request.EndDate,
                    request.UserId);

                var result = await sender.Send(command, cancellationToken);
                
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(Permissions.GameLogAccess)
            .WithTags(Tags.GameLogs);
    }
}
