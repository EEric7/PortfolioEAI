using FluentValidation;
using PortfolioEAI.Application.Interfaces.UserPorts;
using PortfolioEAI.Application.Projects.DTOs;

namespace PortfolioEAI.Application.Users.Commands
{
    public class AddProjectExpUserCommandValidator : AbstractValidator<AddProjExpUserCommand>
    {
        public AddProjectExpUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("The user ID is required.");

            RuleFor(x => x.ExpID)
                .NotEmpty().WithMessage("The experience ID is required.");

            RuleFor(x => x.Projects)
                .NotEmpty().WithMessage("At least one project must be provided.");

            When(x => x.Projects != null, () =>
            {
                RuleForEach(x => x.Projects).SetValidator(new ProjectDtoValidator());
            });
        }
    }
}