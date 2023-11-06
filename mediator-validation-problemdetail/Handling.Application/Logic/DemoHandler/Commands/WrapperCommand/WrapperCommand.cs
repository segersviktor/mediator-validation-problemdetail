using Handling.Common.ResponseWrapper;
using MediatR;

namespace Handling.Application.Logic.DemoHandler.Commands.WrapperCommand
{
    public class WrapperCommand : IRequest<Result<string>>
    {
        public int Age { get; set; }
    }
}