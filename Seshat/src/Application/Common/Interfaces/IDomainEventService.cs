using Seshat.Domain.Common;
using System.Threading.Tasks;

namespace Seshat.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}