using System;
using System.Collections.Generic;
using System.Linq;
using VNet.CommandLine.Validation;
using VNet.CommandLine.Validation.FluentValidation;

namespace VNet.CommandLine
{
    public class DefaultParser : IParser
    {
        public ParsedVerbsResult Parse(string[] args, ICollection<IVerb> configuredVerbs, IValidatorManager validator, bool allowUnknownOptions)
        {
            var normalizedArgs = NormalizeArguments(args);
            var result = ParseVerbs(normalizedArgs, configuredVerbs.ToList(), validator, allowUnknownOptions);

            return result;
        }

        private string[] NormalizeArguments(string[] args)
        {
            var joinedArgs = string.Join(' ', args);

            foreach (var s in DefaultValues.OptionValueSeparators)
            {
                joinedArgs = joinedArgs.Replace(", ", ",").Replace("; ", ";").Replace($" {s}", $"{s}").Replace($"{s} ", $"{s}");
            }

            return joinedArgs.Split(' ');
        }

        private ParsedVerbsResult ParseVerbs(string[] args, List<IVerb> configuredVerbs, IValidatorManager validator, bool allowUnknownOptions)
        {
            var addedVerbs = new List<IVerb>();
            var unknownOptions = new List<string>();
            var errorMessages = new List<ValidationError>();
            var result = new ParsedVerbsResult();
            var lastIndexProcessed = -1;
            IVerb previousVerb = null;

            for (var i = 0; i < args.Length; i++)
            {
                if (lastIndexProcessed >= i) continue;

                var verb = Utility.FindVerb(args[i], configuredVerbs);

                if (verb == null)
                {
                    // parse as an option

                    if (previousVerb == null) continue;

                    var optionResult = ParseOption(args, i, previousVerb, validator);

                    lastIndexProcessed = optionResult.LastIndexProcessed;
                    unknownOptions.AddRange(optionResult.UnknownOptions);
                    errorMessages.AddRange(optionResult.ValidationState.Errors);
                }
                else
                {
                    // parse as a verb

                    if (verb.ArgumentIndex.HasValue)
                    {
                        var newVerb = verb.Clone();
                        newVerb.ArgumentIndex = i;
                        newVerb.Source = SourceType.CommandLineArguments;

                        addedVerbs.Add(newVerb);
                    }
                    else
                    {
                        verb.ArgumentIndex = i;
                    }

                    previousVerb = verb;
                }
            }

            var uo = new UnknownOptions();
            uo.ShortNames = unknownOptions;
            uo.AllowUnknownOptions = allowUnknownOptions;

            var unknownOptionsValidator = (UnknownOptionsValidator)validator.ValidatorParameters.AdditionalValidators.Validators[2];
            var unknownValidationState = unknownOptionsValidator.DoValidate(uo);
            result.ValidationState.Errors.AddRange(unknownValidationState.Errors);

            configuredVerbs.AddRange(addedVerbs);
            result.Verbs = configuredVerbs;
            result.ValidationState.Errors.AddRange(errorMessages);

            return result;
        }

        private ParseOptionResult ParseOption(string[] args, int index, IVerb verb, IValidatorManager validator)
        {
            var unknownOptions = new List<string>();
            var errorMessages = new List<ValidationError>();

            var result = new ParseOptionResult
            {
                LastIndexProcessed = index
            };

            var arg = args[index];
            var lastIndex = -1;


            var option = Utility.FindOption(arg, verb.Options);

            if (option == null)
            {
                unknownOptions.Add(arg);
            }
            else
            {
                if (option.ArgumentIndex.HasValue)
                {
                    var newOption = option.Clone();
                    newOption.ArgumentIndex = index;
                    newOption.Source = SourceType.CommandLineArguments;

                    if (newOption.NeedsValue)
                    {
                        if (DefaultValues.OptionValueSeparators.Any(ovs => arg.Contains(ovs)))
                        {
                            var vp = new ArgumentValuePair()
                            {
                                ArgumentValue = arg
                            };

                            var vpValidator = (ArgumentValuePairValidator)validator.ValidatorParameters.AdditionalValidators.Validators[1];
                            var validationState = vpValidator.DoValidate(vp);

                            result.ValidationState.Errors.AddRange(validationState.Errors);

                            if (validationState.Valid)
                            {
                                foreach (var s in DefaultValues.OptionValueSeparators)
                                {
                                    if (!arg.Contains(s)) continue;

                                    var value = arg.After(s);
                                    if (newOption.DataType == typeof(Array))
                                    {
                                        var av = new ArgumentArrayValue()
                                        {
                                            ArrayString = value
                                        };

                                        var avValidator = (ArgumentArrayValueValidator)validator.ValidatorParameters.AdditionalValidators.Validators[0];
                                        var avValidationState = avValidator.DoValidate(av);

                                        result.ValidationState.Errors.AddRange(avValidationState.Errors);

                                        if (!result.ValidationState.Valid) continue;

                                        var delimiter =
                                            DefaultValues.OptionValueArrayDelimiters.First(d =>
                                                value.Contains(d));

                                        newOption.DataValue = value.Substring(1, value.Length - 2)
                                            .Split(delimiter);
                                    }
                                    else
                                    {
                                        newOption.DataValue = string.IsNullOrEmpty(value.ToString())
                                            ? null
                                            : value;
                                    }

                                    break;
                                }
                            }
                        }
                        else if (args.Length > index + 1)
                        {
                            newOption.DataValue = args[index + 1];
                            lastIndex = index + 1;
                        }
                    }

                    verb.Options.Add(newOption);
                }
                else
                {
                    option.ArgumentIndex = index;
                    lastIndex = index;

                    if (DefaultValues.OptionValueSeparators.Any(ovs => arg.Contains(ovs)))
                    {
                        var vp = new ArgumentValuePair()
                        {
                            ArgumentValue = arg
                        };

                        var vpValidator = (ArgumentValuePairValidator)validator.ValidatorParameters.AdditionalValidators.Validators[1];
                        var validationState = vpValidator.DoValidate(vp);
                        
                        result.ValidationState.Errors.AddRange(validationState.Errors);

                        if (validationState.Valid)
                        {
                            foreach (var s in DefaultValues.OptionValueSeparators)
                            {
                                if (!arg.Contains(s)) continue;

                                var value = arg.After(s);
                                if (option.DataType == typeof(Array))
                                {
                                    var av = new ArgumentArrayValue()
                                    {
                                        ArrayString = value
                                    };

                                    var avValidator = (ArgumentArrayValueValidator)validator.ValidatorParameters.AdditionalValidators.Validators[0];
                                    var avValidationState = avValidator.DoValidate(av);

                                    result.ValidationState.Errors.AddRange(avValidationState.Errors);

                                    if (!result.ValidationState.Valid) continue;

                                    var delimiter =
                                        DefaultValues.OptionValueArrayDelimiters.First(d =>
                                            value.Contains(d));

                                    option.DataValue = value.Substring(1, value.Length - 2)
                                        .Split(delimiter);
                                }
                                else
                                {
                                    option.DataValue = string.IsNullOrEmpty(value.ToString())
                                        ? null
                                        : value;
                                }

                                break;
                            }
                        }
                    }
                    else if (option.NeedsValue && args.Length > index + 1)
                    {
                        option.DataValue = args[index + 1];
                        lastIndex = index + 1;
                    }
                }
            }

            result.LastIndexProcessed = lastIndex;
            result.UnknownOptions = unknownOptions;
            result.ValidationState.Errors.AddRange(errorMessages);

            return result;
        }
    }
}
