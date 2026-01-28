using FluentValidation;
using PortfolioEAI.Application.Experiences.DTOs;
using PortfolioEAI.Application.Interfaces.UserPorts;

namespace PortfolioEAI.Application.Users.Commands
{
    public class AddExpUserCommandValidator : AbstractValidator<AddExpUserCommand>
    {
        public AddExpUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("The user ID is required.");

            RuleFor(x => x.Exp)
                .NotEmpty().WithMessage("At least one experience must be provided.");

            When(x => x.Exp != null, () =>
            {
                RuleForEach(x => x.Exp).SetValidator(new ExperienceDtoValidator());
            });
        }
    }
}