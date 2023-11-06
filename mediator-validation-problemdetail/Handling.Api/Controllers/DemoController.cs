using Handling.Application.Logic;
using Handling.Application.Logic.DemoHandler.Commands.DemoCommand;
using Handling.Application.Logic.DemoHandler.Commands.ProblemDetailCommand;
using Handling.Application.Logic.DemoHandler.Commands.WrapperCommand;
using Handling.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Handling.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DemoController : ControllerBase
{
    protected readonly IMediator _mediator;

    public DemoController(IMediator mediator)
    {
        _mediator = mediator;
    }


    #region Return 400 BAD REQUEST

    [HttpPost("BuyTicketsReturn400")]
    public IActionResult BuyTicketsReturn400([FromBody] int age)
    {
        if (age < 18)
        {
            return BadRequest("Age must be greater than 18");
        }

        return Ok();
    }

    #endregion

    #region Return 400 BAD REQUEST with ProblemDetails

    [HttpPost("ReturnProblemDetail")]
    public IActionResult ReturnProblemDetail([FromBody] int age)
    {
        if (age < 18)
        {
            return Problem(
                "The age should be 18 or greater",
                $"/demo/ReturnProblemDetail",
                StatusCodes.Status400BadRequest,
                "Cannot buy tickets",
                "https://mydocumentation.com/problems/age-not-allowed");
        }
        else
        {
            // Tickets are sold out
            return Problem(
                "The tickets are sold out",
                $"/demo/ReturnProblemDetail",
                StatusCodes.Status400BadRequest,
                "Cannot buy tickets",
                "https://mydocumentation.com/problems/tickets-sold-out");
        }
    }

    [HttpPost("ReturnParsedCustomException")]
    public IActionResult ReturnParsedCustomException([FromBody] int age)
    {
        if (age < 18)
        {
            throw new AgeToYoungException();
        }
        else
        {
            throw new TicketsSoldOutException();
        }
    }


    [HttpPost("ReturnException")]
    public IActionResult ReturnException()
    {
        throw new Exception("Testing problem details");
    }

    #endregion

    #region validation

    [HttpPost("PostDemoWithAttributes")]
    public IActionResult PostDemoWithAttributes([FromBody] DemoModelWithAttributes request)
    {
        // LOGIC
        return Ok();
    }

    [HttpPost("DemoWithMediatorWrapper")]
    public IActionResult PostDemoWithAttributes([FromBody] WrapperCommand request)
    {
        return Ok(_mediator.Send(request));
    }
    
    [HttpPost("DemoWithMediatorProblemDetails")]
    public IActionResult PostDemoWithAttributes([FromBody] ProblemDetailCommand request)
    {
        return Ok(_mediator.Send(request));
    }

    #endregion
}