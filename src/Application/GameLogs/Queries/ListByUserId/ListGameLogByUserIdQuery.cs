using Gridify;
using SharedKernel.Queries;

namespace Application.GameLogs.Queries.ListByUserId;

public sealed record ListGameLogByUserIdQuery(GridifyQuery Query) : IQuery<Paging<GameLogResponse>>;
