using System.Collections.Generic;
using VNet.CommandLine.Validation;

namespace VNet.CommandLine
{
    public class ParsedVerbsResult
    {
        public List<IVerb> Verbs { get; set; }
        public ValidationState ValidationState { get; set; }

        public ParsedVerbsResult()
        {
            this.ValidationState = new ValidationState();
        }
    }
}
