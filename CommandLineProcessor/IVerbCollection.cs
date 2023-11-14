using System.Collections.Generic;

namespace VNet.CommandLine
{
    public interface IVerbCollection
    {
        IEnumerable<IVerb> Verbs { get; init; }
    }
}
