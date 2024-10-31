namespace Application.GameLogs.Queries.GetById;

public sealed record GetGameLogByIdQuery(Guid? GameLogId) : IQuery<GameLogResponse>;
