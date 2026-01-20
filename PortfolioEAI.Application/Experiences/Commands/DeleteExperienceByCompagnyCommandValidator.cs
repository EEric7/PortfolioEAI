using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Experiences.Commands
{
    public class DeleteExperienceByCompagnyCommandValidator : AbstractValidator<DeleteExperienceByCompanyCommand>
    {
        public DeleteExperienceByCompagnyCommandValidator()
        {
            RuleFor(cmd => cmd.Company)
                .NotEmpty().WithMessage("Company name must not be empty.");
        }
    }
}