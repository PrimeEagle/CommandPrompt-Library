using Microsoft.VisualStudio.TestTools.UnitTesting;
using VNet.CommandLine;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using VNet.CommandLine.Validation;
using VNet.Utility;

namespace VNet.CommandLineTest.Tests
{
    [TestClass]
    public class UtilityTests
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
       public void GetAfterMatch()
       {
           string s = "test:value";

           Assert.AreEqual("value", s.After(":"));
       }

        [TestMethod]
        public void GetAfterNoMatch()
        {
            string s = "test";

            Assert.AreEqual("test", s.After(":"));
        }

        [TestMethod]
        public void GetBeforeMatch()
        {
            string s = "test:value";

            Assert.AreEqual("test", s.Before(":"));
        }

        [TestMethod]
        public void GetBeforeNoMatch()
        {
            string s = "test";

            Assert.AreEqual("test", s.After(":"));
        }
    }
}