using FluentValidation;

namespace Handling.Application.Logic.DemoHandler.Commands.ProblemDetailCommand
{
    public class ProblemDetailCommandValidator : AbstractValidator<ProblemDetailCommand>
    {
        public ProblemDetailCommandValidator()
        {
            RuleFor(x => x.Age)
                .NotEmpty()
                .InclusiveBetween(18,99);
        }
    }
}
