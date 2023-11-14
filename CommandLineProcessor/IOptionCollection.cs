using System.Collections.Generic;

namespace VNet.CommandLine
{
    public interface IOptionCollection
    {
        IEnumerable<IOption> Options { get; init; }
    }
}
