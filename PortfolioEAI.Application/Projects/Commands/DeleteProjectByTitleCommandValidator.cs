using System.Data;
using FluentValidation;
using PortfolioEAI.Application.Interfaces;

namespace PortfolioEAI.Application.Projects.Commands
{
    public class DeleteProjectByTitleCommandValidator : AbstractValidator<DeleteProjectByTitleCommand>
    {
        public DeleteProjectByTitleCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("The project title is required");
        }
    }
}