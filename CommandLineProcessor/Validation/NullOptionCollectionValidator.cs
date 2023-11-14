using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace VNet.CommandLine.Validation
{
    [ExcludeFromCodeCoverage]
    public class NullOptionCollectionValidator : ICommandValidator<List<IOption>>
    {
        public ValidationState DoValidate(List<IOption> item, string[] ruleSets = null)
        {
            return new ValidationState();
        }
    }
}