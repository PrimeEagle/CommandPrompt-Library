using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VNet.CommandLine.Attributes;

namespace VNet.CommandLine
{
    public class DefaultBuilder : IBuilder
    {
        public ICollection<IVerb> Load(object[] parameters)
        {
            var assembly = (IAssembly)parameters[0];

            return GetConfiguredClasses(assembly);
        }

        private List<IVerb> GetConfiguredClasses(IAssembly assembly)
        {
            var result = new List<IVerb>();

            var classes = assembly.GetTypes()
                .Where(m => m.GetCustomAttributes(typeof(CommandVerbAttribute), false).Length > 0)
                .ToArray();

            foreach (var c in classes)
            {
                var attribute = c.GetCustomAttribute<CommandVerbAttribute>();

                result.Add(GetConfiguredVerb(c, attribute));
            }

            return result;
        }

        private IVerb GetConfiguredVerb(Type classType, CommandVerbAttribute verbAttr)
        {
            IVerb cv = new Verb();
            cv.ShortName = verbAttr.ShortName.ToLower();
            cv.LongNames.AddRange(verbAttr.LongNames?.Select(s => s.ToLower()).ToList<string>());
            cv.DependsOn.AddRange(verbAttr.DependsOn?.Select(s => s.ToLower()).ToList<string>());
            cv.ExclusiveWith.AddRange(verbAttr.ExclusiveWith?.Select(s => s.ToLower()).ToList<string>());
            cv.ExecutionOrder = verbAttr.ExecutionOrder == 0 ? (int?)null : verbAttr.ExecutionOrder;
            cv.Required = verbAttr.Required;
            cv.HelpText = verbAttr.HelpText;
            cv.ClassType = classType;
            cv.Source = SourceType.Configuration;

            var optionAttributes = classType.GetCustomAttributes<CommandOptionAttribute>();

            foreach (var oa in optionAttributes)
            {
                var option = GetConfiguredOption(classType, oa);
                option.ParentVerb = cv;

                cv.Options.Add(option);
            }

            return cv;
        }

        private IOption GetConfiguredOption(Type classType, CommandOptionAttribute opAttr)
        {
            IOption co = new Option();

            co.ShortName = opAttr.ShortName;
            co.DataType = opAttr.DataType;
            co.DependsOn.AddRange(opAttr.DependsOn?.Select(s => s.ToLower()).ToList<string>());
            co.ExclusiveWith.AddRange(opAttr.ExclusiveWith?.Select(s => s.ToLower()).ToList<string>());
            co.LongNames.AddRange(opAttr.LongNames?.Select(s => s.ToLower()).ToList<string>());
            co.Required = opAttr.Required;
            co.NeedsValue = opAttr.NeedsValue;
            co.HelpText = opAttr.HelpText;
            co.ClassType = classType;
            co.AllowDuplicates = opAttr.AllowDuplicates;
            co.AllowNullValue = opAttr.AllowNullValue;
            co.Source = SourceType.Configuration;

            return co;
        }
    }
}
