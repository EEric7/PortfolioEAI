using FluentValidation;
using PortfolioEAI.Application.Interfaces.UserPorts;

namespace PortfolioEAI.Application.Users.Commands
{
    public class AddProjectExpUserCommandValidator : AbstractValidator<AddProjExpUserCommand>
    {
        public AddProjectExpUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("L'ID utilisateur est requis.");

            RuleFor(x => x.ExpID)
                .NotEmpty().WithMessage("L'ID de l'expérience est requis.");

            RuleFor(x => x.Projects)
                .NotEmpty().WithMessage("Au moins un projet doit être fourni.");
        }
    }
}