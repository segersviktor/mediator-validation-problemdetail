using Handling.Api.Configuration;
using Handling.Application.ServiceRegistration;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddApplicationServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

// TODO ENABLE
// Add validation middleware when using Data Annotations
// Add MVC before the APIBehaviorOptions
// builder.Services.AddMvc();
//
// Add logic for handling the validation messages
// builder.Services.Configure<ApiBehaviorOptions>(options =>
// { 
//     options.InvalidModelStateResponseFactory = context =>
//     {
//         var problemDetails = new ValidationProblemDetails(context.ModelState)
//         {
//             Instance = context.HttpContext.Request.Path,
//             Status = StatusCodes.Status400BadRequest,
//             Type = $"https://httpstatuses.com/400",
//          };
//         
//         return new BadRequestObjectResult(problemDetails);
//     };
// });
//


// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationInsightsTelemetry();


// Add middleware for handling exceptions
// 1) Install Hellang.Middleware.ProblemDetails
// Configure problem details

// TODO ENABLE
// builder.Services.AddProblemDetails(ProblemDetailConfiguration.ConfigureProblemDetail(builder));

//-------------------//
var app = builder.Build();


app.UseMiddleware<CorrelationIdMiddleware>();
app.UseSwaggerDocumentation();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

// TODO ENABLE
// app.UseProblemDetails();
app.MapControllers();

app.Run();
