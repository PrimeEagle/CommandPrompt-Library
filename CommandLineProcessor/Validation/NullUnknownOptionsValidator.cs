using System.Diagnostics.CodeAnalysis;

namespace VNet.CommandLine.Validation
{
    public class NullUnknownOptionsValidator : ICommandValidator<IUnknownOptions>
    {
        [ExcludeFromCodeCoverage]
        public ValidationState DoValidate(IUnknownOptions item, string[] ruleSets = null)
        {
            return new ValidationState();
        }
    }
}