﻿using FightFraud.Domain.Entities;

namespace FightFraud.Application.Business.Fraud.Results
{
    public class CalculateFraudResult
    {
        public Person Person { get; set; }
        public decimal MatchingProbaility { get; set; }
    }
}
