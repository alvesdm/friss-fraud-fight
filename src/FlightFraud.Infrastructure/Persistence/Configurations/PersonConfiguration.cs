using FlightFraud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightFraud.Infrastructure.Persistence.Configurations
{

    public class ersonConfiguration : IEntityTypeConfiguration<Person>
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