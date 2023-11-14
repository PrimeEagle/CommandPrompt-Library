using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace VNet.CommandLine.Validation
{
    [ExcludeFromCodeCoverage]
    public class NullAdditionalValidators : IAdditionalValidators
    {
        public List<ICommandValidator> Validators { get; init; }
    }
}
