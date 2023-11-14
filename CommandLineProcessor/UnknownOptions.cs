using System.Collections.Generic;
using VNet.CommandLine.Validation;

namespace VNet.CommandLine
{
    public class UnknownOptions : IUnknownOptions
    {
        public bool AllowUnknownOptions { get; set; }
        public List<string> ShortNames { get; set; }
    }
}
