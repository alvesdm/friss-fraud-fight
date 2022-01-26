using FightFraud.Application.Fraud.Results;
using FightFraud.Domain.Entities;
using System.Threading.Tasks;

namespace FightFraud.Application.Common.Interfaces
{
    public interface IMatchingRuleSettingsService
    {
        Task UpdateAsync(MatchingRuleSettings settings);
        Task<MatchingRuleSettings> GetOrDefaultAsync();
    }
}
