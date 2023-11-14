using System.Collections.Generic;
using VNet.CommandLine.Validation;

namespace VNet.CommandLine
{
    internal class ParseOptionResult
    {
        internal IEnumerable<string> UnknownOptions { get; set; }
        internal int LastIndexProcessed { get; set; }
        internal ValidationState ValidationState { get; set; }

        public ParseOptionResult()
        {
            this.ValidationState = new ValidationState();
        }
    }
}
