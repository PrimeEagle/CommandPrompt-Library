using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace VNet.CommandLine
{
    public class DefaultDisplayer : IDisplayer
    {
        public void ShowHelp(IEnumerable<TextWriter> streams, ILogger logger, IEnumerable<IVerb> commandList)
        {
            var processModule = System.Diagnostics.Process.GetCurrentProcess().MainModule;
            var textWriters = streams?.ToList();

            if (processModule != null)
            {
                var exeName = Path.GetFileName(processModule.FileName);

                WriteLineStreams(textWriters);
                WriteLineStreams(textWriters, $"Usage: {exeName} [commands] [options]");
            }

            foreach (var s in commandList.OrderForDisplay())
            {
                var verbLongName = string.Join(' ', s.LongNames);
                var verbRequiredStart = s.Required ? string.Empty : "[";
                var verbRequiredEnd = s.Required ? string.Empty : "]";

                WriteLineStreams(textWriters, $"     {verbRequiredStart}{s.ShortName}{verbRequiredEnd} ({verbRequiredStart}{verbLongName}{verbRequiredEnd}          {s.HelpText}");
                foreach(var o in s.Options)
                {
                    var optionLongName = string.Join(' ', o.LongNames);
                    var optionRequiredStart = o.Required ? string.Empty : "[";
                    var optionRequiredEnd = o.Required ? string.Empty : "]";

                    WriteLineStreams(textWriters, $"          {optionRequiredStart}{o.ShortName}{optionRequiredEnd} ({optionRequiredStart}{optionLongName}{optionRequiredEnd}          {o.HelpText}");
                }
            }
        }

        public void ShowErrors(IEnumerable<TextWriter> streams, ILogger logger, IEnumerable<string> errorMessages, bool log)
        {
            var textWriters = streams?.ToList();

            foreach (var msg in errorMessages.Select(e => $"{e}"))
            {
                WriteLineStreams(textWriters, msg);
                if(log) WriteLogError(logger, msg);
            }
        }

        private static void WriteLogError(ILogger logger, string text = "")
        {
            logger?.LogError(text);
        }

        private static void WriteLineStreams(IEnumerable<TextWriter> streams, string text = "")
        {
            if (streams == null) return;

            foreach (var s in streams)
            {
                Console.SetOut(s);
                Console.WriteLine(text);
            }
        }



        //internal static void CloseStreams(IEnumerable<TextWriter> streams)
        //{
        //    if (streams == null) return;

        //    foreach (var s in streams)
        //    {
        //        s.Close();
        //    }

        //    var standardOutput = new StreamWriter(Console.OpenStandardOutput());
        //    standardOutput.AutoFlush = true;
        //    Console.SetOut(standardOutput);
        //}
    }
}