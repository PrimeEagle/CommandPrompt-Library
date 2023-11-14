using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace VNet.CommandLine.Validation.FluentValidation
{
    public class OptionCollectionValidator : AbstractValidator<IOptionCollection>, ICommandValidator<IOptionCollection>
    {
        public override ValidationResult Validate(ValidationContext<IOptionCollection> context)
        {
            return base.Validate(context);
        }

        public OptionCollectionValidator()
        {
            RuleForEach(o => o.Options).SetValidator(new OptionValidator());



            // No duplicate options
            RuleFor(v => v.Options.Where(c => !c.AllowDuplicates && c.ArgumentIndex.HasValue)
                    .GroupBy(g => g.ArgumentIndex)
                    .Select(h => h.First()).ToList()
                    .AllOptionNames()
                    .Where(n => !string.IsNullOrEmpty(n)))
                .Must(n => n.GroupBy(g => g).All(x => x.Count<string>() <= 1))
                .WithName("ShortName and LongName")
                .WithErrorCode("CLUOC0001")
                .WithMessage("Option names cannot be duplicated");


            // Everything in DependsOn must refer to existing short names
            RuleFor(o => o.Options).Must(o =>
                    o.ActiveOptions().SelectMany(d => d.DependsOn)
                        .All(x => o.ActiveOptions().Select(n => n.ShortName)
                            .Any(s => s == x))
                )
                .WithName("DependsOn")
                .WithErrorCode("CLUOC0002")
                .WithMessage("{PropertyName} refers to a ShortName that was not found");


            // Everything in ExclusiveWith must not refer to existing short names
            RuleFor(o => o.Options).Must(o =>
                    o.ActiveOptions().SelectMany(d => d.ExclusiveWith)
                        .Count(x => o.ActiveOptions().Select(n => n.ShortName)
                            .Any(s => s == x)) == 0
                )
                .WithName("ExclusiveWith")
                .WithErrorCode("CLUOC0003")
                .WithMessage("{PropertyName} refers to a ShortName that was not found");
        }

        [ExcludeFromCodeCoverage]
        public ValidationState DoValidate(IOptionCollection item, string[] ruleSets = null)
        {
            var v = new OptionCollectionValidator();

            var result = ruleSets is null ? v.Validate(item) : v.Validate(item, options => { options.IncludeRuleSets(ruleSets); });

            return result.ConvertToValidationState(ErrorCategory.Usage);
        }
    }
}