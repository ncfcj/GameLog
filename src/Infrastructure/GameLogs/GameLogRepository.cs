using Domain.GameLogs;
using Gridify;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.GameLogs;

public class GameLogRepository(ApplicationDbContext context) : IGameLogRepository
{
    public async Task<GameLog?> GetGameLogByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.GameLogs.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<GameLog?> GetGameLogByIdReadOnlyAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.GameLogs
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public QueryablePaging<GameLog> GetGameLogQueryablePagingByQuery(GridifyQuery query)
    {
        return context.GameLogs
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.User)
            .GridifyQueryable(query);
    }
    
    public void Update(GameLog gameLog)
    {
        context.GameLogs.Update(gameLog);
    }
    
    public async Task AddAsync(GameLog gameLog, CancellationToken cancellationToken)
    {
        await context.GameLogs.AddAsync(gameLog, cancellationToken);
    }

    public void Delete(GameLog gameLog)
    {
        context.GameLogs.Remove(gameLog);
    }
    
    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await context.CommitAsync(cancellationToken);
    }
}
