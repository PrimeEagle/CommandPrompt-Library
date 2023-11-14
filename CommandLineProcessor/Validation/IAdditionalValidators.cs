using System.Collections.Generic;

namespace VNet.CommandLine.Validation
{
    public interface IAdditionalValidators
    {
        List<ICommandValidator> Validators { get; init; }
    }
}
