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
    public class ParsingValidationTests
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
        public void UnknownOptionsNotPresentAllowFlagFalse()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.AllowUnknownOptions = false;
            _cm.Process(new string[] { "-c s -i d" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void UnknownOptionsNotPresentAllowFlagTrue()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s -i d" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void UnknownOptionsPresentAllowFlagFalse()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.AllowUnknownOptions = false;
            _cm.Process(new string[] { "-c s -i d r" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void UnknownOptionsPresentAllowFlagTrue()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s -i d r" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void DuplicateOptionAllowDuplicatesFalse()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s s -i d r" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void DuplicateOptionAllowDuplicatesTrue()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c s -i d d r" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void RequiredOptionPresent()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c -m t -i" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void RequiredOptionMissing()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c -m -i" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void DependencyPresent()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c -m t -i -sv so1 so2" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void DependencyMissing()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c -m t -i -sv so1" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ExclusivityMissing()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c -m t -i -sv so1 so2" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ExclusivityPresent()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c -m t -i -sv so1 so2 so3" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void NeedsValueAndHasOneWithSeparator()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv so4:test" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void NeedsValueAndHasOneWithoutSeparator()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv so4 test" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void NeedsValueAndHasNone()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv so4" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(2, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void DoesNotNeedValueHasOneWithSeparator()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv so5:test" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void AllowsNullValueNull()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv so6:" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void AllowsNullValueNotNull()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv so6:test" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void DoesNotAllowsNullValueNull()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv so4:" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(2, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void DoesNotAllowsNullValueNotNull()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv so4:test" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsString1()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv string1:testing" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsString2()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv string2:testing" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsChar()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv char:t" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsNotChar()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv char:test" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsBool()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv bool:true" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsNotBool()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv bool:test" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsDateTime()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv datetime:2021-05-31" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsNotDateTime()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv datetime:testing" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsInt()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv int:77" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsNotInt()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv int:testing" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsShort()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv short:54" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsNotShort()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv short:testing" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsLong()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv log:87123" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsNotLong()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv long:testing" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(1, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsInt16()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv int16:31" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsArrayMissingStartDelimiter()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv array:1,2,3]" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(4, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsArrayMissingEndDelimiter()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv array:[1,2,3" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(4, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsArrayMismatchedDelimiter()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv array:[1,2,3'" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(3, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsArrayInvalidSeparator()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv array:[1+2+3]" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(3, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsArrayMultipleSeparators()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv array:=[a,b,c]" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(3, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsArrayDuplicate()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv array:[a, b, c] array:[d, e, f]" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(0, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsArrayDuplicateMissingStartDelimiter()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv array:1,2,3] array:4,5,6]" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(8, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsArrayDuplicateMissingEndDelimiter()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv array:[1,2,3 array:[4,5,6" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(8, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsArrayDuplicateMismatchedDelimiter()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv array:[1,2,3' array:[4,5,6'" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(6, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsArrayDuplicateInvalidSeparator()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv array:[1+2+3] array:[4+5+6]" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(6, _cm.Validity.UsageErrorMessages.Count);
        }

        [TestMethod]
        public void ValueIsArrayDuplicateMultipleSeparators()
        {
            ConfigTestHelper.CreateValidConfiguration(_assembly);

            _cm.Process(new string[] { "-c i -sv array:=[a,b,c] array:=[d,e,f]" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
            Assert.AreEqual(6, _cm.Validity.UsageErrorMessages.Count);
        }
    }
}