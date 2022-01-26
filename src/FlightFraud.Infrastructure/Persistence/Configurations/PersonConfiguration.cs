using FightFraud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FightFraud.Infrastructure.Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.Property(t => t.FirstName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(t => t.LastName)
                .HasMaxLength(30)
                .IsRequired();

        }
    }
}