using System.Data;
using FluentValidation;
using PortfolioEAI.Application.Interfaces;
using PortfolioEAI.Application.Projects.DTOs;

namespace PortfolioEAI.Application.Projects.Commands
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(x => x.Project)
                .NotNull().WithMessage("The project is required");

            When(x => x.Project != null, () =>
            {
                RuleFor(x => x.Project).SetValidator(new ProjectDtoValidator());
            });
        }
    }
}