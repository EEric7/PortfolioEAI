
using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Projects.Commands
{
    public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
    {
        public DeleteProjectCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("L'identifiant du projet est requis");
        }
    }
}