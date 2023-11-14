using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VNet.CommandLine
{
    [ExcludeFromCodeCoverage]
    public static class DefaultValues
    {
        public static IEnumerable<string> ReservedVerbs { get; } = new List<string> { "h", "help", "?", "v", "version" };
        public static IEnumerable<string> VerbPrefixes { get; } = new List<string> { "", "-", "--", "/", "\\" };
        public static IEnumerable<string> OptionPrefixes { get; } = new List<string> { "", "-", "--" };
        public static IEnumerable<string> OptionValueSeparators { get; } = new List<string> { "=", ":" };
        public static IEnumerable<string> OptionValueArrayStartDelimiters { get; } = new List<string> { "'", "\"", "[" };
        public static IEnumerable<string> OptionValueArrayEndDelimiters { get; } = new List<string> { "'", "\"", "]" };
        public static IEnumerable<string> OptionValueArrayDelimiters { get; } = new List<string> { ",", ";", "\t" };

        public static IEnumerable<Type> AllowedDataTypes { get; } = new List<Type> { typeof(string), typeof(String), typeof(char), typeof(bool),
                                                                                typeof(Int16), typeof(Int32), typeof(Int64),
                                                                                typeof(short), typeof(int), typeof(long),
                                                                                typeof(Single), typeof(float), typeof(decimal), typeof(double),
                                                                                typeof(DateTime), typeof(Enum), typeof(Array) };

        public static IEnumerable<string> SpecialCharacters
        {
            get
            {
                var specials = DefaultValues.VerbPrefixes.SelectMany(v => v.Where(n => !char.IsLetterOrDigit(n)))
                    .Concat(DefaultValues.OptionPrefixes.SelectMany(v => v.Where(n => !char.IsLetterOrDigit(n))))
                    .Concat(DefaultValues.OptionValueArrayStartDelimiters.SelectMany(v => v.Where(n => !char.IsLetterOrDigit(n))))
                    .Concat(DefaultValues.OptionValueArrayEndDelimiters.SelectMany(v => v.Where(n => !char.IsLetterOrDigit(n))))
                    .Concat(DefaultValues.OptionValueArrayDelimiters.SelectMany(v => v.Where(n => !char.IsLetterOrDigit(n))))
                    .Concat(DefaultValues.OptionValueSeparators.SelectMany(v => v.Where(n => !char.IsLetterOrDigit(n))));

                return specials.Select(c => c.ToString()).Where(s => s.Length > 0).Distinct().ToList();
            }
        }
    }
}
