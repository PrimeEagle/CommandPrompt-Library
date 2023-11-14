using System.Diagnostics.CodeAnalysis;

namespace VNet.CommandLine.Validation
{
    [ExcludeFromCodeCoverage]
    public class NullVerbValidator :ICommandValidator<IVerb>
    {
        public ValidationState DoValidate(IVerb item, string[] ruleSets = null)
        {
            return new ValidationState();
        }
    }
}