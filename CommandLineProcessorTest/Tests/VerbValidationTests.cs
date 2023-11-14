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
    public class VerbValidationTests
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
        public void UnknownVerbNotUsed()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s -i d" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void UnknownVerbUsed()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s -i d -xyz" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void RequiredVerbPresent()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s -i d" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void RequiredVerbMissing()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-m t -i d" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void DuplicateVerbsNotPresent()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s -m t -i d" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void DuplicateVerbsPresent()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s -m t -c -i" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void VerbDependencyPresent()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s -m t -i" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void VerbDependencyNotPresent()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s -m t" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }


        [TestMethod]
        public void VerbExclusivityPresent()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s -m t -i -e" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void VerbExclusivityNotPresent()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s -m t -i" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }
    }
}