namespace VNet.CommandLine.Validation
{
    public interface IValidatorManager
    {
        IAssembly Assembly { get; set; }
        IValidatorParameters ValidatorParameters { get; init; }

        ValidationState Validate(ParsedVerbsResult parseResult, bool allowUnknownOptions, bool stopIfConfigurationErrors);
    }
}
