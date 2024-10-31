using SharedKernel.Events;

namespace SharedKernel;

public abstract class Entity
{
    private readonly List<DomainEvent> _domainEvents = [];

    public List<DomainEvent> DomainEvents => [.. _domainEvents];

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
