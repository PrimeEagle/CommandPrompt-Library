using System;
using System.Collections.Generic;

namespace VNet.CommandLine
{
    public class Option : IOption
    {
        public string ShortName { get; set; }
        public List<string> LongNames { get;  }
        public List<string> DependsOn { get;  }
        public List<string> ExclusiveWith { get;  }
        public bool Required { get; set; }
        public Type DataType { get; set; }
        public object DataValue { get; set; }
        public int? ArgumentIndex { get; set; }
        public bool NeedsValue { get; set; }
        public Type ClassType { get; set; }
        public string HelpText { get; set; }
        public bool AllowDuplicates { get; set; }
        public bool AllowNullValue { get; set; }
        public SourceType Source { get; set; }
        public IVerb ParentVerb { get; set; }


        public Option()
        {
            this.LongNames = new List<string>();
            this.DependsOn = new List<string>();
            this.ExclusiveWith = new List<string>();
        }

        public IOption Clone()
        {
            var newOption = new Option()
            {
                ShortName = this.ShortName,
                Required = this.Required,
                DataType = this.DataType,
                DataValue = this.DataValue,
                ArgumentIndex = this.ArgumentIndex,
                NeedsValue = this.NeedsValue,
                ClassType = this.ClassType,
                HelpText = this.HelpText,
                AllowDuplicates = this.AllowDuplicates,
                AllowNullValue = this.AllowNullValue,
                Source = this.Source,
                ParentVerb = this.ParentVerb
            };

            newOption.LongNames.AddRange(this.LongNames);
            newOption.DependsOn.AddRange(this.DependsOn);
            newOption.ExclusiveWith.AddRange(this.ExclusiveWith);

            return newOption;
        }
    }
}
