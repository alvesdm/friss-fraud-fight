using FightFraud.Domain.Common;
using System.Threading.Tasks;

namespace FightFraud.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}