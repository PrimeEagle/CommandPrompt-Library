using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VNet.Utility
{
    public static class ReflectionUtility
    {
        public static IEnumerable<Type> ClassesWithAttribute<T>(this Assembly assembly) where T : Attribute
        {
            return assembly.GetTypes()
                .Where(m => m.GetCustomAttributes(typeof(T), false).Length > 0);
        }
    }
}
