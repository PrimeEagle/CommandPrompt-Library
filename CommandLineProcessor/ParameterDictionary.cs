using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable BuiltInTypeReferenceStyle

namespace VNet.CommandLine
{
    public class ParameterDictionary
    {
        readonly Dictionary<string, OptionParameter> _parameters = new Dictionary<string, OptionParameter>();

        public ParameterDictionary() { }

        private ParameterDictionary(Dictionary<string, OptionParameter> parameters)
        {
            _parameters = parameters;
        }

        public int Count => _parameters.Count;

        // ReSharper disable once MemberCanBePrivate.Global
        public bool Contains(string name)
        {
            return _parameters.ContainsKey(name);
        }

        public short GetShort(string name)
        {
            short result;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(short))
                {
                    result = Convert.ToInt16(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public Int16 GetInt16(string name)
        {
            Int16 result;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(Int16))
                {
                    result = Convert.ToInt16(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public int GetInt(string name)
        {
            int result;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(int))
                {
                    result = Convert.ToInt32(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public Int32 GetInt32(string name)
        {
            Int32 result;

            if(Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(Int32))
                {
                    result = Convert.ToInt32(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public long GetLong(string name)
        {
            long result;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(long))
                {
                    result = Convert.ToInt64(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public Int64 GetInt64(string name)
        {
            Int64 result;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(Int64))
                {
                    result = Convert.ToInt64(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public bool GetBoolean(string name)
        {
            bool result;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(bool))
                {
                    result = Convert.ToBoolean(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public char GetChar(string name)
        {
            char result;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(char))
                {
                    result = Convert.ToChar(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public string GetString(string name)
        {
            string result;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(string))
                {
                    result = Convert.ToString(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public decimal GetDecimal(string name)
        {
            decimal result;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(decimal))
                {
                    result = Convert.ToDecimal(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public double GetDouble(string name)
        {
            double result;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(double))
                {
                    result = Convert.ToDouble(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public Single GetSingle(string name)
        {
            Single result;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(Single))
                {
                    result = Convert.ToSingle(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public float GetFloat(string name)
        {
            float result;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(float))
                {
                    result = Convert.ToSingle(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public DateTime GetDateTime(string name)
        {
            DateTime result;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(DateTime))
                {
                    result = Convert.ToDateTime(p.DataValue);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public T[] GetArray<T>(string name) where T : IConvertible
        {
            var result = new T[] {};

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(Array))
                {
                    result = Array.ConvertAll((object[])p.DataValue, item => (T)item);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            return result;
        }

        public T GetEnum<T>(string name) where T : struct, IConvertible
        {
            T result;
            bool success;

            if (Contains(name))
            {
                var p = _parameters[name];

                if (p.DataType == typeof(T))
                {
                    success = Enum.TryParse(p.DataValue.ToString(), true, out result);
                }
                else
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new KeyNotFoundException();
            }

            if (success)
            {
                return result;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public void Add(OptionParameter parameter)
        {
            if (!_parameters.ContainsKey(parameter.ShortName))
                _parameters.Add(parameter.ShortName, parameter);
        }

        public void Remove(string name)
        {
            if (_parameters.ContainsKey(name))
                _parameters.Remove(name);
        }

        public void Clear()
        {
            _parameters.Clear();
        }

        public ParameterDictionary With(Func<KeyValuePair<string, OptionParameter>, bool> predicate)
        {
            var dict = _parameters.Where(predicate).ToDictionary(x => x.Key, x => x.Value);
            return new ParameterDictionary(dict);
        }
    }
}