using System;

namespace VNet.CommandLine
{
    public interface ICommandOptionAttribute
    {
        string ShortName { get; set; }
        string[] LongNames { get; set; }
        Type DataType { get; set; }
        bool Required { get; set; }
        string[] DependsOn { get; set; }
        string[] ExclusiveWith { get; set; }
        string HelpText { get; set; }
        bool NeedsValue { get; set; }
        bool AllowDuplicates { get; set; }
        bool AllowNullValue { get; set; }
    }
}
