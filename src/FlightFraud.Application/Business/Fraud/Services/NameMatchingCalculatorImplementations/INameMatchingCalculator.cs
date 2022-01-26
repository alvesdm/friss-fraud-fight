using FightFraud.Application.Business.Fraud.Results;
using FightFraud.Domain.Entities;
using System.Threading.Tasks;

namespace FightFraud.Application.Business.Fraud.Services.NameMatchingCalculatorImplementations
{
    public interface INameMatchingCalculator
    {
        Task<CalculateFraudResult> CalculateAsync(Person person);
    }
}
