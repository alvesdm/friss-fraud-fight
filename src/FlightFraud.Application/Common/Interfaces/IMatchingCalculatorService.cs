using FightFraud.Application.Fraud.Results;
using FightFraud.Domain.Entities;
using System.Threading.Tasks;

namespace FightFraud.Application.Fraud.Services
{
    public interface IMatchingCalculatorService
    {
        Task<CalculateFraudResult> CalculateProbabilityAsync(Person person);
    }
}
