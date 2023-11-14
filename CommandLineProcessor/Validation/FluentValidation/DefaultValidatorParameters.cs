using System.Collections.Generic;

namespace VNet.CommandLine.Validation.FluentValidation
{
    public class DefaultValidatorParameters : IValidatorParameters
    {
        public ICommandValidator<IVerb> VerbValidator { get; set; }
        public ICommandValidator<IOption> OptionValidator { get; set; }
        public ICommandValidator<IVerbCollection> VerbCollectionValidator { get; set; }
        public ICommandValidator<IOptionCollection> OptionCollectionValidator { get; set; }
        public IAdditionalValidators AdditionalValidators { get; init; }

        public DefaultValidatorParameters(ICommandValidator<IVerb> verbValidator,
            ICommandValidator<IOption> optionValidator,
            ICommandValidator<IVerbCollection> verbCollectionValidator,
            ICommandValidator<IOptionCollection> optionCollectionValidator,
            IAdditionalValidators additionalValidators)
        {
            this.VerbValidator = verbValidator;
            this.VerbCollectionValidator = verbCollectionValidator;
            this.OptionValidator = optionValidator;
            this.OptionCollectionValidator = optionCollectionValidator;
            this.AdditionalValidators = additionalValidators;
        }
    }
}
