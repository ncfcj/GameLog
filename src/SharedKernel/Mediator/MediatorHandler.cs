using Elastic.Apm;
using Elastic.Apm.Api;
using Hangfire;
using MediatR;
using SharedKernel.Commands;
using SharedKernel.Events;
using SharedKernel.Handlers;
using SharedKernel.Queries;

namespace SharedKernel.Mediator;

public class MediatorHandler(IMediator mediator) : IMediatorHandler
{
    private readonly IMediator _mediator = mediator;

    public async Task PublishNotificationAsync<T>(T notification) where T : INotification
    {
        await _mediator.Publish(notification);
    }

    public async Task<Result<T>> SendCommandAsync<T>(ICommand<T> command)
    {
        return await _mediator.Send(command);
    }

    public async Task<Result<T>> SendQueryAsync<T>(IQuery<T> query)
    {
        return await _mediator.Send(query);
    }

    public void RunDomainEventAsync<T>(T domainEvent) where T : DomainEvent
    {
        string? distributedTracingData = Agent.Tracer.CurrentTransaction?.OutgoingDistributedTracingData?.SerializeToString();

        BackgroundJobClient client = new ();

        if (domainEvent?.DelayToStartInSeconds > 0)
            client.Schedule(() => PublishDomainEventAsync(domainEvent, distributedTracingData), TimeSpan.FromSeconds(domainEvent.DelayToStartInSeconds));

    }

    public async Task PublishDomainEventAsync<T>(T domainEvent, string? distributedTraceId = null) where T : DomainEvent
    {
        ITransaction? transaction = distributedTraceId != null
            ? Agent.Tracer.StartTransaction(domainEvent.EventName, "DomainEvent", DistributedTracingData.TryDeserializeFromString(distributedTraceId))
            : Agent.Tracer.CurrentTransaction;

        await transaction.CaptureSpan($"Domain Event {domainEvent.EventName}", ApiConstants.TypeMessaging,
            async (span) =>
            {
                await PublishNotificationAsync(domainEvent);

                span.End();
            },
            ApiConstants.ActionExec);

        if (distributedTraceId != null)
            transaction.End();
    }
}
