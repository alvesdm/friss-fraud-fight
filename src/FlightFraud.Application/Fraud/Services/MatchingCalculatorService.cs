using FightFraud.Application.Common.Interfaces;
using FightFraud.Application.Fraud.Results;
using FightFraud.Application.Fraud.Services.NameMatchingCalculatorImplementations;
using FightFraud.Application.People.Extensions;
using FightFraud.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace FightFraud.Application.Fraud.Services
{
    public class MatchingCalculatorService : IMatchingCalculatorService
    {
        private readonly IApplicationDbContext _context;
        private readonly INameMatchingCalculator _nameMatchingCalculator;

        public MatchingCalculatorService(
            IApplicationDbContext context,
            INameMatchingCalculator nameMatchingCalculator)
        {
            _context = context;
            _nameMatchingCalculator = nameMatchingCalculator;
        }

        public async Task<CalculateFraudResult> CalculateProbabilityAsync(Person person)
        {
            var result = new CalculateFraudResult();

            //If the Identification number matches then 100%
            if (!string.IsNullOrWhiteSpace(person.IdentificationNumber))
            {
                var matchingPerson = _context.People.FirstOrDefault(p => p.IdentificationNumber == person.IdentificationNumber);
                if (matchingPerson != null)
                {
                    result = new CalculateFraudResult
                    {
                        MatchingProbaility = 100,
                        Person = matchingPerson
                    };

                    return result;
                }
            }

            //If the dates of birth are known and not the same, there is no match
            var doesntDateOfBirthMatch = person.IsDateOfBirthKnown() && !_context.People.Any(p => p.DateOfBirth.HasValue && p.DateOfBirth == person.DateOfBirth);
            if (doesntDateOfBirthMatch)
            {
                result = new CalculateFraudResult
                {
                    MatchingProbaility = 0
                };

                return result;
            }

            return await _nameMatchingCalculator.CalculateAsync(person);
        }
    }
}
