using FightFraud.Application.Common.Interfaces;
using FightFraud.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FightFraud.Application.Business.Fraud.Commands
{
    public class GetMatchingRuleSettingsCommand : IRequest<MatchingRuleSettings>
    { }

    public class GetMatchingRuleSettingsCommandHandler : IRequestHandler<GetMatchingRuleSettingsCommand, MatchingRuleSettings>
    {
        private readonly IMatchingRuleSettingsService _matchingRuleSettingsService;

        public GetMatchingRuleSettingsCommandHandler(
            IMatchingRuleSettingsService matchingRuleSettingsService)
        {
            _matchingRuleSettingsService = matchingRuleSettingsService;
        }

        public async Task<MatchingRuleSettings> Handle(GetMatchingRuleSettingsCommand request, CancellationToken cancellationToken)
        {
            return await _matchingRuleSettingsService.GetOrDefaultAsync();
        }
    }
}
