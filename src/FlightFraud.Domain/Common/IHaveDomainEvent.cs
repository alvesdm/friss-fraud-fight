using System.Collections.Generic;

namespace FightFraud.Domain.Common
{
    public interface IHaveDomainEvent
    {
        List<DomainEvent> DomainEvents { get; set; }
    }
}