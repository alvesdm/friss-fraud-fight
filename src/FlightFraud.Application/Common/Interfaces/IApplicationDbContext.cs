using FlightFraud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FlightFraud.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Person> People { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}