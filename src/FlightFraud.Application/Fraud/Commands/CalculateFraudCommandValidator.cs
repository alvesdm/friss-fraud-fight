using FluentValidation;

namespace FightFraud.Application.Fraud.Commands
{
    public class CalculateFraudCommandValidator : AbstractValidator<CalculateFraudCommand>
    {
        public CalculateFraudCommandValidator()
        {
            RuleFor(v => v.FirstName)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(v => v.LastName)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}