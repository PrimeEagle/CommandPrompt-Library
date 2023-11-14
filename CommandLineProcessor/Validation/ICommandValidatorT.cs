namespace VNet.CommandLine.Validation
{
    public interface ICommandValidator<T> : ICommandValidator
    {
        ValidationState DoValidate(T item, string[] ruleSets = null);
    }
}
