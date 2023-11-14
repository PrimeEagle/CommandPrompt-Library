using System.Collections.Generic;
using VNet.CommandLine.Validation;

namespace VNet.CommandLine
{
    public interface IParser
    {
        ParsedVerbsResult Parse(string[] args, ICollection<IVerb> configuredVerbs, IValidatorManager validator, bool allowUnknownOptions);
    }
}
