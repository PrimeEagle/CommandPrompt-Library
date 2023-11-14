using System;

namespace VNet.CommandLineTest
{
    internal class FakeAttributeParams
    {
        internal Type AttributeType { get; init; }
        internal int ConstructorIndex { get; init; }
        internal object[] ConstructorArgs { get; init; }
        internal string[] PropertyNames { get; init; }
        internal object[] PropertyValues { get; init; }
    }
}
