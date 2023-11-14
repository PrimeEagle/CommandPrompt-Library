namespace VNet.CommandLine.Validation
{
    public interface IValidatorParameters
    {
        ICommandValidator<IVerb> VerbValidator { get; set; }
        ICommandValidator<IOption> OptionValidator { get; set; }
        ICommandValidator<IVerbCollection> VerbCollectionValidator { get; set; }
        ICommandValidator<IOptionCollection> OptionCollectionValidator { get; set; }
        IAdditionalValidators AdditionalValidators { get; init; }
    }
}
