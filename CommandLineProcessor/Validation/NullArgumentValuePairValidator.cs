using System.Diagnostics.CodeAnalysis;

namespace VNet.CommandLine.Validation
{
    [ExcludeFromCodeCoverage]
    public class NullArgumentValuePairValidator : ICommandValidator<IArgumentValuePair>
    {
        public ValidationState DoValidate(IArgumentValuePair item, string[] ruleSets = null)
        {
            return new ValidationState();
        }
    }
}