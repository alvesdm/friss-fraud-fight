using FightFraud.Domain.Common;
using FightFraud.Domain.Entities;

namespace FightFraud.Domain.Events
{
    public class PersonCreatedEvent : DomainEvent
    {
        public PersonCreatedEvent(Person entity)
        {
            Person = entity;
        }

        public Person Person { get; }
    }
}