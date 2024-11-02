using Application.GameLogs.Commands.Complete;
using Domain.GameLogs.Entities;
using MediatR;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.GameLogs;

internal sealed class CompleteGameLog : IEndpoint
{
    public sealed record Request(Guid GameLogId, Rating Rating, string? Review);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch($"GameLogs/ChangeStatus/Complete", async (Request request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new CompleteGameLogCommand(
                    request.GameLogId,
                    request.Rating,
                    request.Review);

                var result = await sender.Send(command, cancellationToken);
                
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(Permissions.GameLogAccess)
            .WithTags(Tags.GameLogs);
    }
}
