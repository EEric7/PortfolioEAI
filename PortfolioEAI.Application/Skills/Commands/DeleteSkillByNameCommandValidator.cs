using System.Data;
using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Skills.Commands
{
    public class DeleteSkillByNameCommandValidator : AbstractValidator<DeleteSkillsByNameCommand>
    {
        public DeleteSkillByNameCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The skill name must not be empty.");
        }
    }
}