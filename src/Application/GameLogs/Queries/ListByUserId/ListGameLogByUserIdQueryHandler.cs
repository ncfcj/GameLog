using Domain.GameLogs;
using Gridify;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Queries;

namespace Application.GameLogs.Queries.ListByUserId;

internal sealed class ListGameLogByUserIdQueryHandler(IGameLogRepository repository)
    : IQueryHandler<ListGameLogByUserIdQuery, Paging<GameLogResponse>>
{
    public async Task<Result<Paging<GameLogResponse>>> Handle(ListGameLogByUserIdQuery query, CancellationToken cancellationToken)
    {
        QueryablePaging<GameLog> gameLogQuery = repository.GetGameLogQueryablePagingByQuery(query.Query);

        IQueryable<GameLogResponse> gameLogResponseList = gameLogQuery!.Query
            .Select(x => new GameLogResponse(x));

        return new Paging<GameLogResponse>(
            gameLogQuery.Count, 
            await gameLogResponseList.ToListAsync(cancellationToken));
    }
}
