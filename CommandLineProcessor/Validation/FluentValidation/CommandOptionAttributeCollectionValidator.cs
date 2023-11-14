using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentValidation;

namespace VNet.CommandLine.Validation.FluentValidation
{
    public class CommandOptionAttributeCollectionValidator : AbstractValidator<ICommandOptionAttributeCollection>, ICommandValidator<ICommandOptionAttributeCollection>
    {
        public CommandOptionAttributeCollectionValidator()
        {
            RuleForEach(o => o.OptionAttributes).SetValidator(new CommandOptionAttributeValidator());

            // options for each verb must be unique
            RuleFor(opt => opt)
                .Must(o => o.OptionAttributes
                    .AllOptionNames()
                    .Where(s => !string.IsNullOrEmpty(s))
                    .GroupBy(g => g)
                    .All(x => x.Count() <= 1))
                .WithErrorCode("CLCOC0001")
                .WithName("Attribute")
                .WithMessage("Options cannot have duplicates within the same verb");

            RuleSet("EntireConfig", () =>
            {
                // option must depend on items that exist
                RuleFor(c => c)
                    .Must(o => o.OptionAttributes.All(d => d.DependsOn
                        .AllExistsIn(o.OptionAttributes.OptionShortNames())))
                    .WithName("DependsOn")
                    .WithErrorCode("CLCOC0003")
                    .WithMessage("All DependsOn option values must exist");

                // option must be exclusive with items that exist
                RuleFor(c => c)
                    .Must(o => o.OptionAttributes.All(d => d.ExclusiveWith
                        .AllExistsIn(o.OptionAttributes.OptionShortNames())))
                    .WithName("ExclusiveWith")
                    .WithErrorCode("CLCOC0004")
                    .WithMessage("All ExclusiveWith option values must exist");
            });
        }

        [ExcludeFromCodeCoverage]
        public ValidationState DoValidate(ICommandOptionAttributeCollection item, string[] ruleSets = null)
        {
            var v = new CommandOptionAttributeCollectionValidator();

            var result = ruleSets is null ? v.Validate(item) : v.Validate(item, options => { options.IncludeRuleSets(ruleSets); });

            return result.ConvertToValidationState(ErrorCategory.Configuration);
        }
    }
}