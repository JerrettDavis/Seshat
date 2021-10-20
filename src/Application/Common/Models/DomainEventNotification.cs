using Seshat.Domain.Common;
using MediatR;

namespace Seshat.Application.Common.Models
{
    public class DomainEventNotification<TDomainEvent> : INotification 
        where TDomainEvent : DomainEvent
    {
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }

        public TDomainEvent DomainEvent { get; }
    }
}