using Handling.Application.Logic.DemoHandler.Commands.DemoCommand;
using Handling.Common.ResponseWrapper;
using MediatR;

namespace Handling.Application.Logic.DemoHandler.Commands.WrapperCommand
{
    public class DemoCommandHandler : IRequestHandler<WrapperCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(WrapperCommand request, CancellationToken cancellationToken)
        { 
            var validator = new WrapperCommandValidator();
            var x = await validator.ValidateAsync(request, cancellationToken);
            if (!x.IsValid)
            {
                return await Result<string>.FailedAsync(x.Errors.Select(failure => failure.ErrorMessage).ToList()); 
            } 
            
            // Application logic
            // DO STUFF
            
            const string ticketCode = "1234567890";
            return await Result<string>.SuccessAsync(ticketCode);
        }
    }
}
