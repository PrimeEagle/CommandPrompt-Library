using System.Collections.Generic;

namespace VNet.CommandLine
{
    public interface IBuilder
    {
        ICollection<IVerb> Load(object[] parameters);
    }
}
