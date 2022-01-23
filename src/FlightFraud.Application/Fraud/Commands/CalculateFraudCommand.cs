using FlightFraud.Application.Common.Interfaces;
using FlightFraud.Application.Fraud.Results;
using FlightFraud.Application.People.Extensions;
using FlightFraud.Domain.Entities;
using FlightFraud.Domain.Events;
using MediatR;
using NinjaNye.SearchExtensions.Levenshtein;
using Quickenshtein;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FlightFraud.Application.Fraud.Commands
{
    public class CalculateFraudCommand : IRequest<CalculateFraudResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? IdentificationNumber { get; set; }
    }

    public class CalculateFraudCommandHandler : IRequestHandler<CalculateFraudCommand, CalculateFraudResult>
    {
        private readonly IMatchingRulesService _matchingRulesService;

        public CalculateFraudCommandHandler(
            IMatchingRulesService matchingRulesService)
        {
            _matchingRulesService = matchingRulesService;
        }

        public async Task<CalculateFraudResult> Handle(CalculateFraudCommand request, CancellationToken cancellationToken)
        {
            var person = new Person
            {
                DateOfBirth = request.DateOfBirth,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IdentificationNumber = request.IdentificationNumber
            };

            return await _matchingRulesService.CalculateProbabilityAsync(person);
        }
    }

    public interface IMatchingRulesService
    {
        Task<CalculateFraudResult> CalculateProbabilityAsync(Person person);
    }

    public class MatchingRulesService : IMatchingRulesService
    {
        private readonly IApplicationDbContext _context;

        public MatchingRulesService(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CalculateFraudResult> CalculateProbabilityAsync(Person person)
        {
            var result = new CalculateFraudResult();

            //If the Identification number matches then 100%
            var matchingPerson = _context.People.FirstOrDefault(p => p.IdentificationNumber == person.IdentificationNumber);
            if (matchingPerson != null)
            {
                result = new CalculateFraudResult
                {
                    MatchingProbaility = 100,
                    Person = matchingPerson
                };

                //return Task.FromResult(result);
                return result;
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

            /*
             * IMPORTANT
             * For the sake of the challenge this should be enough.
             * Although I have never implemented such kind of search,
             * I believe this is where perhaps a SP would be the approach to be taken,
             * to avoid performance issues.
             * OR...
             * perhaps a background search
             */
            var asyncPeopleEnumerator = _context.People.AsAsyncEnumerable();
            await foreach(var p in asyncPeopleEnumerator)
            {
                var matchingProbability = 0.0;

                // If the last name is the same + 40 %
                if (p.LastName.Trim().Equals(person.LastName.Trim()))
                    matchingProbability += 40;
                // If the first name is the same + 20 %
                if (p.FirstName.Trim().Equals(person.FirstName.Trim()))
                    matchingProbability += 20;
                // If the first name is similar + 15 % (see examples)
                var source = p.FirstName.Trim();
                var target = person.FirstName.Trim();
                var higherLength = source.Length >= target.Length ? source.Length : target.Length;
                var firstNameDistance = Levenshtein.GetDistance(p.FirstName.Trim(), person.FirstName.Trim());
                if (firstNameDistance < higherLength)
                    matchingProbability += 15;
                // If the date of birth matches + 40 %
                if (p.DateOfBirth?.Date == person.DateOfBirth?.Date)
                    matchingProbability += 40;

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
