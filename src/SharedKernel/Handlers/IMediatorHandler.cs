using MediatR;
using SharedKernel.Commands;
using SharedKernel.Events;
using SharedKernel.Queries;

namespace SharedKernel.Handlers;

public interface IMediatorHandler
{
    Task PublishNotificationAsync<T>(T notification) where T : INotification;
    Task<Result<T>> SendQueryAsync<T>(IQuery<T> query);
    Task<Result<T>> SendCommandAsync<T>(ICommand<T> command);
    void RunDomainEventAsync<T>(T domainEvent) where T : DomainEvent;
    Task PublishDomainEventAsync<T>(T domainEvent, string? distributedTraceId = null) where T : DomainEvent;
}
