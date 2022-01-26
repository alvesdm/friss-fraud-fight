using FightFraud.Application.Business.Fraud.Results;
using FightFraud.Application.Common;
using FightFraud.Application.Common.Interfaces;
using FightFraud.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// I like keeping the Command and the CommandHandler together 
/// as it provides a better visibility as well as quick access.
/// </summary>
namespace FightFraud.Application.Business.Fraud.Commands
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
        private readonly ILogger<CalculateFraudCommandHandler> _logger;
        private readonly IMatchingCalculatorService _matchingCalculatorService;

        public CalculateFraudCommandHandler(
            IAmCaching cache,
            ILogger<CalculateFraudCommandHandler> logger,
            IMatchingCalculatorService matchingCalculatorService)
        {
            _cache = cache;
            _logger = logger;
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

            var cachingSeconds = 5;
            var cacheKey = $"{nameof(CalculateFraudCommand)}.{person.GetHashCode()}";
            var fraudProbabilityResult = await _cache.GetOrCacheAsync(
                cacheKey,
                async () => {
                    _logger.LogInformation("[Caching] Command {Name} caching {@CacheKey} for {@CachingTime} seconds", typeof(CalculateFraudCommand).Name, cacheKey, cachingSeconds);
                    return await _matchingCalculatorService.CalculateProbabilityAsync(person); 
                },
                TimeSpan.FromSeconds(cachingSeconds));

            return fraudProbabilityResult;
        }
    }
}
