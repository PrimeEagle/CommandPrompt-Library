using System;

namespace VNet.CommandLineTest
{
    internal class ConfigTestOptionParameter
    {
        internal string ShortName { get; init; }
        internal string[] LongNames { get; init; }
        internal string[] PropertyNames { get; init; }
        internal object[] PropertyValues { get; init; }
        internal Type DataType { get; init; }
    }
}
