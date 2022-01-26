using FightFraud.Application.Business.Fraud.Results;
using FightFraud.Application.Common.Interfaces;
using FightFraud.Domain.Entities;
using Quickenshtein;
using System.Threading.Tasks;

namespace FightFraud.Application.Business.Fraud.Services.NameMatchingCalculatorImplementations
{
    public class LevenshteinDistanceNameMatchingCalculator : INameMatchingCalculator
    {
        private readonly IApplicationDbContext _context;
        private readonly IMatchingRuleSettingsService _matchingRuleSettingsService;

        public LevenshteinDistanceNameMatchingCalculator(
            IApplicationDbContext context,
            IMatchingRuleSettingsService matchingRuleSettingsService)
        {
            _context = context;
            _matchingRuleSettingsService = matchingRuleSettingsService;
        }

        public async Task<CalculateFraudResult> CalculateAsync(Person person)
        {
            var result = new CalculateFraudResult();

            var matchingRuleSettings = await _matchingRuleSettingsService.GetOrDefaultAsync();

            /*
             * IMPORTANT
             * For the sake of this challenge this should be enough.
             * Although I have never implemented such kind of search,
             * I believe this is where perhaps a SP would be the approach to be taken 
             * or a custom DB function added to Linq() to help with performance issues by no trips back.
             * OR...
             * perhaps a background search
             */
            var asyncPeopleEnumerator = _context.People.AsAsyncEnumerable();
            await foreach (var p in asyncPeopleEnumerator)
            {
                var matchingProbability = 0.0M;

                // If the last name is the same + 40 %
                if (p.LastName.Trim().Equals(person.LastName.Trim()))
                    matchingProbability += matchingRuleSettings.LastNameSamePercent;
                // If the first name is the same + 20 %
                if (p.FirstName.Trim().Equals(person.FirstName.Trim()))
                {
                    matchingProbability += matchingRuleSettings.FirstNameSamePercent;
                }
                else
                {
                    // If the first name is similar + 15 % (see examples)
                    var source = p.FirstName.Trim();
                    var target = person.FirstName.Trim();
                    var higherLength = source.Length >= target.Length ? source.Length : target.Length;
                    var firstNameDistance = Levenshtein.GetDistance(p.FirstName.Trim(), person.FirstName.Trim());
                    if (firstNameDistance < higherLength)
                        matchingProbability += matchingRuleSettings.FirstNameSimilarPercent;
                }
                // If the date of birth matches + 40 %
                if (p.DateOfBirth?.Date == person.DateOfBirth?.Date)
                    matchingProbability += matchingRuleSettings.DateOfBirthSamePercent;

                if (matchingProbability > 0 && matchingProbability > result.MatchingProbaility)
                {
                    result = new CalculateFraudResult
                    {
                        MatchingProbaility = matchingProbability,
                        Person = p
                    };
                }

                if (matchingProbability >= 100)
                    break;
            }
            return result;
        }
    }
}
