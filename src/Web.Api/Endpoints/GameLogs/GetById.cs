using Application.GameLogs.Queries.GetById;
using MediatR;
using Web.Api.Endpoints.Users;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.GameLogs;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("GameLogs/{gameLogId}", async (Guid gameLogId, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetGameLogByIdQuery(gameLogId);

                var result = await sender.Send(query, cancellationToken);
                
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .HasPermission(Permissions.GameLogAccess)
            .WithTags(Tags.GameLogs);
    }
}
