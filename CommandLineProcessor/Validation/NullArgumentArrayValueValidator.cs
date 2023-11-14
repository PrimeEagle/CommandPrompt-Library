using System.Diagnostics.CodeAnalysis;

namespace VNet.CommandLine.Validation
{
    [ExcludeFromCodeCoverage]
    public class NullArgumentArrayValueValidator : ICommandValidator<IArgumentArrayValue>
    {
        public ValidationState DoValidate(IArgumentArrayValue item, string[] ruleSets = null)
        {
            return new ValidationState();
        }
    }
}