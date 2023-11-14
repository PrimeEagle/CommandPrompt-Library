using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace VNet.CommandLine
{
    [ExcludeFromCodeCoverage]
    public class NullAssembly : IAssembly
    {
        public ICollection<Type> Types => null;


        public Type[] GetTypes()
        {
            return null;
        }

        public Assembly GetExecutingAssembly()
        {
            return null;
        }
    }
}