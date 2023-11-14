using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VNet.CommandLine.Validation;
using Microsoft.Extensions.Logging;

namespace VNet.CommandLine
{
    public class CommandLineManager
    {
        public bool AllowUnknownOptions { get; set; }
        public bool LogConfigurationErrors { get; set; }
        public bool LogUsageErrors { get; set; }
        public bool DisplayHelpOnConfigurationErrors { get; set; }
        public bool DisplayHelpOnUsageErrors { get; set; }
        public bool DisplayConfigurationErrorMessages { get; set; }
        public bool DisplayUsageErrorMessages { get; set; }
        public bool StopIfConfigurationErrors { get; set; }
        public string ExecuteMethodName { get; set; }


        public ValidationState Validity { get; private set; }

        public CommandLineManager()
        {
            InitializeDefaults();
        }

        private void InitializeDefaults()
        {
            this.AllowUnknownOptions = true;
            this.LogConfigurationErrors = true;
            this.LogUsageErrors = false;
            this.DisplayHelpOnConfigurationErrors = false;
            this.DisplayHelpOnUsageErrors = true;
            this.DisplayConfigurationErrorMessages = true;
            this.DisplayUsageErrorMessages = true;
            this.StopIfConfigurationErrors = true;
            this.ExecuteMethodName = "Execute";
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public void Process(string[] args, IAssembly assembly, IEnumerable<TextWriter> streams, ILogger logger,
            IBuilder config, IParser parser, IDisplayer display, IValidatorManager validator)
        {
            var commands = config.Load(new object[] { assembly });
            var parsedResult = parser.Parse(args, commands, validator, this.AllowUnknownOptions);

            validator.Assembly = assembly;

            this.Validity = validator.Validate(parsedResult, this.AllowUnknownOptions, this.StopIfConfigurationErrors);

            if (this.Validity.Valid)
            {
                ExecuteVerbs(commands);
            }
            else
            {
                if (this.Validity.HasConfigurationErrors)
                {
                    if (this.DisplayConfigurationErrorMessages)
                        display.ShowErrors(streams, logger, this.Validity.ConfigurationErrorMessages, this.LogConfigurationErrors);

                    if (this.DisplayHelpOnConfigurationErrors)
                        display.ShowHelp(streams, logger, commands);
                }

                if (this.Validity.HasUsageErrors && !(this.Validity.HasConfigurationErrors && this.StopIfConfigurationErrors))
                {
                    if (this.DisplayUsageErrorMessages)
                        display.ShowErrors(streams, logger, this.Validity.UsageErrorMessages, this.LogUsageErrors);

                    if (this.DisplayHelpOnUsageErrors)
                        display.ShowHelp(streams, logger, commands);
                }
            }
        }

        private void ExecuteVerbs(ICollection<IVerb> commands)
        {
            foreach (var ex in commands.OrderForExecution())
            {
                var methodInfo = ex?.ClassType.GetMethod(this.ExecuteMethodName);

                if (methodInfo != null)
                {
                    var classInstance = Activator.CreateInstance(ex.ClassType, null);

                    var paramDictionary = BuildParameters(ex);
                    var parametersArray = new object[] { paramDictionary };

                    methodInfo.Invoke(classInstance, parametersArray);
                }
            }
        }

        private static ParameterDictionary BuildParameters(IVerb command)
        {
            var paramDictionary = new ParameterDictionary();

            foreach (var p in command.Options.Select(o => new OptionParameter
            {
                ShortName = o.ShortName,
                LongNames = o.LongNames,
                DataType = o.DataType,
                DataValue = o.DataValue
            }))
            {
                paramDictionary.Add(p);
            }

            return paramDictionary;
        }
    }
}