using System.Collections.Generic;
using VNet.CommandLine.Validation;

namespace VNet.CommandLine
{
    public interface ICommandVerbAttributeCollection
    {
        IEnumerable<ICommandVerbAttribute> VerbAttributes { get; init; }
    }
}
