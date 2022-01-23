using FlightFraud.Domain.Common;
using FlightFraud.Domain.Entities;

namespace FlightFraud.Domain.Events
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