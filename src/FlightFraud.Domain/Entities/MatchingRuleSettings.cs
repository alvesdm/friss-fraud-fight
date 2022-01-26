using FightFraud.Domain.Common;
using System;

namespace FightFraud.Domain.Entities
{
    /// <summary>
    /// This would be better off in either a document db, 
    /// or at least a key/value table for settings or such.
    /// But for the sake of simplicity, I created it in a tabular structure,
    /// as I believe for the sake of the exercise it won't have much impact 
    /// and make it slightly simpler for now.
    /// </summary>
    public class MatchingRuleSettings : IAmEntity<Guid>
    {
        public Guid Id { get; set; }
        public decimal LastNameSamePercent { get; set; }
        public decimal FirstNameSamePercent { get; set; }
        public decimal FirstNameSimilarPercent { get; set; }
        public decimal DateOfBirthSamePercent { get; set; }
    }
}