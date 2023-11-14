using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VNet.CommandLine;
using VNet.CommandLine.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VNet.CommandLineTest.Tests
{
    [TestClass]
    public class ConfigurationValidationTests
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
        public void InterfaceImplemented()
        {
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass");

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void InterfaceNotImplemented()
        {
            ConfigTestHelper.AddSingleVerbClassNoInterface(_assembly, "TestClass");
            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void ReservedVerbUsedShortName()
        {
            ConfigTestHelper.AddSingleReservedVerbClassShortName(_assembly, "TestClass");
            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void ReservedVerbUsedLongName()
        {
            ConfigTestHelper.AddSingleReservedVerbClassLongName(_assembly, "TestClass");
            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void ReservedOptionUsedShortName()
        {
            ConfigTestHelper.AddSingleReservedOptionClassShortName(_assembly, "TestClass");
            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void ReservedOptionUsedLongName()
        {
            ConfigTestHelper.AddSingleReservedOptionClassShortName(_assembly, "TestClass");
            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void NonUniqueVerbShortNames()
        {
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass1", "t", "test1");
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass2", "t", "test2", null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void NonUniqueVerbLongNames()
        {
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass1", "t1", "test");
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass2", "t2", "test", null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void NonUniqueVerbBothNames()
        {
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass1", "t", "test");
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass2", "t", "test", null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void EmptyVerbShortName()
        {
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass1", "", "test");

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void EmptyVerbLongName()
        {
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass1", "t", "");

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void EmptyVerbBothNames()
        {
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass1", "", "");

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(2, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void EmptyOptionShortName()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t", "test", "", "extra");

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void EmptyOptionLongName()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t", "test", "e", "");

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void EmptyOptionBothNames()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t", "test", "", "");

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(2, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void NonUniqueOptionShortName()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t1", "test1", "e", "extra1");
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", "t2", "test2", "e", "extra2", null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void NonUniqueOptionLongNames()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t1", "test1", "e1", "extra");
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", "t2", "test2", "e2", "extra", null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void NonUniqueOptionBothNames()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t1", "test1", "e", "extra");
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", "t2", "test2", "e", "extra", null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void SingleDefaultVerb()
        {
            ConfigTestHelper.AddSingleVerbWithDefaultVerbClass(_assembly, "TestClass1", "t1", "test1", "e", "extra");

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void MultipleDefaultVerbs()
        {
            ConfigTestHelper.AddSingleVerbWithDefaultVerbClass(_assembly, "TestClass1", "t1", "test1", "e1", "extra1");
            ConfigTestHelper.AddSingleVerbWithDefaultVerbClass(_assembly, "TestClass2", "t2", "test2", "e2", "extra2", true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionWithVerb()
        {
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass1");

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionWithoutVerb()
        {
            ConfigTestHelper.AddOptionWithoutVerbClass(_assembly, "TestClass1", "e", "extra");

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(2, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbDependencyRefersToSelf()
        {
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass1", "t", "test", new string[] { "DependsOn" }, new object[] { new string[] { "t" } });

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbDependencyDoesNotReferToSelf()
        {
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass1", "t", "test", new string[] { "DependsOn" }, new object[] { new string[] { "a" } });
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass2", "a", "testa", null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDependencyRefersToSelf()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, "t", "test", null, null, new string[] { "DependsOn" }, new object[] { new string[] { "t" } });

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDependencyDoesNotReferToSelf()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, "t", "test", null, null, new string[] { "DependsOn" }, new object[] { new string[] { "a" } });
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", null, null, "a", "testa", null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbExclusiityRefersToSelf()
        {
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass1", "t", "test", new string[] { "ExclusiveWith" }, new object[] { new string[] { "t" } });

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbExclusiityDoesNotReferToSelf()
        {
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass1", "t", "test", new string[] { "ExclusiveWith" }, new object[] { new string[] { "a" } });
            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass2", "a", "testa", null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionExclusivityRefersToSelf()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, "t", "test", null, null, new string[] { "ExclusiveWith" }, new object[] { new string[] { "t" } });

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionExclusivityDoesNotReferToSelf()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, "t", "test", null, null, new string[] { "ExclusiveWith" }, new object[] { new string[] { "a" } });
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", null, null, "a", "testa", null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbDependencyHasValidValue()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t", "test", null, null, new string[] { "DependsOn" }, new object[] { new string[] { "a" } }, null, null);
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", "a", "testa", null, null, null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbDependencyDoesNotHaveValidValue()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t", "test", null, null, new string[] { "DependsOn" }, new object[] { new string[] { "a" } }, null, null);
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", "b", "testb", null, null, null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbExclusivityHasValidValue()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t", "test", null, null, new string[] { "ExclusiveWith" }, new object[] { new string[] { "a" } }, null, null);
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", "a", "testa", null, null, null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbExclusivityDoesNotHaveValidValue()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t", "test", null, null, new string[] { "ExclusiveWith" }, new object[] { new string[] { "a" } }, null, null);
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", "b", "testb", null, null, null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDependencyHasValidValue()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, "t", "test", null, null, new string[] { "DependsOn" }, new object[] { new string[] { "a" } });
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", null, null, "a", "testa", null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDependencyDoesNotHaveValidValue()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, "t", "test", null, null, new string[] { "DependsOn" }, new object[] { new string[] { "a" } });
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", null, null, "b", "testb", null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionExclusivityHasValidValue()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, "t", "test", null, null, new string[] { "ExclusiveWith" }, new object[] { new string[] { "a" } });
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", null, null, "a", "testa", null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionExclusivityDoesNotHaveValidValue()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, "t", "test", null, null, new string[] { "ExclusiveWith" }, new object[] { new string[] { "a" } });
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", null, null, "b", "testb", null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbExecutionOrderValid()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t1", "test1", null, null, new string[] { "ExecutionOrder" }, new object[] { (int?)1 }, null, null);
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", "t2", "test2", null, null, new string[] { "ExecutionOrder" }, new object[] { (int?)2 }, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbExecutionOrderNotValid()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t1", "test1", null, null, new string[] { "ExecutionOrder" }, new object[] { 2 }, null, null);
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", "t2", "test2", null, null, new string[] { "ExecutionOrder" }, new object[] { 2 }, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbAndOptionNamesUnique()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t1", "test1", "e1", "extra1");
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", "t2", "test2", "e2", "extra2", null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbAndOptionNamesNotUniqueDifferentVerbs()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t1", "test1", "e1", "extra1");
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass2", "e1", "extra1", null, null, null, null, null, null, null, true);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbAndOptionNamesNotUniqueSameVerb()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", "t1", "test1", "t1", "test1");

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidString1()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(string));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidString2()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(String));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidChar()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(char));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidBool()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(bool));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidInt16()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(Int16));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidInt32()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(Int32));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidInt64()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(Int64));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidShort()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(short));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidInt()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(int));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidLong()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(long));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidSingle()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(Single));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidFloat()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(float));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidDecimal()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(decimal));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidDouble()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(double));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidDateTime()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(DateTime));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeValidEnum()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(CommandVerbProperty));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionDataTypeInvalid()
        {
            ConfigTestHelper.AddSingleVerbWithOptionClass(_assembly, "TestClass1", null, null, null, null, null, null, null, null, typeof(List<string>));

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void DefaultHelpPresent()
        {
            ConfigTestHelper.AddSingleVerbForDefaultVerbs(_assembly, "TestClass1", true, true, null, null, null, null);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void DefaultHelpMissing()
        {
            ConfigTestHelper.AddSingleVerbForDefaultVerbs(_assembly, "TestClass1", false, true, null, null, null, null);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void DefaultHelpUsedMoreThanOnce()
        {
            ConfigTestHelper.AddSingleVerbForDefaultVerbs(_assembly, "TestClass1", true, true, null, null, null, null);
            ConfigTestHelper.AddSingleVerbForDefaultVerbs(_assembly, "TestClass2", true, false, null, null, null, null);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void DefaultVersionPresent()
        {
            ConfigTestHelper.AddSingleVerbForDefaultVerbs(_assembly, "TestClass1", true, true, null, null, null, null);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void DefaultVersionMissing()
        {
            ConfigTestHelper.AddSingleVerbForDefaultVerbs(_assembly, "TestClass1", true, false, null, null, null, null);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(0, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void DefaultVersionUsedMoreThanOnce()
        {
            ConfigTestHelper.AddSingleVerbForDefaultVerbs(_assembly, "TestClass1", true, true, null, null, null, null);
            ConfigTestHelper.AddSingleVerbForDefaultVerbs(_assembly, "TestClass2", false, true, null, null, null, null);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void DuplicateOptionsDefined()
        {
            var options = new List<ConfigTestOptionParameter>();

            var op1 = new ConfigTestOptionParameter()
            {
                ShortName = "prm1",
                LongNames = new string[] {}
            };

            var op2 = new ConfigTestOptionParameter()
            {
                ShortName = "prm1",
                LongNames = new string[] { }
            };

            options.Add(op1);
            options.Add(op2);

            ConfigTestHelper.AddSingleVerbWithMultipleOptionClasses(_assembly, "TestClass1", "tc", "testclass", null, null, options);

            _cm.Process(new string[] { "" }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionAllowNullsNotNeededWithoutNeedsValue()
        {
            var options = new List<ConfigTestOptionParameter>();

            var op1 = new ConfigTestOptionParameter()
            {
                DataType = typeof(string),
                ShortName = "o",
                LongNames = new string[] { "option" },
                PropertyNames = new string[] { "AllowNullValue", "NeedsValue" },
                PropertyValues = new object[] { true, false }
            };

            options.Add(op1);

            ConfigTestHelper.AddSingleVerbWithMultipleOptionClasses(_assembly, "TestClass1", "t", "test", null, null, options);

            _cm.Process(new string[] { }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(1, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbStartsWithPrefixShortName()
        {
            string name = DefaultValues.VerbPrefixes.First(n => n.Length > 0);

            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass", name, "test");

            _cm.Process(new string[] { }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(3, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void VerbStartsWithPrefixLongName()
        {
            string name = DefaultValues.VerbPrefixes.First(n => n.Length > 0);

            ConfigTestHelper.AddSingleVerbClassWithInterface(_assembly, "TestClass", "test", name);

            _cm.Process(new string[] { }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(3, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionStartsWithPrefixShortName()
        {
            string name = DefaultValues.OptionPrefixes.First(n => n.Length > 0);
            var options = new List<ConfigTestOptionParameter>();

            var op1 = new ConfigTestOptionParameter()
            {
                DataType = typeof(string),
                ShortName = name,
                LongNames = new string[] { "option" },
                PropertyNames = new string[] { },
                PropertyValues = new object[] { }
            };

            options.Add(op1);

            ConfigTestHelper.AddSingleVerbWithMultipleOptionClasses(_assembly, "TestClass1", "t", "test", null, null, options);

            _cm.Process(new string[] { }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(3, _cm.Validity.ConfigurationErrorMessages.Count);
        }

        [TestMethod]
        public void OptionStartsWithPrefixLongName()
        {
            string name = DefaultValues.OptionPrefixes.First(n => n.Length > 0);
            var options = new List<ConfigTestOptionParameter>();

            var op1 = new ConfigTestOptionParameter()
            {
                DataType = typeof(string),
                ShortName = "o",
                LongNames = new string[] { name },
                PropertyNames = new string[] { },
                PropertyValues = new object[] { }
            };

            options.Add(op1);

            ConfigTestHelper.AddSingleVerbWithMultipleOptionClasses(_assembly, "TestClass1", "t", "test", null, null, options);

            _cm.Process(new string[] { }, _assembly, _streams, _logger, _config, _parse, _display, _validate);

            Assert.AreEqual(3, _cm.Validity.ConfigurationErrorMessages.Count);
        }
    }
}