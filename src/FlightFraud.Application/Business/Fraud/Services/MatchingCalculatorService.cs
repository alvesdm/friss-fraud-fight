using FightFraud.Application.Business.Fraud.Results;
using FightFraud.Application.Business.Fraud.Services.NameMatchingCalculatorImplementations;
using FightFraud.Application.Business.People.Extensions;
using FightFraud.Application.Common.Interfaces;
using FightFraud.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace FightFraud.Application.Business.Fraud.Services
{
    public class MatchingCalculatorService : IMatchingCalculatorService
    {
        private readonly IApplicationDbContext _context;
        private readonly INameMatchingCalculator _nameMatchingCalculator;

        public MatchingCalculatorService(
            IApplicationDbContext context,
            //This allows us to have Bridge Pattern in place, as we can have different implementations
            INameMatchingCalculator nameMatchingCalculator)
        {
            _context = context;
            _nameMatchingCalculator = nameMatchingCalculator;
        }

        public async Task<CalculateFraudResult> CalculateProbabilityAsync(Person person)
        {
            //If the Identification number matches then 100%
            if (!string.IsNullOrWhiteSpace(person.IdentificationNumber))
            {
                var matchingPerson = _context.People.FirstOrDefault(p => p.IdentificationNumber == person.IdentificationNumber);
                if (matchingPerson != null)
                {
                    return new CalculateFraudResult
                    {
                        MatchingProbaility = 100,
                        Person = matchingPerson
                    };
                }
            }

            //If the dates of birth are known and not the same, there is no match
            var doesntDateOfBirthMatch = person.IsDateOfBirthKnown() && !_context.People.Any(p => p.DateOfBirth.HasValue && p.DateOfBirth == person.DateOfBirth);
            if (doesntDateOfBirthMatch)
            {
                return new CalculateFraudResult
                {
                    MatchingProbaility = 0
                };
            }

            //The conditions above, rule out any other condition.
            //So, once they are not satisfied, we can go on with the ones name fuzziness

            return await _nameMatchingCalculator.CalculateAsync(person);
        }
    }
}
