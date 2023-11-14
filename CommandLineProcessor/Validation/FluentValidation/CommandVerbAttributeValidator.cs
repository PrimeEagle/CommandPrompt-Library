using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentValidation;

namespace VNet.CommandLine.Validation.FluentValidation
{
    public class CommandVerbAttributeValidator : AbstractValidator<ICommandVerbAttribute>, ICommandValidator<ICommandVerbAttribute>
    {
        public CommandVerbAttributeValidator()
        {
            // short name not empty
            RuleFor(verb => verb.ShortName)
                .NotEmpty()
                .WithErrorCode("CLCVA0001")
                .WithMessage("Verb {PropertyName} cannot be empty");

            // long name not empty
            RuleForEach(verb => verb.LongNames)
                .NotEmpty()
                .WithErrorCode("CLCVA0002")
                .WithMessage("Verb {PropertyName} cannot be empty");

            // short name not a reserved verb
            RuleFor(verb => verb.ShortName)
                .Must(n => !DefaultValues.ReservedVerbs.Contains(n))
                .WithName("ShortName")
                .WithErrorCode("CLCVA0003")
                .WithMessage("Verb {PropertyName} cannot be a reserved word");

            // long name not a reserved verb
            RuleForEach(verb => verb.LongNames)
                .Must(n => !DefaultValues.ReservedVerbs.Contains(n))
                .WithName("LongName")
                .WithErrorCode("CLCVA0004")
                .WithMessage("Verb {PropertyName} cannot be a reserved word");

            //verb cannot depend on itself
            RuleFor(verb => verb)
                .Must(v => !v.DependsOn.Contains(v.ShortName))
                .WithName("DependsOn")
                .WithErrorCode("CLCVA0005")
                .WithMessage("A verb with DependsOn cannot refer to itself");

            //verb cannot be exclusive with itself
            RuleFor(verb => verb)
                .Must(n => !n.ExclusiveWith.Contains(n.ShortName))
                .WithName("ExclusiveWith")
                .WithErrorCode("CLCVA0006")
                .WithMessage("A verb with ExclusiveWith cannot refer to itself");

            // short names cannot contain special characters
            RuleFor(verb => verb.ShortName)
                .Must(v => !v.Any(n => DefaultValues.SpecialCharacters.Where(s => !string.IsNullOrEmpty(s)).Any(v.Contains)))
                .WithName("ShortName")
                .WithErrorCode("CLCVA0007")
                .WithMessage("Verb ShortNames cannot contain special characters");

            // long names cannot contain special characters
            RuleForEach(verb => verb.LongNames)
                .Must(v => !v.Any(n => DefaultValues.SpecialCharacters.Where(s => !string.IsNullOrEmpty(s)).Any(v.Contains)))
                .WithName("LongName")
                .WithErrorCode("CLCVA0008")
                .WithMessage("Verb LongNames cannot contain special characters");

            // short names cannot start with verb prefixes
            RuleFor(verb => verb.ShortName)
                .Must(v => !v.Any(n => DefaultValues.VerbPrefixes.Where(s => !string.IsNullOrEmpty(s)).Any(v.StartsWith)))
                .WithName("ShortName")
                .WithErrorCode("CLCVA0009")
                .WithMessage("Verb ShortNames cannot start with verb prefixes");

            //long names cannot start with verb prefixes
            RuleForEach(verb => verb.LongNames)
                .Must(v => !v.Any(n => DefaultValues.VerbPrefixes.Where(s => !string.IsNullOrEmpty(s)).Any(v.StartsWith)))
                .WithName("ShortName")
                .WithErrorCode("CLCVA0010")
                .WithMessage("Verb ShortNames cannot start with verb prefixes");

            // short names cannot start with option prefixes
            RuleFor(verb => verb.ShortName)
                .Must(v => !v.Any(n => DefaultValues.OptionPrefixes.Where(s => !string.IsNullOrEmpty(s)).Any(v.StartsWith)))
                .WithName("ShortName")
                .WithErrorCode("CLCVA0011")
                .WithMessage("Verb ShortNames cannot start with option prefixes");

            //long names cannot start with option prefixes
            RuleForEach(verb => verb.LongNames)
                .Must(v => !v.Any(n => DefaultValues.OptionPrefixes.Where(s => !string.IsNullOrEmpty(s)).Any(v.StartsWith)))
                .WithName("ShortName")
                .WithErrorCode("CLCVA0012")
                .WithMessage("Verb ShortNames cannot start with option prefixes");
        }

        [ExcludeFromCodeCoverage]
        public ValidationState DoValidate(ICommandVerbAttribute item, string[] ruleSets = null)
        {
            var v = new CommandVerbAttributeValidator();

            var result = ruleSets is null ? v.Validate(item) : v.Validate(item, options => { options.IncludeRuleSets(ruleSets); });

            return result.ConvertToValidationState(ErrorCategory.Configuration);
        }
    }
}