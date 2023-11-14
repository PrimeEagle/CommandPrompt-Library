using System.Diagnostics.CodeAnalysis;

namespace VNet.CommandLine.Validation
{
    [ExcludeFromCodeCoverage]
    public class ValidationError
    {
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public string PropertyName { get; set; }
        public object AttemptedValue { get; set; }
        public ErrorSeverity Severity { get; set; }
        public ErrorCategory Category { get; set; }
        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}
