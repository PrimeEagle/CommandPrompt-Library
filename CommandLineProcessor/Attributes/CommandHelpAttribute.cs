using System;
using System.Diagnostics.CodeAnalysis;

namespace VNet.CommandLine.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    [ExcludeFromCodeCoverage]
    public class CommandHelpAttribute : Attribute
    {

    }
}