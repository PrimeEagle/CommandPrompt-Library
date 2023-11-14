using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentValidation;

namespace VNet.CommandLine.Validation.FluentValidation
{
    public class CommandVerbAttributeCollectionValidator : AbstractValidator<ICommandVerbAttributeCollection>, ICommandValidator<ICommandVerbAttributeCollection>
    {
        public CommandVerbAttributeCollectionValidator()
        {
            RuleForEach(v => v.VerbAttributes).SetValidator(new CommandVerbAttributeValidator());

            // verb names not duplicated
            RuleFor(v => v)
                .Must(n => n.VerbAttributes
                    .AllVerbNames()
                    .Where(s => !string.IsNullOrEmpty(s))
                    .GroupBy(g => g)
                    .All(x => x.Count() <= 1))
                .WithName("ShortName and LongName")
                .WithErrorCode("CLCVC0001")
                .WithMessage("Verb names cannot be duplicated");

            // each execution order can only be used once
            RuleFor(v => v)
                .Must(n => n.VerbAttributes
                    .Where(e => e.ExecutionOrder > 0)
                    .Select(e => e.ExecutionOrder)
                    .GroupBy(g => g)
                    .All(x => x.Count() <= 1))
                .WithName("ExecutionOrder")
                .WithErrorCode("CLCVC0002")
                .WithMessage("Execution orders cannot be duplicated");

            // verb must depend on items that exist
            RuleFor(c => c)
                .Must(v => v.VerbAttributes.All(d => d.DependsOn
                    .AllExistsIn(v.VerbAttributes.VerbShortNames())))
                .WithName("DependsOn")
                .WithErrorCode("CLCVC0003")
                .WithMessage("All DependsOn verb values must exist");

            // verbs must be exclusive to items that
            RuleFor(c => c)
                .Must(v => v.VerbAttributes.All(d => d.ExclusiveWith
                    .AllExistsIn(v.VerbAttributes.VerbShortNames())))
                .WithName("ExclusiveWith")
                .WithErrorCode("CLCVC0004")
                .WithMessage("All ExclusiveWith verb values must exist");
        }

        [ExcludeFromCodeCoverage]
        public ValidationState DoValidate(ICommandVerbAttributeCollection item, string[] ruleSets = null)
        {
            var v = new CommandVerbAttributeCollectionValidator();

            var result = ruleSets is null ? v.Validate(item) : v.Validate(item, options => { options.IncludeRuleSets(ruleSets); });

            return result.ConvertToValidationState(ErrorCategory.Configuration);
        }
    }
}