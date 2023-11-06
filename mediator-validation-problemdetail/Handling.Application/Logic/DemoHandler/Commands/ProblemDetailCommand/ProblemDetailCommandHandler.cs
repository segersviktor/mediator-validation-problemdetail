using MediatR;

namespace Handling.Application.Logic.DemoHandler.Commands.ProblemDetailCommand
{
    public class ProblemDetailCommandHandler : IRequestHandler<ProblemDetailCommand, Guid>
    {
        public Task<Guid> Handle(ProblemDetailCommand request, CancellationToken cancellationToken)
        { 
            // Do stuff 
            return Task.FromResult(Guid.NewGuid()); 
        }
    }
}
