using System.Collections.Generic;
using System.IO;
using VNet.CommandLine;
using VNet.CommandLine.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VNet.CommandLineTest.Tests
{
    [TestClass]
    public class CommandLineManagerTests
    {
        private static TestServices _services;
        private static IAssembly _assembly;
        private static TextWriter _textWriter;
        private static List<TextWriter> _streams;
        private static ILogger _logger;
        private static IBuilder _config;
        private static IParser _parse;
        private static IDisplayer _display;
        private static IValidatorManager _validate;
        private CommandLineManager _cm;

        [ClassInitialize()]
        public static void ClassSetup(TestContext context)
        {
            _services = ConfigTestServices.ConfigureServices();

            _textWriter = _services.ApplicationServiceProvider.GetService<TextWriter>();
            _streams = new List<TextWriter>
            {
                _textWriter
            };
            _logger = _services.ApplicationServiceProvider.GetService<ILogger<CommandLineManagerTests>>();
            _config = _services.ApplicationServiceProvider.GetService<IBuilder>();
            _parse = _services.ApplicationServiceProvider.GetService<IParser>();
            _display = _services.ApplicationServiceProvider.GetService<IDisplayer>();
            _validate = _services.ApplicationServiceProvider.GetService<IValidatorManager>();
        }

        [TestInitialize()]
        public void TestSetup()
        {
            _assembly = _services.AssemblyServiceProvider.GetService<IAssembly>();
            _cm = new CommandLineManager();
        }

        [TestMethod]
        public void NullStreams()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s -i d -h" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void LoggerErrors()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-i d" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);


            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void NothingConfigured()
        {
            _cm.Process(new string[] {  }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }
    }
}