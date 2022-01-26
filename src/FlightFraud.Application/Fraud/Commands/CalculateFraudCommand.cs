using FightFraud.Application.Common.Interfaces;
using FightFraud.Application.Fraud.Results;
using FightFraud.Application.Fraud.Services;
using FightFraud.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FightFraud.Application.Fraud.Commands
{
    public class CalculateFraudCommand : IRequest<CalculateFraudResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string IdentificationNumber { get; set; }
    }

    public class CalculateFraudCommandHandler : IRequestHandler<CalculateFraudCommand, CalculateFraudResult>
    {
        private readonly IAmCaching _cache;
        private readonly IMatchingCalculatorService _matchingCalculatorService;

        public CalculateFraudCommandHandler(
            IAmCaching cache,
            IMatchingCalculatorService matchingCalculatorService)
        {
            _cache = cache;
            _matchingCalculatorService = matchingCalculatorService;
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

            var cacheKey = $"{nameof(CalculateFraudCommand)}.{person.GetHashCode()}";
            var fraudResult = await _cache.TryGetAsync<CalculateFraudResult>(cacheKey);
            if (fraudResult != null)
                return fraudResult;

            fraudResult = await _matchingCalculatorService.CalculateProbabilityAsync(person);
            await _cache.CreateAsync(cacheKey, fraudResult, TimeSpan.FromSeconds(5));
            return fraudResult;
        }
    }
}
