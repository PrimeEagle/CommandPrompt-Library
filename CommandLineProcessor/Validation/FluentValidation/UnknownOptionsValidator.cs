using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using FluentValidation.Results;

namespace VNet.CommandLine.Validation.FluentValidation
{
    public class UnknownOptionsValidator : AbstractValidator<IUnknownOptions>, ICommandValidator<IUnknownOptions>
    {
        public override ValidationResult Validate(ValidationContext<IUnknownOptions> context)
        {
            return base.Validate(context);
        }

        public UnknownOptionsValidator()
        {
            // unknown options not allowed unless AllowUnknownOptions is true
            RuleFor(u => u.ShortNames)
                .Empty()
                .Unless(u => u.AllowUnknownOptions)
                .WithErrorCode("CLUO0001")
                .WithMessage("Unknown options are not allowed unless AllowUnknownOptions = true");
        }

        [ExcludeFromCodeCoverage]
        public ValidationState DoValidate(IUnknownOptions item, string[] ruleSets = null)
        {
            var v = new UnknownOptionsValidator();

            var result = ruleSets is null ? v.Validate(item) : v.Validate(item, options => { options.IncludeRuleSets(ruleSets); });

            return result.ConvertToValidationState(ErrorCategory.Usage);
        }
    }
}