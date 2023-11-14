using System.Collections.Generic;

namespace VNet.CommandLine
{
    public class OptionCollection : IOptionCollection
    {
        private IEnumerable<IOption> _options;

        public IEnumerable<IOption> Options { get; init; }

        public OptionCollection(IEnumerable<IOption> options)
        {
            _options = options;
            this.Options = _options;
        }
    }
}
