using FluentValidation;

namespace Handling.Application.Logic.DemoHandler.Commands.WrapperCommand
{
    public class WrapperCommandValidator : AbstractValidator<WrapperCommand>
    {
        public WrapperCommandValidator()
        {
            RuleFor(x => x.Age)
                .NotEmpty()
                .InclusiveBetween(18,99);
        }
    }
}
