using System.Data;
using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Skills.Commands
{
    public class DeleteSkillCommandValidator : AbstractValidator<DeleteSkillCommand>
    {
        public DeleteSkillCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("L'ID de la compétence est requis");
        }
    }
}