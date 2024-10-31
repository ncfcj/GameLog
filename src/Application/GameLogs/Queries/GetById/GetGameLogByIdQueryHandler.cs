using Application.Abstractions.Data;
using Domain.GameLogs;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using SharedKernel.Queries;

namespace Application.GameLogs.Queries.GetById;

internal sealed class GetGameLogByIdQueryHandler(IApplicationDbContext context) 
    : IQueryHandler<GetGameLogByIdQuery, GameLogResponse>
{
    public async Task<Result<GameLogResponse>> Handle(GetGameLogByIdQuery query, CancellationToken cancellationToken)
    {
        GameLog gameLog = await context.GameLogs
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == query.GameLogId, cancellationToken);

        if (gameLog is null)
        {
            return Result.Failure<GameLogResponse>(GameLogErrors.NotFound(query.GameLogId!.Value)) 
        }
        
        return GameLogResponse.FromEntity(gameLog);
    }
}
