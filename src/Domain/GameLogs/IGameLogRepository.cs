using SharedKernel;

namespace Domain.GameLogs;

public interface IGameLogRepository : IUnitOfWork
{
    Task<GameLog?> GetGameLogByIdAsync(Guid id, CancellationToken cancellationToken);
    void Update(GameLog gameLog);
    Task AddAsync(GameLog gameLog, CancellationToken cancellationToken);
}
