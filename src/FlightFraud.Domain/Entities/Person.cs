using FightFraud.Domain.Common;
using System;
using System.Collections.Generic;

namespace FightFraud.Domain.Entities
{
    public class Person : IAmEntity<Guid>, IHaveDomainEvent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string IdentificationNumber { get; set; }

        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, DateOfBirth, IdentificationNumber);
        }
    }
}