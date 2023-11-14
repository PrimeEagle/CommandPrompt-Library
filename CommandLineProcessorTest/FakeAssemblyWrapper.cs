using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using VNet.CommandLine;
using VNet.CommandLine.Attributes;

// ReSharper disable InconsistentNaming

namespace VNet.CommandLineTest
{
    internal class FakeAssemblyWrapper : IAssembly
    {
        public ICollection<Type> Types { get; set; }

        public FakeAssemblyWrapper()
        {
            Types = new List<Type>();
        }

        public Assembly GetExecutingAssembly()
        {
            return null;
        }

        public Type[] GetTypes()
        {
            return Types.ToArray();
        }

        public static Type CreateType(string typeName, Type interfaceType, IEnumerable<FakeAttributeParams> attributes)
        {
            var an = new AssemblyName(typeName);
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            var tb = moduleBuilder.DefineType(typeName,
                      TypeAttributes.NotPublic
                    | TypeAttributes.Class
                    | TypeAttributes.AutoClass
                    | TypeAttributes.AnsiClass
                    | TypeAttributes.BeforeFieldInit
                    | TypeAttributes.AutoLayout
                    , null);
            
            if(interfaceType != null) tb.AddInterfaceImplementation(interfaceType);
            
            if(interfaceType == typeof(IVerbExecutable))
            {
                var mb = 
                         tb.DefineMethod("Execute",
                         MethodAttributes.Public | MethodAttributes.Virtual,
                         typeof(void),
                         new Type[] { typeof(ParameterDictionary)});

                var myMethodIL = mb.GetILGenerator();
                myMethodIL.EmitWriteLine($"Execute called for {typeName}");
                myMethodIL.Emit(OpCodes.Ret);
                var sayHelloMethod = typeof(IVerbExecutable).GetMethod("Execute");
                tb.DefineMethodOverride(mb, sayHelloMethod);
            }

            if (attributes == null) return tb.CreateType();
            foreach(var a in attributes)
            {
                var attrConstructor = a.AttributeType.GetConstructors()[a.ConstructorIndex];

                var piList = new List<PropertyInfo>();
                foreach (var name in a.PropertyNames)
                {
                    if(a.AttributeType == typeof(CommandVerbAttribute))
                    {
                        if (!Enum.TryParse<CommandVerbProperty>(name, out var tempCommandVerbProperty))
                        {
                            throw new InvalidCastException("Property name not valid for type CommandVerbAttribute");
                        }
                    }

                    if(a.AttributeType == typeof(CommandOptionAttribute))
                    {
                        if (!Enum.TryParse<CommandOptionProperty>(name, out var tempCommandOptionProperty))
                        {
                            throw new InvalidCastException("Property name not valid for type CommandOptionAttribute");
                        }
                    }
                        
                    piList.Add(a.AttributeType.GetProperty(name));
                }

                var customAttributeBuilder = new CustomAttributeBuilder(attrConstructor, a.ConstructorArgs, piList.ToArray(), a.PropertyValues);
                tb.SetCustomAttribute(customAttributeBuilder);
            }
            return tb.CreateType();
        }
    }
}
