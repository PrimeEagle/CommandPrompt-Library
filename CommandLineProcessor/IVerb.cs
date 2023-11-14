using System;
using System.Collections.Generic;
using VNet.CommandLine.Validation;

namespace VNet.CommandLine
{
    public interface IVerb
    {
        string ShortName { get; set; }
        List<string> LongNames { get; }
        List<string> DependsOn { get; }
        List<string> ExclusiveWith { get; }
        bool Required { get; set; }
        int? ExecutionOrder { get; set; }
        List<IOption> Options { get; }
        int? ArgumentIndex { get; set; }
        Type ClassType { get; set; }
        string HelpText { get; set; }
        SourceType Source { get; set; }


        IVerb Clone();
    }
}
