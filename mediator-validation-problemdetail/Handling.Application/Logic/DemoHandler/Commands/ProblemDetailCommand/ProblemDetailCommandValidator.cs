using FluentValidation;

namespace Handling.Application.Logic.DemoHandler.Commands.DemoCommand
{
    public class ProblemDetailCommandValidator : AbstractValidator<ProblemDetailCommand.ProblemDetailCommand>
    {
        public ProblemDetailCommandValidator()
        {
            RuleFor(x => x.Age)
                .NotEmpty()
                .InclusiveBetween(18,99);
        }
    }
}
