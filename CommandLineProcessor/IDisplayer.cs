using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;

namespace VNet.CommandLine
{
    public interface IDisplayer
    {
        void ShowHelp(IEnumerable<TextWriter> textWriters, ILogger logger, IEnumerable<IVerb> commands);
        void ShowErrors(IEnumerable<TextWriter> textWriters, ILogger logger, IEnumerable<string> errorMessages, bool log);
    }
}
