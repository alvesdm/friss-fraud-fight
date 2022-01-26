using FightFraud.Domain.Common;
using System;

namespace FightFraud.Domain.Entities
{
    public class MatchingRuleSettings : IAmEntity<Guid>
    {
        public Guid Id { get; set; }
        public decimal LastNameSamePercent { get; set; }
        public decimal FirstNameSamePercent { get; set; }
        public decimal FirstNameSimilarPercent { get; set; }
        public decimal DateOfBirthSamePercent { get; set; }
    }
}