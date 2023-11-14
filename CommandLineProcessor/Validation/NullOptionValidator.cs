using System.Diagnostics.CodeAnalysis;

namespace VNet.CommandLine.Validation
{
    [ExcludeFromCodeCoverage]
    public class NullOptionValidator : ICommandValidator<IOption>
    {
        public ValidationState DoValidate(IOption item, string[] ruleSets = null)
        {
            return new ValidationState();
        }
    }
}