using FluentValidation;
using PortfolioEAI.Application.Interfaces.UserPorts;

namespace PortfolioEAI.Application.Users.Commands
{
    public class AddExpUserCommandValidator : AbstractValidator<AddExpUserCommand>
    {
        public AddExpUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("L'ID utilisateur est requis.");

            RuleFor(x => x.Exp)
                .NotEmpty().WithMessage("Au moins une expérience doit être fournie.");
        }
    }
}