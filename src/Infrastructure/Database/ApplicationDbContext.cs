using Domain.GameLogs;
using Domain.Users;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SharedKernel;
using SharedKernel.Events;
using SharedKernel.Handlers;
using SharedKernel.Logging;

namespace Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, 
    IPublisher publisher, 
    IMediatorHandler mediatorHandler,
    ILoggerManager loggerManager)
    : DbContext(options), IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler = mediatorHandler;
    private readonly ILoggerManager _logger = loggerManager;
    
    public DbSet<User> Users { get; set; }
    public DbSet<GameLog> GameLogs { get; set; }
    public DbSet<Friends> Friends { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);
    }

    
    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        IExecutionStrategy strategy = Database.CreateExecutionStrategy();

        var result = await strategy.ExecuteAsync(async () =>
        {
            await using IDbContextTransaction transaction = await Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var success = await SaveChangesAsync(cancellationToken) > 0;

                if (success)
                {
                    await _mediatorHandler.PublishEvents(this);
                    await transaction.CommitAsync(cancellationToken);
                }
                else
                {
                    await transaction.RollbackAsync(cancellationToken);
                }

                return success;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex);
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        });

        return result;
    }

    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // When should you publish domain events?
        //
        // 1. BEFORE calling SaveChangesAsync
        //     - domain events are part of the same transaction
        //     - immediate consistency
        // 2. AFTER calling SaveChangesAsync
        //     - domain events are a separate transaction
        //     - eventual consistency
        //     - handlers can fail
        
        await PublishDomainEventsAsync();

        int result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                List<DomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }
}
