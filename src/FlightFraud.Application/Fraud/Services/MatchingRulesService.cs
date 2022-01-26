using FightFraud.Application.Common.Interfaces;
using FightFraud.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FightFraud.Application.Fraud.Services
{
    public class MatchingRuleSettingsService : IMatchingRuleSettingsService
    {
        private const decimal DefaultLastNameSamePercent = 40;
        private const decimal DefaultFirstNameSamePercent = 20;
        private const decimal DefaultFirstNameSimilarPercent = 15;
        private const decimal DefaultDateOfBirthSamePercent = 40;

        private readonly IApplicationDbContext _context;

        public MatchingRuleSettingsService(
            IApplicationDbContext context
            )
        {
            _context = context;
        }

        public async Task UpdateAsync(MatchingRuleSettings settings)
        {
            var existingSettings = await GetOrDefaultAsync();
            existingSettings.FirstNameSamePercent = settings.FirstNameSamePercent;
            existingSettings.FirstNameSimilarPercent = settings.FirstNameSimilarPercent;
            existingSettings.LastNameSamePercent = settings.LastNameSamePercent;
            existingSettings.DateOfBirthSamePercent = settings.DateOfBirthSamePercent;

            await _context.SaveChangesAsync();
        }

        public async Task<MatchingRuleSettings> GetOrDefaultAsync()
        {
            var setting = _context.MatchingRuleSettings.FirstOrDefault();
            if (setting == null)
            {
                setting = await GenerateDefault();
            }

            return setting;
        }

        private async Task<MatchingRuleSettings> GenerateDefault()
        {
            var setting = new MatchingRuleSettings
            {
                LastNameSamePercent = DefaultLastNameSamePercent,
                FirstNameSamePercent = DefaultFirstNameSamePercent,
                FirstNameSimilarPercent = DefaultFirstNameSimilarPercent,
                DateOfBirthSamePercent = DefaultDateOfBirthSamePercent
            };

             _context.MatchingRuleSettings.Add(setting);
            await _context.SaveChangesAsync();

            return setting;
        }
    }
}
