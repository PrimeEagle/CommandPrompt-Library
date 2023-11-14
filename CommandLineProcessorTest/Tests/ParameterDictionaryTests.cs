using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VNet.CommandLine;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.IO;
using VNet.CommandLine.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace VNet.CommandLineTest.Tests
{
    [TestClass]
    public class ParameterDictionaryTests
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
        public void GetChar()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "char",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual('a', pd.GetChar("char"));
        }

        [TestMethod]
        public void GetString()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(string),
                DataValue = "abc",
                ShortName = "string",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual("abc", pd.GetString("string"));
        }

        [TestMethod]
        public void GetBool()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(bool),
                DataValue = true,
                ShortName = "bool",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual(true, pd.GetBoolean("bool"));
        }

        [TestMethod]
        public void GetDateTime()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(DateTime),
                DataValue = "2021-12-25",
                ShortName = "datetime",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual(new DateTime(2021, 12, 25), pd.GetDateTime("datetime"));
        }

        [TestMethod]
        public void GetShort()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(short),
                DataValue = "16",
                ShortName = "short",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual(16, pd.GetShort("short"));
        }

        [TestMethod]
        public void GetInt()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(int),
                DataValue = "32",
                ShortName = "int",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual(32, pd.GetInt("int"));
        }

        [TestMethod]
        public void GetLong()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(long),
                DataValue = "64",
                ShortName = "long",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual(64, pd.GetLong("long"));
        }

        [TestMethod]
        public void GetInt16()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(Int16),
                DataValue = "16",
                ShortName = "int16",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual(16, pd.GetInt16("int16"));
        }

        [TestMethod]
        public void GetIt32()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(Int32),
                DataValue = "32",
                ShortName = "int32",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual(32, pd.GetInt32("int32"));
        }

        [TestMethod]
        public void GetInt64()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(Int64),
                DataValue = "64",
                ShortName = "int64",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual(64, pd.GetInt64("int64"));
        }

        [TestMethod]
        public void GetSingle()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(Single),
                DataValue = "32.0",
                ShortName = "single",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual(32.0, pd.GetSingle("single"));
        }

        [TestMethod]
        public void GetFloat()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(float),
                DataValue = "32.0",
                ShortName = "float",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual(32.0, pd.GetFloat("float"));
        }

        [TestMethod]
        public void GetDouble()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(double),
                DataValue = "64.0",
                ShortName = "double",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual(64.0, pd.GetDouble("double"));
        }

        [TestMethod]
        public void GetDecimal()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(decimal),
                DataValue = "128.0",
                ShortName = "decimal",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual(128.0M, pd.GetDecimal("decimal"));
        }

        [TestMethod]
        public void GetEnum()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(CommandVerbProperty),
                DataValue = "helptext",
                ShortName = "enum",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.AreEqual(CommandVerbProperty.HelpText, pd.GetEnum<CommandVerbProperty>("enum"));
        }

        [TestMethod]
        public void GetArray()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(Array),
                DataValue = new string[] { "helptext1", "helptext2", "helptext3" },
                ShortName = "array",
                LongNames = new List<string>()
            };
            pd.Add(p);
            
            var expectedResult = new string[] {"helptext1", "helptext2", "helptext3"};

            Assert.IsTrue(expectedResult.SequenceEqual(pd.GetArray<string>("array")));
        }

        [TestMethod]
        public void GetCharInvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "abc",
                ShortName = "char",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<FormatException>(() => pd.GetChar("char"));
        }

        [TestMethod]
        public void GetBoolInvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(bool),
                DataValue = "a",
                ShortName = "bool",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<FormatException>(() => pd.GetBoolean("bool"));
        }

        [TestMethod]
        public void GetDateTimeInvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(DateTime),
                DataValue = "abc",
                ShortName = "datetime",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<FormatException>(() => pd.GetDateTime("datetime"));
        }

        [TestMethod]
        public void GetShortInvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(short),
                DataValue = "a",
                ShortName = "short",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<FormatException>(() => pd.GetShort("short"));
        }

        [TestMethod]
        public void GetIntInvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(int),
                DataValue = "a",
                ShortName = "int",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<FormatException>(() => pd.GetInt("int"));
        }

        [TestMethod]
        public void GetLongInvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(long),
                DataValue = "a",
                ShortName = "long",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<FormatException>(() => pd.GetLong("long"));
        }

        [TestMethod]
        public void GetInt16InvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(Int16),
                DataValue = "a",
                ShortName = "int16",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<FormatException>(() => pd.GetInt16("int16"));
        }

        [TestMethod]
        public void GetIt32InvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(Int32),
                DataValue = "a",
                ShortName = "int32",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<FormatException>(() => pd.GetInt32("int32"));
        }

        [TestMethod]
        public void GetInt64InvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(Int64),
                DataValue = "a",
                ShortName = "int64",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<FormatException>(() => pd.GetInt64("int64"));
        }

        [TestMethod]
        public void GetSingleInvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(Single),
                DataValue = "a",
                ShortName = "single",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<FormatException>(() => pd.GetSingle("single"));
        }

        [TestMethod]
        public void GetFloatInvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(float),
                DataValue = "a",
                ShortName = "float",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<FormatException>(() => pd.GetFloat("float"));
        }

        [TestMethod]
        public void GetDoubleInvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(double),
                DataValue = "a",
                ShortName = "double",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<FormatException>(() => pd.GetDouble("double"));
        }

        [TestMethod]
        public void GetDecimalInvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(decimal),
                DataValue = "a",
                ShortName = "decimal",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<FormatException>(() => pd.GetDecimal("decimal"));
        }

        [TestMethod]
        public void GetEnumInvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(CommandVerbProperty),
                DataValue = "test",
                ShortName = "enum",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetEnum<CommandVerbProperty>("enum"));
        }

        [TestMethod]
        public void GetArrayInvalidCast()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(CommandVerbProperty),
                DataValue = new string[] { "helptext1", "helptext2", "helptext3" },
                ShortName = "array",
                LongNames = new List<string>()
            };
            pd.Add(p);

            var expectedResult = new string[] { "helptext1", "helptext2", "helptext3" };

            Assert.ThrowsException<InvalidCastException>(() => pd.GetArray<string>("array"));
        }

        [TestMethod]
        public void GetCharKeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetChar("char"));
        }

        [TestMethod]
        public void GetStringKeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetString("char"));
        }

        [TestMethod]
        public void GetBoolKeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetBoolean("bool"));
        }

        [TestMethod]
        public void GetDateTimeKeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetDateTime("datetime"));
        }

        [TestMethod]
        public void GetShortKeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetShort("short"));
        }

        [TestMethod]
        public void GetIntKeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetInt("int"));
        }

        [TestMethod]
        public void GetLongKeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetLong("long"));
        }

        [TestMethod]
        public void GetInt16KeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetInt16("int16"));
        }

        [TestMethod]
        public void GetIt32KeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetInt32("int32"));
        }

        [TestMethod]
        public void GetInt64KeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetInt64("int64"));
        }

        [TestMethod]
        public void GetSingleKeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetSingle("single"));
        }

        [TestMethod]
        public void GetFloatKeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetFloat("float"));
        }

        [TestMethod]
        public void GetDoubleKeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetDouble("double"));
        }

        [TestMethod]
        public void GetDecimalKeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetDecimal("decimal"));
        }

        [TestMethod]
        public void GetEnumKeyNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetEnum<CommandVerbProperty>("enum"));
        }

        [TestMethod]
        public void GetArrayNotFound()
        {
            var pd = new ParameterDictionary();

            Assert.ThrowsException<KeyNotFoundException>(() => pd.GetArray<string>("array"));
        }

        [TestMethod]
        public void GetCharTypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(int),
                DataValue = "abc",
                ShortName = "char",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetChar("char"));
        }

        [TestMethod]
        public void GetStringTypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(int),
                DataValue = "abc",
                ShortName = "string",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetString("string"));
        }

        [TestMethod]
        public void GetBoolTypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "bool",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetBoolean("bool"));
        }

        [TestMethod]
        public void GetDateTimeTypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "abc",
                ShortName = "datetime",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetDateTime("datetime"));
        }

        [TestMethod]
        public void GetShortTypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "short",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetShort("short"));
        }

        [TestMethod]
        public void GetIntTypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetInt("int"));
        }

        [TestMethod]
        public void GetLongTypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "long",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetLong("long"));
        }

        [TestMethod]
        public void GetInt16TypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int16",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetInt16("int16"));
        }

        [TestMethod]
        public void GetIt32TypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int32",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetInt32("int32"));
        }

        [TestMethod]
        public void GetInt64TypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int64",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetInt64("int64"));
        }

        [TestMethod]
        public void GetSingleTypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "single",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetSingle("single"));
        }

        [TestMethod]
        public void GetFloatTypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "float",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetFloat("float"));
        }

        [TestMethod]
        public void GetDoubleTypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "double",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetDouble("double"));
        }

        [TestMethod]
        public void GetDecimalTypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "decimal",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetDecimal("decimal"));
        }

        [TestMethod]
        public void GetEnumTypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "test",
                ShortName = "enum",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetEnum<CommandVerbProperty>("enum"));
        }

        [TestMethod]
        public void GetArrayTypeMismatch()
        {
            var pd = new ParameterDictionary();

            var p = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "test",
                ShortName = "array",
                LongNames = new List<string>()
            };
            pd.Add(p);

            Assert.ThrowsException<InvalidCastException>(() => pd.GetArray<string>("array"));
        }

        [TestMethod]
        public void Count()
        {
            var pd = new ParameterDictionary();

            var p1 = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int1",
                LongNames = new List<string>()
            };
            pd.Add(p1);

            var p2 = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int2",
                LongNames = new List<string>()
            };
            pd.Add(p2);

            var p3 = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int3",
                LongNames = new List<string>()
            };
            pd.Add(p3);

            Assert.AreEqual(3, pd.Count);
        }

        [TestMethod]
        public void Clear()
        {
            var pd = new ParameterDictionary();

            var p1 = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int1",
                LongNames = new List<string>()
            };
            pd.Add(p1);

            var p2 = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int2",
                LongNames = new List<string>()
            };
            pd.Add(p2);

            var p3 = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int3",
                LongNames = new List<string>()
            };
            pd.Add(p3);
            pd.Clear();

            Assert.AreEqual(0, pd.Count);
        }

        [TestMethod]
        public void Remove()
        {
            var pd = new ParameterDictionary();

            var p1 = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int1",
                LongNames = new List<string>()
            };
            pd.Add(p1);

            var p2 = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int2",
                LongNames = new List<string>()
            };
            pd.Add(p2);

            var p3 = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int3",
                LongNames = new List<string>()
            };
            pd.Add(p3);
            pd.Remove("int3");

            Assert.AreEqual(2, pd.Count);
        }

        [TestMethod]
        public void With()
        {
            var pd = new ParameterDictionary();

            var p1 = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int1",
                LongNames = new List<string>()
            };
            pd.Add(p1);

            var p2 = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int2",
                LongNames = new List<string>()
            };
            pd.Add(p2);

            var p3 = new OptionParameter()
            {
                DataType = typeof(char),
                DataValue = "a",
                ShortName = "int3",
                LongNames = new List<string>()
            };
            pd.Add(p3);

            Assert.AreEqual(1, pd.With(p => p.Value.ShortName == "int3").Count);
        }
    }
}