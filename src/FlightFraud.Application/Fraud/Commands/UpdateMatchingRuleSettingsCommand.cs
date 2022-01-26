using FightFraud.Application.Common.Interfaces;
using FightFraud.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FightFraud.Application.Fraud.Commands
{
    public class UpdateMatchingRuleSettingsCommand : IRequest
    {
        public decimal LastNameSamePercent { get; set; }
        public decimal FirstNameSamePercent { get; set; }
        public decimal FirstNameSimilarPercent { get; set; }
        public decimal DateOfBirthSamePercent { get; set; }
    }

    public class UpdateMatchingRuleSettingsCommandHandler : AsyncRequestHandler<UpdateMatchingRuleSettingsCommand>
    {
        private readonly IMatchingRuleSettingsService _matchingRulesService;

        public UpdateMatchingRuleSettingsCommandHandler(
            IMatchingRuleSettingsService matchingRulesService)
        {
            _matchingRulesService = matchingRulesService;
        }

        protected override async Task Handle(UpdateMatchingRuleSettingsCommand request, CancellationToken cancellationToken)
        {
            await _matchingRulesService.UpdateAsync(new MatchingRuleSettings
            {
                DateOfBirthSamePercent = request.DateOfBirthSamePercent,
                FirstNameSamePercent = request.FirstNameSamePercent,
                FirstNameSimilarPercent = request.FirstNameSimilarPercent,
                LastNameSamePercent = request.LastNameSamePercent
            });
        }
    }
}
