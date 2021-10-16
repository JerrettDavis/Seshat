using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Seshat.Application.Common.Models;
using Seshat.Domain.Events;

namespace Seshat.Application.Manufacturers.EventHandlers
{
    public class ManufacturerCreatedEventHandler :  
        INotificationHandler<DomainEventNotification<ManufacturerCreatedEvent>>
    {
        private readonly ILogger<ManufacturerCreatedEventHandler> _logger;

        public ManufacturerCreatedEventHandler(
            ILogger<ManufacturerCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(
            DomainEventNotification<ManufacturerCreatedEvent> notification,
            CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Seshat Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}