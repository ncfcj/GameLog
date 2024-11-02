using Application.GameLogs.Commands.ReduceReviewLike;
using MediatR;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.GameLogs;

internal sealed class ReduceLike : IEndpoint
{
    public sealed record Request(Guid GameLogId);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("GameLogs/IncreaseLike", async (Request request, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new ReduceGameLogReviewLikeCommand(request.GameLogId);

                var result = await sender.Send(command, cancellationToken);
                
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(Permissions.GameLogAccess)
            .WithTags(Tags.GameLogs);
    }
}
