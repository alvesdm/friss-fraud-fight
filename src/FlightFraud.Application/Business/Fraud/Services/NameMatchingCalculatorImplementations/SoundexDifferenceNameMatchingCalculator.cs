﻿using FightFraud.Application.Business.Fraud.Results;
using FightFraud.Application.Common.Interfaces;
using FightFraud.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FightFraud.Application.Business.Fraud.Services.NameMatchingCalculatorImplementations
{
    /// <summary>
    /// Uses the SQL Soundex function to perform the similar name matching
    /// </summary>
    public class SoundexDifferenceNameMatchingCalculator : INameMatchingCalculator
    {
        private readonly IApplicationDbContext _context;

        public SoundexDifferenceNameMatchingCalculator(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CalculateFraudResult> CalculateAsync(Person person)
        {
            throw new NotImplementedException();

            /*
             * For simplicity I've decided to use EF InMemory db and it seems like 
             * we can't do much extending on it.
             * In order to use SQL DIFFERENCE() function I believe the best approach 
             * would be to create a SP that would then perform the query using it 
             * on it's search criteria.
             * For that this implementation would be injected instead.
             */
        }
    }
}
