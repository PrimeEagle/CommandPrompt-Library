using System.Collections.Generic;
using VNet.CommandLine.Validation;

namespace VNet.CommandLine
{
    public interface ICommandOptionAttributeCollection
    {
        IEnumerable<ICommandOptionAttribute> OptionAttributes { get; init; }
    }
}
