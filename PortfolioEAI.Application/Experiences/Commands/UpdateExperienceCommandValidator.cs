using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Experiences.Commands
{
    public class UpdateExperienceCommandValidator : AbstractValidator<UpdateExperienceCommand>
    {
        public UpdateExperienceCommandValidator()
        {
            RuleFor(x => x.experience)
                .NotNull().WithMessage("Experience data must be provided.");

            When(x => x.experience != null, () =>
            {   
                RuleFor(x => x.experience.Id)
                    .NotEqual(Guid.Empty).WithMessage("Experience ID must be provided.");
                RuleFor(x => x.experience.Company)
                    .NotEmpty().WithMessage("Company name is required.");
                RuleFor(x => x.experience.Position)
                    .NotEmpty().WithMessage("Position is required.");
                // Additional rules can be added as needed
            });
        }
    }
}