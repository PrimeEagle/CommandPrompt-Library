using System.Collections.Generic;

namespace VNet.CommandLine.Validation
{
    public class AdditionalValidators : IAdditionalValidators
    {
        public List<ICommandValidator> Validators { get; init; }

        public AdditionalValidators(IEnumerable<ICommandValidator> validators)
        {
            this.Validators = new List<ICommandValidator>();

            this.Validators.AddRange(validators);
        }
    }
}
