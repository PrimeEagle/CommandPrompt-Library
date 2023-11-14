using System.Collections.Generic;

namespace VNet.CommandLine
{
    public class VerbCollection : IVerbCollection
    {
        private IEnumerable<IVerb> _verbs;

        public IEnumerable<IVerb> Verbs { get; init; }

        public VerbCollection(IEnumerable<IVerb> options)
        {
            _verbs = options;
            this.Verbs = _verbs;
        }
    }
}
