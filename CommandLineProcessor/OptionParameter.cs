using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace VNet.CommandLine
{
    [ExcludeFromCodeCoverage]
    public class OptionParameter
    {
        public string ShortName { get; init; }
        public IList<string> LongNames { get; set; }
        public Type DataType { get; init; }
        public object DataValue { get; init; }
    }
}
