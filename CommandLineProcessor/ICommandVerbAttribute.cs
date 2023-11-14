using VNet.CommandLine.Validation;

namespace VNet.CommandLine
{
    public interface ICommandVerbAttribute
    {
        string ShortName { get; set; }
        string[] LongNames { get; set; }
        string[] DependsOn { get; set; }
        string[] ExclusiveWith { get; set; }
        bool Required { get; set; }
        int ExecutionOrder { get; set; }
        string HelpText { get; set; }
        bool FailOnUnknownOptions { get; set; }
    }
}
