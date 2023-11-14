using System;
using System.Collections.Generic;
using System.Reflection;

namespace VNet.CommandLine
{
    public interface IAssembly
    {
        ICollection<Type> Types { get; }

        // ReSharper disable once ReturnTypeCanBeEnumerable.Global
        public Type[] GetTypes();
        public Assembly GetExecutingAssembly();
    }
}
