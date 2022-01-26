using FightFraud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FightFraud.Infrastructure.Persistence.Configurations
{
    public class MatchingRuleSettingsConfiguration : IEntityTypeConfiguration<MatchingRuleSettings>
    {
        public void Configure(EntityTypeBuilder<MatchingRuleSettings> builder)
        {
            builder.Property(t => t.DateOfBirthSamePercent)
                .IsRequired();
            builder.Property(t => t.FirstNameSimilarPercent)
                .IsRequired();
            builder.Property(t => t.LastNameSamePercent)
                .IsRequired();
            builder.Property(t => t.FirstNameSamePercent)
                .IsRequired();
        }
    }
}