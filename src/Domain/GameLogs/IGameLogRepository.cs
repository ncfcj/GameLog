using Gridify;
using SharedKernel;

namespace Domain.GameLogs;

public interface IGameLogRepository : IUnitOfWork
{
    Task<GameLog?> GetGameLogByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<GameLog?> GetGameLogByIdReadOnlyAsync(Guid id, CancellationToken cancellationToken);
    QueryablePaging<GameLog> GetGameLogQueryablePagingByQuery(GridifyQuery query);
    void Update(GameLog gameLog);
    Task AddAsync(GameLog gameLog, CancellationToken cancellationToken);
    void Delete(GameLog gameLog);
}
