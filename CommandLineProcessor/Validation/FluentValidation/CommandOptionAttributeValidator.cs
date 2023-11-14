using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentValidation;

namespace VNet.CommandLine.Validation.FluentValidation
{
    public class CommandOptionAttributeValidator : AbstractValidator<ICommandOptionAttribute>, ICommandValidator<ICommandOptionAttribute>
    {
        public CommandOptionAttributeValidator()
        {
            // options must have a valid data type
            RuleFor(opt => opt.DataType)
                .Must(o => DefaultValues.AllowedDataTypes.Contains(o) || o.IsArray || o.IsEnum)
                .Unless(o => o is null)
                .WithName("DataType")
                .WithErrorCode("CLCOA0001")
                .WithMessage("Data Type for option is not valid");

            // short name not empty
            RuleFor(opt => opt.ShortName)
                .NotEmpty()
                .WithName("ShortName")
                .WithErrorCode("CLCOA0002")
                .WithMessage("Option {PropertyName} cannot be empty");

            // long name not empty
            RuleForEach(opt => opt.LongNames)
                .NotEmpty()
                .WithName("LongNames")
                .WithErrorCode("CLCOA0003")
                .WithMessage("Option {PropertyName} cannot be empty");

            // short name not a reserved option
            RuleFor(opt => opt.ShortName)
                .Must(o => !DefaultValues.ReservedVerbs.ToList().Contains(o))
                .WithName("ShortName")
                .WithErrorCode("CLCOA0004")
                .WithMessage("Option {PropertyName} cannot be a reserved word");

            // long name not a reserved option
            RuleForEach(opt => opt.LongNames)
                .Must(o => !DefaultValues.ReservedVerbs.ToList().Contains(o))
                .WithName("LongNames")
                .WithErrorCode("CLCOA0005")
                .WithMessage("Option {PropertyName} cannot be a reserved word");

            // option cannot depend on itself
            RuleFor(opt => opt)
                .Must(o => !o.DependsOn.Contains(o.ShortName))
                .WithName("DependsOn")
                .WithErrorCode("CLCOA0006")
                .WithMessage("An option with DependsOn cannot refer to itself");

            // option cannot be exclusive with itself
            RuleFor(opt => opt)
                .Must(o => !o.ExclusiveWith.Contains(o.ShortName))
                .WithName("ExclusiveWith")
                .WithErrorCode("CLCOA0007")
                .WithMessage("An option with ExclusiveWith cannot refer to itself");

            // short names cannot contain special characters
            RuleFor(opt => opt.ShortName)
                .Must(o => !o.Any(n => DefaultValues.SpecialCharacters.Where(s => !string.IsNullOrEmpty(s)).Any(o.Contains)))
                .WithName("ShortName")
                .WithErrorCode("CLCOA0008")
                .WithMessage("Option ShortNames cannot contain special characters");

            // long names cannot contain special characters
            RuleForEach(opt => opt.LongNames)
                .Must(o => !o.Any(n => DefaultValues.SpecialCharacters.Where(s => !string.IsNullOrEmpty(s)).Any(o.Contains)))
                .WithName("LongName")
                .WithErrorCode("CLCOA0009")
                .WithMessage("Option LongNames cannot contain special characters");

            // short names cannot start with option prefixes
            RuleFor(opt => opt.ShortName)
                .Must(o => !o.Any(n => DefaultValues.OptionPrefixes.Where(s => !string.IsNullOrEmpty(s)).Any(o.StartsWith)))
                .WithName("ShortName")
                .WithErrorCode("CLCOA0010")
                .WithMessage("Option ShortNames cannot start with option prefixes");

            //long names cannot start with option prefixes
            RuleForEach(opt => opt.LongNames)
                .Must(o => !o.Any(n => DefaultValues.OptionPrefixes.Where(s => !string.IsNullOrEmpty(s)).Any(o.StartsWith)))
                .WithName("ShortName")
                .WithErrorCode("CLCOA0011")
                .WithMessage("Option ShortNames cannot start with option prefixes");

            // short names cannot start with verb prefixes
            RuleFor(opt => opt.ShortName)
                .Must(o => !o.Any(n => DefaultValues.VerbPrefixes.Where(s => !string.IsNullOrEmpty(s)).Any(o.StartsWith)))
                .WithName("ShortName")
                .WithErrorCode("CLCOA0012")
                .WithMessage("Option ShortNames cannot start with verb prefixes");

            //long names cannot start with verb prefixes
            RuleForEach(opt => opt.LongNames)
                .Must(o => !o.Any(n => DefaultValues.VerbPrefixes.Where(s => !string.IsNullOrEmpty(s)).Any(o.StartsWith)))
                .WithName("ShortName")
                .WithErrorCode("CLCOA0013")
                .WithMessage("Option ShortNames cannot start with verb prefixes");

            //option needs value in order to allow null value
            RuleFor(opt => opt)
                .Must(o => o.NeedsValue || !o.AllowNullValue)
                .WithName("NeedsValue and AllowNullValue")
                .WithErrorCode("CLCOA0014")
                .WithMessage("Option cannot allow null value if it does not need a value");
        }

        [ExcludeFromCodeCoverage]
        public ValidationState DoValidate(ICommandOptionAttribute item, string[] ruleSets = null)
        {
            var v = new CommandOptionAttributeValidator();

            var result = ruleSets is null ? v.Validate(item) : v.Validate(item, options => { options.IncludeRuleSets(ruleSets); });

            return result.ConvertToValidationState(ErrorCategory.Configuration);
        }
    }
}