using System.Diagnostics.CodeAnalysis;
using System.Linq;
using VNet.CommandLine.Attributes;
using FluentValidation;

namespace VNet.CommandLine.Validation.FluentValidation
{
    public class AssemblyValidator : AbstractValidator<IAssembly>, ICommandValidator<IAssembly>
    {
        public AssemblyValidator()
        {
            // all classes with [CommandVerb] must have IVerbExecutable
            RuleFor(a => a.ClassesWithAttribute<CommandVerbAttribute>())
                .Must(c => c.All(a => a.GetInterfaces().Contains(typeof(IVerbExecutable))))
                .WithErrorCode("CLCA0001")
                .WithName("Attribute")
                .WithMessage("Classes with the [CommandVerb] attribute must implement the interface IVerbExecutable");

            // [CommandHelp] is used one time
            RuleFor(a => a.ClassesWithAttribute<CommandHelpAttribute>())
                .Must(c => c.Count() <= 1)
                .WithErrorCode("CLCA0002")
                .WithName("Attribute")
                .WithMessage("The attribute [CommandHelp] must be used one, and only one, time");

            // [CommandVersion] is used one time
            RuleFor(a => a.ClassesWithAttribute<CommandVersionAttribute>())
                .Must(c => c.Count() <= 1)
                .WithErrorCode("CLCA0003")
                .WithName("Attribute")
                .WithMessage("The attribute [CommandVersion] must be used one, and only one, time");

            // [CommandDefaultVerb] is used no more than one time
            RuleFor(a => a.ClassesWithAttribute<CommandDefaultVerbAttribute>())
                .Must(c => c.Count() <= 1)
                .WithErrorCode("CLCA0004")
                .WithName("Attribute")
                .WithMessage("The attribute [CommandDefaultVerb] must be used one, and only one, time");

            // all options must have a verb
            RuleFor(a => a)
                .Must(c => c.ClassesWithOptionAttributes().AllExistsIn(c.ClassesWithVerbAttributes()))
                .WithErrorCode("CLCA0005")
                .WithName("Attribute")
                .WithMessage("All classes with a [CommandOption] attribute must also have a [CommandVerb] attribute");

            // configuration must have at least one verb
            RuleFor(a => a)
                .Must(v => v.ClassesWithVerbAttributes().Any())
                .WithErrorCode("CLCA0006")
                .WithName("Attribute")
                .WithMessage("Configuration must have at least one verb");

            RuleFor(a => a)
                .Must(v => !v.AllVerbNames().Intersect(v.AllOptionNames()).Any())
                .WithErrorCode("CLCA0007")
                .WithName("Attribute")
                .WithMessage("Verbs and options cannot have the same name as each other");
        }

        [ExcludeFromCodeCoverage]
        public ValidationState DoValidate(IAssembly item, string[] ruleSets = null)
        {
            var v = new AssemblyValidator();

            var result = ruleSets is null ? v.Validate(item) : v.Validate(item, options => { options.IncludeRuleSets(ruleSets); });

            return result.ConvertToValidationState(ErrorCategory.Configuration);
        }
    }
}