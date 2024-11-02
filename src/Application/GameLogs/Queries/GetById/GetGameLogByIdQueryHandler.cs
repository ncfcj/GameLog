using Domain.GameLogs;
using SharedKernel;
using SharedKernel.Queries;

namespace Application.GameLogs.Queries.GetById;

internal sealed class GetGameLogByIdQueryHandler(IGameLogRepository repository) 
    : IQueryHandler<GetGameLogByIdQuery, GameLogResponse>
{
    public async Task<Result<GameLogResponse>> Handle(GetGameLogByIdQuery query, CancellationToken cancellationToken)
    {
        GameLog gameLog = await repository.GetGameLogByIdReadOnlyAsync(query.GameLogId!.Value, cancellationToken);

        if (gameLog is null)
            return Result.Failure<GameLogResponse>(GameLogErrors.NotFound(query.GameLogId!.Value));

        var response = new GameLogResponse(gameLog);
        return response;
    }
}
