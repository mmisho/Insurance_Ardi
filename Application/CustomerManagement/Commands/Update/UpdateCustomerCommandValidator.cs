using FluentValidation;
namespace Application.CustomerManagement.Commands.Update
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.FirstName)
                    .NotEmpty().WithMessage("FirstName is required.");

            RuleFor(x => x.LastName)
                    .NotEmpty().WithMessage("LastName  is required.");

            RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email address is required.")
                    .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.PersonalNumber)
                    .NotEmpty().WithMessage("Personal number is required.")
                    .Matches("^[0-9]*$").WithMessage("Personal number should only contain numeric digits.");

            RuleFor(x => x.DateOfBirth)
                    .Must(x => x < DateTimeOffset.UtcNow).WithMessage("Invalid date of birth.");
        }
    }
}
