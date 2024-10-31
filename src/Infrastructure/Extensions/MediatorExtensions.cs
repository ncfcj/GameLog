using Infrastructure.Database;
using SharedKernel;
using SharedKernel.Handlers;

namespace Infrastructure.Extensions;

public static class MediatorExtensions
{
    public static async Task PublishEvents(this IMediatorHandler mediator, ApplicationDbContext context)
    {
        await PublishDomainEvents(mediator, context);
    }

    private static async Task PublishDomainEvents(IMediatorHandler mediator, ApplicationDbContext context)
    {
        var domainEntities = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        var domainAsynchronousEvents = domainEvents
            .Where(x => x.RunAsynchronous)
            .ToList();

        var domainNotAsynchronousEvents = domainEvents
            .Except(domainAsynchronousEvents)
            .ToList();

        foreach (var entity in domainEntities)
            entity.Entity.ClearDomainEvents();

        var tasksDomainNotAsynchronousEvents = domainNotAsynchronousEvents
            .Select(domainEvent => mediator.PublishDomainEventAsync(domainEvent));

        await Task.WhenAll(tasksDomainNotAsynchronousEvents);

        foreach (var domainEvent in domainAsynchronousEvents)
            mediator.RunDomainEventAsync(domainEvent);
    }
}
