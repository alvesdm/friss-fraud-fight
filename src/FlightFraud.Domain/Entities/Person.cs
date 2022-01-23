using FlightFraud.Domain.Common;
using System;
using System.Collections.Generic;

namespace FlightFraud.Domain.Entities
{
    public class Person : IAmEntity<Guid>, IHasDomainEvent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? IdentificationNumber { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}