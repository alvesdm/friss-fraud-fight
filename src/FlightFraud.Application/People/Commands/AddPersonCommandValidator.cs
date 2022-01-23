using FluentValidation;

namespace FlightFraud.Application.People.Commands
{
    public class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
    {
        public AddPersonCommandValidator()
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