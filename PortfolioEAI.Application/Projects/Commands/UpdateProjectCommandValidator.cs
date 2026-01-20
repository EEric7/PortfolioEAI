using System.Data;
using FluentValidation;
using PortfolioEAI.Application.Interfaces;

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
                RuleFor(x => x.Project.Id)
                    .NotEqual(Guid.Empty).WithMessage("The project ID is required");

                RuleFor(x => x.Project.Title)
                    .MaximumLength(200).WithMessage("The project title cannot exceed 200 characters");
                
                RuleFor(x => x.Project.Description)
                    .MaximumLength(2000).WithMessage("The project description cannot exceed 2000 characters");

                RuleFor(x => x.Project.StartDate)
                    .LessThanOrEqualTo(x => x.Project.EndDate)
                    .When(x => x.Project.EndDate.HasValue)
                    .WithMessage("The start date must be earlier than or equal to the end date");
            });
            
        }
    }
}