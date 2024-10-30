using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Domain.GameLogs;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    
    DbSet<GameLog> GameLogs { get; }
    
    DbSet<Friends> Friends { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
