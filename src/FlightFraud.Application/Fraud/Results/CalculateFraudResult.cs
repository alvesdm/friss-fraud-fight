using FlightFraud.Domain.Entities;

namespace FlightFraud.Application.Fraud.Results
{
    public class CalculateFraudResult
    {
        public Person? Person { get; set; }
        public double MatchingProbaility { get; set; }
    }
}
