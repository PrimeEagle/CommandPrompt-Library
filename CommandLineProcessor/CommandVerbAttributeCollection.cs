using System.Collections.Generic;

namespace VNet.CommandLine
{
    public class CommandVerbAttributeCollection : ICommandVerbAttributeCollection
    {
        private IEnumerable<ICommandVerbAttribute> _verbAttributes;

        public IEnumerable<ICommandVerbAttribute> VerbAttributes { get; init; }

        public CommandVerbAttributeCollection(IEnumerable<ICommandVerbAttribute> verbAttributes)
        {
            _verbAttributes = verbAttributes;
            this.VerbAttributes = _verbAttributes;
        }
    }
}
