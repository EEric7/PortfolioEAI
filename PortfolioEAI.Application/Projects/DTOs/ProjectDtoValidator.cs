using FluentValidation;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Projects.DTOs
{
    public class ProjectDtoValidator : AbstractValidator<ProjectDto>
    {
        public ProjectDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("The project name is required.")
                .MaximumLength(200).WithMessage("The project name cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("The project description cannot exceed 1000 characters.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("The start date is required.");
        }
    }
}