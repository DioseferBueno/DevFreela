using DevFreela.Application.Commands.Project.InsertProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class InsertProjectValidator : AbstractValidator<InsertProjectCommand>
    {
        public InsertProjectValidator()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .WithMessage("O Titulo deve ser informado")
                .MaximumLength(50)
                .WithMessage("Title must have a maximum of 50 characters");

            RuleFor(c=> c.TotalCost)
                .GreaterThanOrEqualTo(1000)
                .WithMessage("O Projeto deve custar pelo menos R$1000.00");
        }
    }
}
