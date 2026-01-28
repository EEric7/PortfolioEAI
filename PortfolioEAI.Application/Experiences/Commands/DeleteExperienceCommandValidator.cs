using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Experiences.Commands
{
    public class DeleteExperienceCommandValidator : AbstractValidator<DeleteExperienceCommand>
    {
        public DeleteExperienceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Experience ID must not be empty.");
        }
    }
}