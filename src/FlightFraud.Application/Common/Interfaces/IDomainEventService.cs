using FlightFraud.Domain.Common;
using System.Threading.Tasks;

namespace FlightFraud.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}