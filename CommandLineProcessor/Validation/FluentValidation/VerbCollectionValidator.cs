using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace VNet.CommandLine.Validation.FluentValidation
{
    public class VerbCollectionValidator : AbstractValidator<IVerbCollection>, ICommandValidator<IVerbCollection>
    {
        public override ValidationResult Validate(ValidationContext<IVerbCollection> context)
        {
            return base.Validate(context);
        }

        public VerbCollectionValidator()
        {
            RuleForEach(v => v.Verbs).SetValidator(new VerbValidator());



            // No duplicate verbs
            RuleFor(v => v.Verbs).Must(v =>
                v
                    .ActiveVerbs()
                    .GroupBy(x => x.ShortName).Count(g => g.Count<IVerb>() > 1) == 0
            )
                .WithName("ShortName")
                .WithErrorCode("CLUVC0001")
                .WithMessage(v => $"Verb cannot have duplicates");


            // Everything in DependsOn must refer to existing short names
            RuleFor(v => v.Verbs).Must(v =>
                    v.ActiveVerbs().SelectMany(d => d.DependsOn)
                        .All(x => v.ActiveVerbs().Select(n => n.ShortName)
                            .Any(s => s == x))
            )
                .WithName("DependsOn")
                .WithErrorCode("CLUVC0002")
                .WithMessage("{PropertyName} refers to a ShortName that was not found");


            // Everything in ExclusiveWith must not refer to existing short names
            RuleFor(v => v.Verbs).Must(v =>
                    v.ActiveVerbs().SelectMany(d => d.ExclusiveWith)
                        .Count(x => v.ActiveVerbs().Select(n => n.ShortName)
                            .Any(s => s == x)) == 0
                )
                .WithName("ExclusiveWith")
                .WithErrorCode("CLUVC0003")
                .WithMessage("{PropertyName} refers to a ShortName that was not found");


            // First argument must be a verb
            RuleFor(v => v.Verbs)
                .Must(v => v.Any(c => c.ArgumentIndex == 0))
                .When(v => v.Verbs.ActiveVerbs().Any())
                .WithName("ArgumentIndex")
                .WithErrorCode("CLUVC0004")
                .WithMessage("The first argument must be a verb");
        }

        [ExcludeFromCodeCoverage]
        public ValidationState DoValidate(IVerbCollection item, string[] ruleSets = null)
        {
            var v = new VerbCollectionValidator();

            var result = ruleSets is null ? v.Validate(item) : v.Validate(item, options => { options.IncludeRuleSets(ruleSets); });

            return result.ConvertToValidationState(ErrorCategory.Usage);
        }
    }
}