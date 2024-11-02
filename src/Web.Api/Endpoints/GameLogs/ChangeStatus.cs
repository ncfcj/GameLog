using Application.GameLogs.Commands.ChangeStatus;
using Domain.GameLogs.Entities;
using MediatR;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.GameLogs;

internal sealed class ChangeStatus : IEndpoint
{
    public sealed record Request(Guid GameLogId, LogStatus GameLogStatus);
    
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch($"GameLogs/ChangeStatus", async (Request request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new ChangeGameLogStatusCommand(
                    request.GameLogId,
                    request.GameLogStatus);

                var result = await sender.Send(command, cancellationToken);
                
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(Permissions.GameLogAccess)
            .WithTags(Tags.GameLogs);
    }
}
