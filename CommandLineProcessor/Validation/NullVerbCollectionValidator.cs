using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace VNet.CommandLine.Validation
{
    [ExcludeFromCodeCoverage]
    public class NullVerbCollectionValidator : ICommandValidator<List<IVerb>>
    {
        public ValidationState DoValidate(List<IVerb> item, string[] ruleSets = null)
        {
            return new ValidationState();
        }
    }
}