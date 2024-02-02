using FluentValidation;

namespace Application.PolicyManagement.Commands.Update
{
    public class UpdatePolicyCommandValidator : AbstractValidator<UpdatePolicyCommand>
    {
        public UpdatePolicyCommandValidator()
        {
            RuleFor(x => x.Premium)
                .NotEmpty().WithMessage("Premium is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Premium must be greater than or equal to zero.");

            RuleFor(x => x.StartDate.ToUniversalTime())
                .NotEmpty().WithMessage("Start date is required.")
                .Must(x => x.ToUniversalTime() >= DateTimeOffset.UtcNow.Date).WithMessage("Invalid start date.");

            RuleFor(x => x.EndDate.ToUniversalTime())
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThan(x => x.StartDate.ToUniversalTime()).WithMessage("End date must be greater than start date.");
        }
    }
}
