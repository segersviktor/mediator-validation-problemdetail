using Handling.Api.Configuration;
using Handling.Application.ServiceRegistration;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

// TODO ENABLE
// Add validation middleware when using Data Annotations
// Add MVC before the APIBehaviorOptions
 builder.Services.AddMvc();
//
// Add logic for handling the validation messages
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState)
        {
            Title = "One or more validation errors occurred.",
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status400BadRequest,
            Type = $"https://httpstatuses.com/validation-error",
        };

        return new BadRequestObjectResult(problemDetails);
    };
});


// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddApplicationServices();


// Add middleware for handling exceptions
// 1) Install Hellang.Middleware.ProblemDetails
// Configure problem details

// TODO ENABLE
builder.Services.AddProblemDetails(ProblemDetailConfiguration.ConfigureProblemDetail(builder));

//-------------------//
var app = builder.Build();


app.UseSwaggerDocumentation();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

// TODO ENABLE
app.UseProblemDetails();
app.MapControllers();

app.Run();