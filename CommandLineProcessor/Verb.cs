using System;
using System.Collections.Generic;

namespace VNet.CommandLine
{
    public class Verb : IVerb
    {
        public string ShortName { get; set; }
        public List<string> LongNames { get; }
        public List<string> DependsOn { get; }
        public List<string> ExclusiveWith { get; }
        public bool Required { get; set; }
        public int? ExecutionOrder { get; set; }
        public List<IOption> Options { get; }
        public int? ArgumentIndex { get; set; }
        public Type ClassType { get; set; }
        public string HelpText { get; set; }
        public SourceType Source { get; set; }

        public Verb()
        {
            this.LongNames = new List<string>();
            this.DependsOn = new List<string>();
            this.ExclusiveWith = new List<string>();
            this.Options = new List<IOption>();
        }

        public IVerb Clone()
        {
            var newVerb = new Verb()
            {
                ShortName = this.ShortName,
                Required = this.Required,
                ExecutionOrder = this.ExecutionOrder,
                ArgumentIndex = this.ArgumentIndex,
                ClassType = this.ClassType,
                HelpText = this.HelpText
            };

            newVerb.LongNames.AddRange(this.LongNames);
            newVerb.DependsOn.AddRange(this.DependsOn);
            newVerb.ExclusiveWith.AddRange(this.ExclusiveWith);
            newVerb.Options.AddRange(this.Options);

            return newVerb;
        }
    }
}
