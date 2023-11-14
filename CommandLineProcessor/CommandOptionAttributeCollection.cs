using System.Collections.Generic;

namespace VNet.CommandLine
{
    public class CommandOptionAttributeCollection : ICommandOptionAttributeCollection
    {
        private IEnumerable<ICommandOptionAttribute> _optionAttributes;

        public IEnumerable<ICommandOptionAttribute> OptionAttributes { get; init; }

        public CommandOptionAttributeCollection(IEnumerable<ICommandOptionAttribute> optionAttributes)
        {
            _optionAttributes = optionAttributes;
            this.OptionAttributes = _optionAttributes;
        }
    }
}
