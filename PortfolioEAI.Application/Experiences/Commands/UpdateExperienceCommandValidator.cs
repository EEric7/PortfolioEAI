using FluentValidation;
using PortfolioEAI.Application.Experiences.DTOs;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Experiences.Commands
{
    public class UpdateExperienceCommandValidator : AbstractValidator<UpdateExperienceCommand>
    {
        public UpdateExperienceCommandValidator()
        {
            RuleFor(x => x.Dto)
                .NotNull().WithMessage("Experience data must be provided.");

            When(x => x.Dto != null, () =>
            {   
                RuleFor(x => x.Dto).SetValidator(new ExperienceDtoValidator());
            });
        }
    }
}