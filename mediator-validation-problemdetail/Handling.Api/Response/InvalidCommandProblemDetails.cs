using System.Text.Json.Serialization;
using Handling.Application.Configuration.Validation;

namespace Handling.Api.Response
{
    public class InvalidCommandProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        [JsonPropertyName("errors")]
        public new IEnumerable<ValidationError> Errors { get; }
         
        public InvalidCommandProblemDetails(InvalidCommandException exception)
        {
            Title = exception.Message;
            Status = StatusCodes.Status400BadRequest; 
            Type = "https://myepicapi/validation-error";
            Errors = exception.Errors.Select(_ => new ValidationError(_.Reason, _.PropertyName, _.ValidationCode)
            {
                Message = _.Reason,
                Property = _.PropertyName,
                Code = _.ValidationCode,
             }); 
        }
    }
    
    public class ValidationError
    {
        public ValidationError(string property, string message, string code)
        {
            Property = property;
            Message = message;
            Code = code;
            Type = "https://myepicapi/validation-error"+ code;
        }

        public string Property { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
    }
}