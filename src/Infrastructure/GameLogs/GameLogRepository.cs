using Domain.GameLogs;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.GameLogs;

public class GameLogRepository(ApplicationDbContext context) : IGameLogRepository
{
    
    public async Task<GameLog?> GetGameLogByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.GameLogs.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    
    public void Update(GameLog gameLog)
    {
        context.GameLogs.Update(gameLog);
    }
    
    public async Task AddAsync(GameLog gameLog, CancellationToken cancellationToken)
    {
        await context.GameLogs.AddAsync(gameLog, cancellationToken);
    }
    
    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await context.CommitAsync(cancellationToken);
    }
}
