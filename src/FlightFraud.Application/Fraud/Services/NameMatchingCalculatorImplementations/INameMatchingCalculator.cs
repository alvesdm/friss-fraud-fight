using FightFraud.Application.Fraud.Results;
using FightFraud.Domain.Entities;
using System.Threading.Tasks;

namespace FightFraud.Application.Fraud.Services.NameMatchingCalculatorImplementations
{
    public interface INameMatchingCalculator
    {
        Task<CalculateFraudResult> CalculateAsync(Person person);
    }
}
