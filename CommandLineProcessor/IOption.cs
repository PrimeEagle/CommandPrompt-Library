using System;
using System.Collections.Generic;
using VNet.CommandLine.Validation;

namespace VNet.CommandLine
{
    public interface IOption
    {
        string ShortName { get; set; }
        List<string> LongNames { get; }
        List<string> DependsOn { get; }
        List<string> ExclusiveWith { get; }
        bool Required { get; set; }
        Type DataType { get; set; }
        object DataValue { get; set; }
        int? ArgumentIndex { get; set; }
        bool NeedsValue { get; set; }
        Type ClassType { get; set; }
        string HelpText { get; set; }
        bool AllowDuplicates { get; set; }
        bool AllowNullValue { get; set; }
        SourceType Source { get; set; }
        IVerb ParentVerb { get; set; }

        IOption Clone();
    }
}
