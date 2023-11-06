namespace Handling.Application.Configuration.Validation
{
    public class InvalidCommandException : Exception
    {
        public List<ErrorDetails> Errors { get; }
        public InvalidCommandException(string message, List<ErrorDetails> errors) : base(message)
        {
            Errors = errors;
        }
    }

    public class ErrorDetails 
    {
        public ErrorDetails(string propertyName, string reason, string validationCode)
        {
            PropertyName = propertyName;
            Reason = reason;
            ValidationCode = validationCode;
        }

        public ErrorDetails()
        {
            
        }
        
        public string PropertyName { get; set; }
        public string Reason { get; set; } 
        public string ValidationCode { get; set; } 
    }
}