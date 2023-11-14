using System;
using System.Diagnostics.CodeAnalysis;

namespace VNet.CommandLine.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    [ExcludeFromCodeCoverage]
    public class CommandVerbAttribute : Attribute, ICommandVerbAttribute
    {
        public string ShortName { get; set; }
        public string[] LongNames { get; set; }
        public string[] DependsOn { get; set; }
        public string[] ExclusiveWith { get; set; }
        public bool Required { get; set; }
        public int ExecutionOrder { get; set; }
        public string HelpText { get; set; }
        public bool FailOnUnknownOptions { get; set; }

        public CommandVerbAttribute(string shortName, string[] longNames)
        {
            this.ShortName = shortName;
            this.LongNames = longNames;

            this.DependsOn = new string[] { };
            this.ExclusiveWith = new string[] { };
        }

        public CommandVerbAttribute(string shortName, string longName)
        {
            this.ShortName = shortName;
            this.LongNames = new[] { longName };

            this.LongNames = new string[] { };
            this.DependsOn = new string[] { };
            this.ExclusiveWith = new string[] { };
        }
    }
}