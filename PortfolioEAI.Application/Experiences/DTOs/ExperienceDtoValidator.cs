using FluentValidation;
using PortfolioEAI.Application.DTOs;

namespace PortfolioEAI.Application.Experiences.DTOs
{
    public class ExperienceDtoValidator : AbstractValidator<ExperienceDto>
    {
        public ExperienceDtoValidator()
        {
            RuleFor(x => x.Company)
                .NotEmpty().WithMessage("La Company est requise.")
                .MaximumLength(100).WithMessage("La Company ne peut pas dépasser 100 caractères.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La description est requise.")
                .MaximumLength(1000).WithMessage("La description ne peut pas dépasser 1000 caractères.");

            /* RuleFor(x => x.ImageUrl)
                .MaximumLength(200).WithMessage("L'URL de l'image ne peut pas dépasser 200 caractères."); */
        }
    }
}