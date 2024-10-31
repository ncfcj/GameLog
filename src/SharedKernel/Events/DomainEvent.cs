using MediatR;

namespace SharedKernel.Events;

public class DomainEvent : INotification
{
    public String EventName { get; private set; }
    public DateTime Timestamp { get; private set; }
    public Boolean RunAsynchronous { get; private set; }
    public Int32 DelayToStartInSeconds { get; private set; }

    protected DomainEvent(String eventName = nameof(DomainEvent),
        Boolean runAsynchronous = false,
        Int32 delayToStartInSeconds = 0)
    {
        EventName = eventName;
        Timestamp = DateTime.Now;
        RunAsynchronous = runAsynchronous;
        DelayToStartInSeconds = delayToStartInSeconds;
    }
}
