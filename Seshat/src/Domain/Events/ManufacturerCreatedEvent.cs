using Seshat.Domain.Common;
using Seshat.Domain.Entities;

namespace Seshat.Domain.Events
{
    public class ManufacturerCreatedEvent : DomainEvent
    {
        public ManufacturerCreatedEvent(Manufacturer manufacturer)
        {
            Manufacturer = manufacturer;
        }
        
        public Manufacturer Manufacturer { get; }
    }
}