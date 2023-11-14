using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using VNet.CommandLine.Attributes;

[assembly: InternalsVisibleTo("CommandLineProcessorTest")]
namespace VNet.CommandLine
{
    internal static class Utility
    {
        internal static IEnumerable<IOption> ActiveOptions(this IEnumerable<IOption> optionList)
        {
            var activeList = optionList.Where(x => x.ArgumentIndex.HasValue);

            return activeList;
        }

        internal static IEnumerable<IVerb> ActiveVerbs(this IEnumerable<IVerb> commandList)
        {
            var activeList = commandList.Where(x => x.ArgumentIndex.HasValue);

            return activeList;
        }

        internal static string After(this string str, string searchTerm)
        {
            var idx = str.IndexOf(searchTerm, StringComparison.Ordinal);
            var result = idx >= 0 ? str[(idx + searchTerm.Length)..] : str;

            return result;
        }

        internal static bool AllExistsIn<T>(this IEnumerable<T> sourceList, IEnumerable<T> secondList)
        {
            return sourceList.All(x => secondList.Any(y => x.Equals(y)));
        }

        internal static IEnumerable<string> AllOptionNames(this IAssembly assembly)
        {
            var optionNames = assembly.ClassesWithAttribute<CommandOptionAttribute>()
                .SelectMany(c => c.GetCustomAttributes(typeof(CommandOptionAttribute)))
                .Select(a => (CommandOptionAttribute)a).AllOptionNames();

            return optionNames;
        }

        internal static IEnumerable<string> AllOptionNames(this IEnumerable<ICommandOptionAttribute> options)
        {
            var shortNames = options.Select(n => n.ShortName);
            var longNames = options.SelectMany(n => n.LongNames);

            var result = shortNames.Concat(longNames);

            return result;
        }

        internal static IEnumerable<string> AllOptionNames(this IEnumerable<IOption> optionList)
        {
            var result = optionList.OptionShortNames().Concat(optionList.OptionLongNames());

            return result;
        }

        internal static IEnumerable<string> AllVerbNames(this IAssembly assembly)
        {
            var verbNames = assembly.ClassesWithAttribute<CommandVerbAttribute>()
                .SelectMany(c => c.GetCustomAttributes(typeof(CommandVerbAttribute)))
                .Select(a => (CommandVerbAttribute)a).AllVerbNames();

            return verbNames;
        }

        internal static IEnumerable<string> AllVerbNames(this IEnumerable<ICommandVerbAttribute> verbs)
        {
            var shortNames = verbs.Select(n => n.ShortName);
            var longNames = verbs.SelectMany(n => n.LongNames);

            var result = shortNames.Concat(longNames);

            return result;
        }

        internal static IEnumerable<string> AllVerbNames(this IEnumerable<IVerb> commandList)
        {
            var commandVerbs = commandList.ToList();

            var verbShortNames = commandVerbs.Select(c => c.ShortName);
            var verbLongNames = commandVerbs.SelectMany(c => c.LongNames);

            return verbShortNames.Concat(verbLongNames);
        }

        internal static string Before(this string str, string searchTerm)
        {
            var idx = str.IndexOf(searchTerm, StringComparison.Ordinal);
            var result = idx >= 0 ? str[..idx] : str;

            return result;
        }
        internal static IEnumerable<Type> ClassesWithAttribute<T>(this IAssembly assembly) where T : Attribute
        {
            return assembly.GetTypes()
                .Where(m => m.GetCustomAttributes(typeof(T), false).Length > 0);
        }

        internal static IEnumerable<Type> ClassesWithOptionAttributes(this IAssembly assembly)
        {
            return assembly.ClassesWithAttribute<CommandOptionAttribute>();
        }

        internal static IEnumerable<Type> ClassesWithVerbAttributes(this IAssembly assembly)
        {
            return assembly.ClassesWithAttribute<CommandVerbAttribute>();
        }

        internal static IOption FindOption(string argument, IEnumerable<IOption> configuredOptions)
        {
            IOption resultOption = null;
            var term = argument;

            var idx = -1;
            var found = false;

            foreach (var c in term)
            {
                idx++;
                if (!DefaultValues.OptionValueSeparators.Contains(c.ToString())) continue;

                found = true;
                break;
            }

            if (found && idx >= 0)
            {
                term = argument.Before(argument[idx].ToString());
            }

            foreach (var prefix in DefaultValues.OptionPrefixes)
            {
                foreach (var option in configuredOptions)
                {
                    var names = new List<string> { option.ShortName };
                    names.AddRange(option.LongNames.Select(r => r).Distinct().ToList());

                    foreach (var comboName in names.Select(n => $"{prefix}{n}"))
                    {
                        if (comboName == term)
                        {
                            resultOption = option;
                        }

                        if (resultOption != null) break;
                    }

                    if (resultOption != null) break;
                }

                if (resultOption != null) break;
            }

            return resultOption;
        }

        internal static IVerb FindVerb(string argument, IEnumerable<IVerb> configuredVerbs)
        {
            IVerb resultVerb = null;

            foreach (var prefix in DefaultValues.VerbPrefixes)
            {
                foreach (var verb in configuredVerbs)
                {
                    var names = new List<string> { verb.ShortName };
                    names.AddRange(verb.LongNames.Select(r => r).Distinct().ToList());

                    foreach (var comboName in names.Select(n => $"{prefix}{n}"))
                    {
                        if (comboName == argument)
                        {
                            resultVerb = verb;
                        }

                        if (resultVerb != null) break;
                    }

                    if (resultVerb != null) break;
                }

                if (resultVerb != null) break;
            }

            return resultVerb;
        }

        internal static IEnumerable<CommandOptionAttribute> OptionAttributes(this Type classType)
        {
            return classType.GetCustomAttributes<CommandOptionAttribute>();
        }

        internal static IEnumerable<CommandOptionAttribute> OptionAttributes(this IAssembly assembly)
        {
            return assembly.ClassesWithAttribute<CommandOptionAttribute>()
                .SelectMany(a => a.GetCustomAttributes(typeof(CommandOptionAttribute)))
                .Select(x => (CommandOptionAttribute)x);
        }

        internal static IEnumerable<string> OptionLongNames(this IEnumerable<IOption> optionList)
        {
            var optLongNames = optionList.SelectMany(c => c.LongNames);

            return optLongNames;
        }

        internal static IEnumerable<string> OptionShortNames(this IEnumerable<ICommandOptionAttribute> options)
        {
            var shortNames = options.Select(n => n.ShortName);

            return shortNames;
        }

        //    return verbNames.Concat(optionNames);
        //}
        internal static IEnumerable<string> OptionShortNames(this IEnumerable<IOption> optionList)
        {
            var optShortNames = optionList.Select(c => c.ShortName);

            return optShortNames;
        }

        internal static IEnumerable<IVerb> OrderForDisplay(this IEnumerable<IVerb> commandList)
        {
            var sortedList = commandList.OrderBy(a => a.ShortName);

            // ReSharper disable once PossibleMultipleEnumeration
            foreach (var s in sortedList)
            {
                var tempOptions = s.Options.OrderBy(a => a.ShortName).ToList();

                s.Options.Clear();
                s.Options.AddRange(tempOptions);
            }

            // ReSharper disable once PossibleMultipleEnumeration
            return sortedList;
        }

        internal static IEnumerable<IVerb> OrderForExecution(this IEnumerable<IVerb> commandList)
        {
            var commandVerbs = commandList.ToList();

            var unsorted = commandVerbs.Where(c => !c.ExecutionOrder.HasValue);
            var sortedList = commandVerbs.Where(c => c.ExecutionOrder.HasValue).OrderBy(a => a.ExecutionOrder);

            return sortedList.Concat(unsorted);
        }

        //    return verbShortNames.Concat(verbLongNames).Concat(optionShortNames).Concat(optionLongNames);
        //}
        internal static IEnumerable<CommandVerbAttribute> VerbAttributes(this IAssembly assembly)
        {
            return assembly.ClassesWithAttribute<CommandVerbAttribute>()
                .SelectMany(a => a.GetCustomAttributes(typeof(CommandVerbAttribute)))
                .Select(x => (CommandVerbAttribute)x);
        }

        internal static IEnumerable<string> VerbShortNames(this IEnumerable<ICommandVerbAttribute> verbs)
        {
            var shortNames = verbs.Select(n => n.ShortName);

            return shortNames;
        }
    }
}
