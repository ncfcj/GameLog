using Application.GameLogs.Commands.Delete;
using MediatR;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.GameLogs;

internal sealed class DeleteGameLog : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("GameLogs/Delete/{gameLogId}", async (Guid gameLogId, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new DeleteGameLogCommand(gameLogId);

                var result = await sender.Send(command, cancellationToken);
                
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(Permissions.GameLogAccess)
            .WithTags(Tags.GameLogs);
    }
}
