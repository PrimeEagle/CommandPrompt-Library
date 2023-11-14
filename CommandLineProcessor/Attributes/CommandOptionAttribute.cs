using System;
using System.Diagnostics.CodeAnalysis;

namespace VNet.CommandLine.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [ExcludeFromCodeCoverage]
    public class CommandOptionAttribute : Attribute, ICommandOptionAttribute
    {
        public string ShortName { get; set; }
        public string[] LongNames { get; set;  }
        public Type DataType { get; set; }
        public bool Required { get; set; }
        public string[] DependsOn { get; set; }
        public string[] ExclusiveWith { get; set; }
        public string HelpText { get; set; }
        public bool NeedsValue { get; set; }
        public bool AllowDuplicates { get; set; }
        public bool AllowNullValue { get; set; }

        public CommandOptionAttribute(string shortName, string[] longNames, Type dataType)
        {
            ShortName = shortName;
            LongNames = longNames;
            DataType = dataType;

            this.DependsOn = new string[] { };
            this.ExclusiveWith = new string[] { };
        }

        public CommandOptionAttribute(string shortName, string longName, Type dataType)
        {
            ShortName = shortName;
            LongNames = new[] { longName };
            DataType = dataType;

            this.LongNames = new string[] { };
            this.DependsOn = new string[] { };
            this.ExclusiveWith = new string[] { };
        }
    }
}