using MediatR;

namespace Handling.Application.Logic.DemoHandler.Commands.ProblemDetailCommand
{
    public class ProblemDetailCommand : IRequest<Guid>
    {
        public int Age { get; set; }
    }
}