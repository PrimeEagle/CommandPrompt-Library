using System;
using System.Collections.Generic;
using System.Linq;
using VNet.CommandLine;
using VNet.CommandLine.Attributes;

namespace VNet.CommandLineTest
{
    internal static class ConfigTestHelper
    {
        internal static void AddSingleVerbClassNoInterface(IAssembly assembly, string className)
        {
            var attrList = new List<FakeAttributeParams>();

            var verb = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVerbAttribute),
                ConstructorArgs = new object[] { $"short{className}", new string[] { $"long{className}" } },
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var help = new FakeAttributeParams
            {
                AttributeType = typeof(CommandHelpAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var version = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVersionAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            attrList.Add(verb);
            attrList.Add(help);
            attrList.Add(version);

            var classType = FakeAssemblyWrapper.CreateType(className, null, attrList);
            assembly.Types.Add(classType);
        }

        internal static void AddSingleVerbClassWithInterface(IAssembly assembly, string className,
                                                           string shortName = null, string longName = null,
                                                           string[] propertyNames = null, object[] propertyValues = null,
                                                           bool removeDefaultVerbs = false)
        {
            var attrList = new List<FakeAttributeParams>();

            var sName = shortName ?? $"short{className}";
            var lName = longName ?? $"long{className}";

            var verb = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVerbAttribute),
                ConstructorArgs = new object[] { sName, new string[] { lName } },
                ConstructorIndex = 0,
                PropertyNames = propertyNames ?? Array.Empty<string>(),
                PropertyValues = propertyValues ?? Array.Empty<object>()
            };

            var help = new FakeAttributeParams
            {
                AttributeType = typeof(CommandHelpAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var version = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVersionAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            attrList.Add(verb);

            if (!removeDefaultVerbs)
            {
                attrList.Add(help);
                attrList.Add(version);
            }

            var classType = FakeAssemblyWrapper.CreateType(className, typeof(IVerbExecutable), attrList);
            assembly.Types.Add(classType);
        }

        internal static void AddSingleReservedVerbClassShortName(IAssembly assembly, string className)
        {
            var attrList = new List<FakeAttributeParams>();

            var verb = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVerbAttribute),
                ConstructorArgs = new object[] { $"h", new string[] { $"test" } },
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var help = new FakeAttributeParams
            {
                AttributeType = typeof(CommandHelpAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var version = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVersionAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            attrList.Add(verb);
            attrList.Add(help);
            attrList.Add(version);

            var classType = FakeAssemblyWrapper.CreateType(className, typeof(IVerbExecutable), attrList);
            assembly.Types.Add(classType);
        }

        internal static void AddSingleReservedVerbClassLongName(IAssembly assembly, string className)
        {
            var attrList = new List<FakeAttributeParams>();

            var verb = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVerbAttribute),
                ConstructorArgs = new object[] { $"th", new string[] { $"help" } },
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var help = new FakeAttributeParams
            {
                AttributeType = typeof(CommandHelpAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var version = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVersionAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            attrList.Add(verb);
            attrList.Add(help);
            attrList.Add(version);

            var classType = FakeAssemblyWrapper.CreateType(className, typeof(IVerbExecutable), attrList);
            assembly.Types.Add(classType);
        }

        internal static void AddSingleReservedOptionClassShortName(IAssembly assembly, string className)
        {
            var attrList = new List<FakeAttributeParams>();

            var verb = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVerbAttribute),
                ConstructorArgs = new object[] { $"t", new string[] { $"test" } },
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var opt = new FakeAttributeParams
            {
                AttributeType = typeof(CommandOptionAttribute),
                ConstructorArgs = new object[] { $"h", new string[] { $"tst" }, typeof(string) },
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var help = new FakeAttributeParams
            {
                AttributeType = typeof(CommandHelpAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var version = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVersionAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            attrList.Add(verb);
            attrList.Add(opt);
            attrList.Add(help);
            attrList.Add(version);

            var classType = FakeAssemblyWrapper.CreateType(className, typeof(IVerbExecutable), attrList);
            assembly.Types.Add(classType);
        }

        internal static void AddSingleReservedOptionClassLongName(IAssembly assembly, string className)
        {
            var attrList = new List<FakeAttributeParams>();

            var verb = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVerbAttribute),
                ConstructorArgs = new object[] { $"t", new string[] { $"test" } },
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var opt = new FakeAttributeParams
            {
                AttributeType = typeof(CommandOptionAttribute),
                ConstructorArgs = new object[] { $"th", new string[] { $"help" }, typeof(string) },
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var help = new FakeAttributeParams
            {
                AttributeType = typeof(CommandHelpAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var version = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVersionAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            attrList.Add(verb);
            attrList.Add(opt);
            attrList.Add(help);
            attrList.Add(version);

            var classType = FakeAssemblyWrapper.CreateType(className, typeof(IVerbExecutable), attrList);
            assembly.Types.Add(classType);
        }

        internal static void AddSingleVerbWithOptionClass(IAssembly assembly, string className,
                                                        string shortNameVerb = null, string longNameVerb = null,
                                                        string shortNameOpt = null, string longNameOpt = null,
                                                        string[] verbPropertyNames = null, object[] verbPropertyValues = null,
                                                        string[] optPropertyNames = null, object[] optPropertyValues = null,
                                                        Type optDataType = null,
                                                        bool removeDefaultVerbs = false)
        {
            var attrList = new List<FakeAttributeParams>();

            var sName = shortNameVerb ?? $"shortv{className}";
            var lName = longNameVerb ?? $"longv{className}";

            var verb = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVerbAttribute),
                ConstructorArgs = new object[] { sName, new string[] { lName } },
                ConstructorIndex = 0,
                PropertyNames = verbPropertyNames ?? Array.Empty<string>(),
                PropertyValues = verbPropertyValues ?? Array.Empty<object>()
            };

            var sNameOpt = shortNameOpt ?? $"shorto{className}";
            var lNameOpt = longNameOpt ?? $"longo{className}";

            var opt = new FakeAttributeParams
            {
                AttributeType = typeof(CommandOptionAttribute),
                ConstructorArgs = new object[] { sNameOpt, new string[] { lNameOpt }, optDataType ?? typeof(string) },
                ConstructorIndex = 0,
                PropertyNames = optPropertyNames ?? Array.Empty<string>(),
                PropertyValues = optPropertyValues ?? Array.Empty<object>()
            };

            var help = new FakeAttributeParams
            {
                AttributeType = typeof(CommandHelpAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var version = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVersionAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            attrList.Add(verb);
            attrList.Add(opt);

            if (!removeDefaultVerbs)
            {
                attrList.Add(help);
                attrList.Add(version);
            }

            var classType = FakeAssemblyWrapper.CreateType(className, typeof(IVerbExecutable), attrList);
            assembly.Types.Add(classType);
        }

        internal static void AddSingleVerbWithDefaultVerbClass(IAssembly assembly, string className,
                                                             string shortNameVerb = null, string longNameVerb = null,
                                                             string shortNameOpt = null, string longNameOpt = null,
                                                             bool removeDefaultVerbs = false)
        {
            var attrList = new List<FakeAttributeParams>();

            var sName = shortNameVerb ?? $"short{className}";
            var lName = longNameVerb ?? $"long{className}";

            var verb = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVerbAttribute),
                ConstructorArgs = new object[] { sName, new string[] { lName } },
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var sNameOpt = shortNameOpt ?? $"short{className}";
            var lNameOpt = longNameOpt ?? $"long{className}";

            var opt = new FakeAttributeParams
            {
                AttributeType = typeof(CommandOptionAttribute),
                ConstructorArgs = new object[] { sNameOpt, new string[] { lNameOpt }, typeof(string) },
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var def = new FakeAttributeParams
            {
                AttributeType = typeof(CommandDefaultVerbAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var help = new FakeAttributeParams
            {
                AttributeType = typeof(CommandHelpAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var version = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVersionAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            attrList.Add(verb);
            attrList.Add(opt);
            attrList.Add(def);
            
            if (!removeDefaultVerbs)
            {
                attrList.Add(help);
                attrList.Add(version);
            }

            var classType = FakeAssemblyWrapper.CreateType(className, typeof(IVerbExecutable), attrList);
            assembly.Types.Add(classType);
        }

        internal static void AddOptionWithoutVerbClass(IAssembly assembly, string className,
                                                     string shortNameOpt = null, string longNameOpt = null)
        {
            var attrList = new List<FakeAttributeParams>();

            var sNameOpt = shortNameOpt ?? $"short{className}";
            var lNameOpt = longNameOpt ?? $"long{className}";

            var opt = new FakeAttributeParams
            {
                AttributeType = typeof(CommandOptionAttribute),
                ConstructorArgs = new object[] { sNameOpt, new string[] { lNameOpt }, typeof(string) },
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var help = new FakeAttributeParams
            {
                AttributeType = typeof(CommandHelpAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var version = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVersionAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            attrList.Add(opt);
            attrList.Add(help);
            attrList.Add(version);

            var classType = FakeAssemblyWrapper.CreateType(className, typeof(IVerbExecutable), attrList);
            assembly.Types.Add(classType);
        }

        internal static void AddSingleVerbForDefaultVerbs(IAssembly assembly, string className,
                                                   bool includeHelp, bool includeVersion,
                                                   string shortName = null, string longName = null,
                                                   string[] propertyNames = null, object[] propertyValues = null)
        {
            var attrList = new List<FakeAttributeParams>();

            var sName = shortName ?? $"short{className}";
            var lName = longName ?? $"long{className}";

            var verb = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVerbAttribute),
                ConstructorArgs = new object[] { sName, new string[] { lName } },
                ConstructorIndex = 0,
                PropertyNames = propertyNames ?? Array.Empty<string>(),
                PropertyValues = propertyValues ?? Array.Empty<object>()
            };

            var help = new FakeAttributeParams
            {
                AttributeType = typeof(CommandHelpAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var version = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVersionAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            attrList.Add(verb);

            if (includeHelp) attrList.Add(help);
            if (includeVersion) attrList.Add(version);

            var classType = FakeAssemblyWrapper.CreateType(className, typeof(IVerbExecutable), attrList);
            assembly.Types.Add(classType);
        }

        internal static void AddSingleVerbWithMultipleOptionClasses(IAssembly assembly, string className,
                                                                    string shortNameVerb, string longNameVerb,
                                                                    string[] verbPropertyNames, object[] verbPropertyValues,
                                                                    List<ConfigTestOptionParameter> optionParameters,
                                                                    bool removeDefaultVerbs = false)
        {
            var verb = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVerbAttribute),
                ConstructorArgs = new object[] { shortNameVerb, new string[] { longNameVerb } },
                ConstructorIndex = 0,
                PropertyNames = verbPropertyNames ?? Array.Empty<string>(),
                PropertyValues = verbPropertyValues ?? Array.Empty<object>()
            };

            var attrList = optionParameters.Select(option => new FakeAttributeParams
                {
                    AttributeType = typeof(CommandOptionAttribute),
                    ConstructorArgs = new object[] {option.ShortName, option.LongNames, option.DataType ?? typeof(string)},
                    ConstructorIndex = 0,
                    PropertyNames = option.PropertyNames ?? Array.Empty<string>(),
                    PropertyValues = option.PropertyValues ?? Array.Empty<object>()
                })
                .ToList();

            var help = new FakeAttributeParams
            {
                AttributeType = typeof(CommandHelpAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var version = new FakeAttributeParams
            {
                AttributeType = typeof(CommandVersionAttribute),
                ConstructorArgs = Array.Empty<object>(),
                ConstructorIndex = 0,
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            attrList.Add(verb);
            

            if (!removeDefaultVerbs)
            {
                attrList.Add(help);
                attrList.Add(version);
            }

            var classType = FakeAssemblyWrapper.CreateType(className, typeof(IVerbExecutable), attrList);
            assembly.Types.Add(classType);
        }

        internal static void CreateValidConfiguration(IAssembly assembly)
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(assembly, "CopyCommand", "c", "copy", "s", "source", new string[] { "Required", "DependsOn" }, new object[] { true, new string[] { "i" } });
            ConfigTestHelper.AddSingleVerbWithOptionClass(assembly, "MoveCommand", "m", "move", "t", "target", null, null, new string[] {"Required"}, new object[] {true}, null,  true);
            ConfigTestHelper.AddSingleVerbWithOptionClass(assembly, "InfoCommand", "i", "info", "d", "details", new string[] { "ExclusiveWith" }, new object[] { new string[] { "e" } }, new string[] {"AllowDuplicates" }, new object[] { true }, null, true);
            ConfigTestHelper.AddSingleVerbWithOptionClass(assembly, "ExtractCommand", "e", "extract", null, null, null, null, null, null, null, true);

            var saveOptions = new List<ConfigTestOptionParameter>();

            var option1 = new ConfigTestOptionParameter()
            {
                ShortName = "so1",
                LongNames = new string[] { "saveoption1" },
                PropertyNames = new [] { "DependsOn", "ExclusiveWith" },
                PropertyValues = new object[] { new[] {"so2"}, new[] {"so3"} }
            };

            var option2 = new ConfigTestOptionParameter()
            {
                ShortName = "so2",
                LongNames = new string[] { "saveoption2" },
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var option3 = new ConfigTestOptionParameter()
            {
                ShortName = "so3",
                LongNames = new string[] { "saveoption3" },
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var option4 = new ConfigTestOptionParameter()
            {
                ShortName = "so4",
                LongNames = new string[] { "saveoption4" },
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var option5 = new ConfigTestOptionParameter()
            {
                ShortName = "so5",
                LongNames = new string[] { "saveoption5" },
                PropertyNames = Array.Empty<string>(),
                PropertyValues = Array.Empty<object>()
            };

            var option6 = new ConfigTestOptionParameter()
            {
                ShortName = "so6",
                LongNames = new string[] { "saveoption6" },
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, true }
            };

            var option7 = new ConfigTestOptionParameter()
            {
                ShortName = "so7",
                LongNames = new string[] { "saveoption7" },
                PropertyNames = new string[] { "NeedsValue", "AllowDuplicates" },
                PropertyValues = new object[] { true, true }
            };

            var optionChar = new ConfigTestOptionParameter()
            {
                ShortName = "char",
                LongNames = Array.Empty<string>(),
                DataType = typeof(char),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionString1 = new ConfigTestOptionParameter()
            {
                ShortName = "string1",
                LongNames = Array.Empty<string>(),
                DataType = typeof(string),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionString2 = new ConfigTestOptionParameter()
            {
                ShortName = "string2",
                LongNames = Array.Empty<string>(),
                DataType = typeof(String),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionBool = new ConfigTestOptionParameter()
            {
                ShortName = "bool",
                LongNames = Array.Empty<string>(),
                DataType = typeof(bool),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };


            var optionDateTime = new ConfigTestOptionParameter()
            {
                ShortName = "datetime",
                LongNames = Array.Empty<string>(),
                DataType = typeof(DateTime),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionInt = new ConfigTestOptionParameter()
            {
                ShortName = "int",
                LongNames = Array.Empty<string>(),
                DataType = typeof(int),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionShort = new ConfigTestOptionParameter()
            {
                ShortName = "short",
                LongNames = Array.Empty<string>(),
                DataType = typeof(short),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionLong = new ConfigTestOptionParameter()
            {
                ShortName = "long",
                LongNames = Array.Empty<string>(),
                DataType = typeof(long),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionInt16 = new ConfigTestOptionParameter()
            {
                ShortName = "int16",
                LongNames = Array.Empty<string>(),
                DataType = typeof(Int16),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionInt32 = new ConfigTestOptionParameter()
            {
                ShortName = "int32",
                LongNames = Array.Empty<string>(),
                DataType = typeof(Int32),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionInt64= new ConfigTestOptionParameter()
            {
                ShortName = "int64",
                LongNames = Array.Empty<string>(),
                DataType = typeof(Int64),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionSingle = new ConfigTestOptionParameter()
            {
                ShortName = "single",
                LongNames = Array.Empty<string>(),
                DataType = typeof(Single),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionDouble = new ConfigTestOptionParameter()
            {
                ShortName = "double",
                LongNames = Array.Empty<string>(),
                DataType = typeof(double),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionDecimal = new ConfigTestOptionParameter()
            {
                ShortName = "decimal",
                LongNames = Array.Empty<string>(),
                DataType = typeof(decimal),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionFloat = new ConfigTestOptionParameter()
            {
                ShortName = "float",
                LongNames = Array.Empty<string>(),
                DataType = typeof(float),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionEnum = new ConfigTestOptionParameter()
            {
                ShortName = "enum",
                LongNames = Array.Empty<string>(),
                DataType = typeof(CommandVerbProperty),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue" },
                PropertyValues = new object[] { true, false }
            };

            var optionArray = new ConfigTestOptionParameter()
            {
                ShortName = "array",
                LongNames = Array.Empty<string>(),
                DataType = typeof(Array),
                PropertyNames = new string[] { "NeedsValue", "AllowNullValue", "AllowDuplicates" },
                PropertyValues = new object[] { true, false, true }
            };

            saveOptions.Add(option1);
            saveOptions.Add(option2);
            saveOptions.Add(option3);
            saveOptions.Add(option4);
            saveOptions.Add(option5);
            saveOptions.Add(option6);
            saveOptions.Add(option7);
            saveOptions.Add(optionChar);
            saveOptions.Add(optionString1);
            saveOptions.Add(optionString2);
            saveOptions.Add(optionBool);
            saveOptions.Add(optionDateTime);
            saveOptions.Add(optionInt);
            saveOptions.Add(optionShort);
            saveOptions.Add(optionLong);
            saveOptions.Add(optionInt16);
            saveOptions.Add(optionInt32);
            saveOptions.Add(optionInt64);
            saveOptions.Add(optionSingle);
            saveOptions.Add(optionDouble);
            saveOptions.Add(optionDecimal);
            saveOptions.Add(optionFloat);
            saveOptions.Add(optionEnum);
            saveOptions.Add(optionArray);

            ConfigTestHelper.AddSingleVerbWithMultipleOptionClasses(assembly, "SaveCommand", "sv", "save", null, null, saveOptions, true);
        }
    }
}