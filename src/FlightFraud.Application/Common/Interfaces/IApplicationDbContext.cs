using FightFraud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FightFraud.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Person> People { get; }
        DbSet<MatchingRuleSettings> MatchingRuleSettings { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}