using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace VNet.CommandLine
{
    [ExcludeFromCodeCoverage]
    public class AssemblyWrapper : IAssembly
    {
        private readonly Assembly _assembly;

        public ICollection<Type> Types => _assembly.GetTypes().ToList();


        public AssemblyWrapper(Assembly assembly)
        {
            _assembly = assembly;
        }

        public Type[] GetTypes()
        {
            return _assembly.GetTypes();
        }

        public Assembly GetExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}