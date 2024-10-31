using Application.Abstractions.Data;
using Domain.GameLogs;
using Gridify;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Queries;

namespace Application.GameLogs.Queries.ListByUserId;

internal sealed class ListGameLogByUserIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<ListGameLogByUserIdQuery, Paging<GameLogResponse>>
{
    public async Task<Result<Paging<GameLogResponse>>> Handle(ListGameLogByUserIdQuery query, CancellationToken cancellationToken)
    {
        QueryablePaging<GameLog> gameLogQuery = context.GameLogs
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.User)
            .GridifyQueryable(query.Query);

        IQueryable<GameLogResponse> gameLogResponseList = gameLogQuery!.Query
            .Select(x => GameLogResponse.FromEntity(x));

        return new Paging<GameLogResponse>(
            gameLogQuery.Count, 
            await gameLogResponseList.ToListAsync(cancellationToken));
    }
}
