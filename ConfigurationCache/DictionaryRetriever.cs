using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.ConfigurationCache
{
    /// <summary>
    /// Cache value retriever when the source is simply a dictionary that stores strings.
    /// </summary>
    /// <remarks>
    /// In this case, the category is a class that can convert a string to the
    /// desired output type.
    /// </remarks>
    public class DictionaryRetriever : ICategoryRetriever<TypeCode>
    {
        private readonly Dictionary<string, string> source;
        private readonly Dictionary<TypeCode, Func<string, object>> typeParsers;

        public DictionaryRetriever(Dictionary<string, string> source)
        {
            this.source = source;
            this.typeParsers = this.DefineParsers();
        }

        public T RetrieveValue<T>(TypeCode valueType, string key)
        {
            // Guard that we know how to deal with the type and that
            // the key is defined in the dictionary. Also check that
            // the type code matched the generic type provided.
            if (!source.ContainsKey(key))
            {
                var unsupportedType = $"The type code provided, '{valueType.ToString()}', is not supported.";
                throw new ArgumentException(unsupportedType);
            }
            if (!source.ContainsKey(key))
            {
                var missingKeyMessage = $"The key provided, '{key}', is not defined.";
                throw new ArgumentException(missingKeyMessage);
            }
            if (!this.TypeCodeMatchesType<T>(valueType))
            {
                var typeMismatch = $"The type code, '{valueType.ToString()}', and the generic type, '{typeof(T).ToString()}, do not match.";
                throw new ArgumentException(typeMismatch);
            }

            var sourceValue = source[key];

            var parser = this.typeParsers[valueType];
            var parsedValue = parser(sourceValue);

            var strongValue = (T)parsedValue;

            return strongValue;
        }

        private Dictionary<TypeCode, Func<string, object>> DefineParsers()
        {
            var parsers = new Dictionary<TypeCode, Func<string, object>>
            {
                [TypeCode.Boolean] = value => bool.Parse(value),
                [TypeCode.Byte] = value => byte.Parse(value),
                [TypeCode.Char] = value => value.ToString(),
                [TypeCode.DateTime] = value => DateTime.Parse(value),
                [TypeCode.Decimal] = value => Decimal.Parse(value),
                [TypeCode.Double] = value => Double.Parse(value),
                [TypeCode.Int16] = value => Int16.Parse(value),
                [TypeCode.Int32] = value => Int32.Parse(value),
                [TypeCode.Int64] = value => Int64.Parse(value),
                [TypeCode.SByte] = value => SByte.Parse(value),
                [TypeCode.Single] = value => Single.Parse(value),
                [TypeCode.String] = value => value,
                [TypeCode.UInt16] = value => UInt16.Parse(value),
                [TypeCode.UInt32] = value => UInt32.Parse(value),
                [TypeCode.UInt64] = value => UInt64.Parse(value),
            };

            return parsers;
        }

        /// <summary>
        /// Return if the provided type code matches the indicated type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeCode"></param>
        /// <returns></returns>
        private bool TypeCodeMatchesType<T>(TypeCode typeCode)
        {
            var code = typeCode.ToString();
            var type = typeof(T).ToString();

            // Type will all have System. in front of them.
            var justType = type.Replace("System.", string.Empty);

            return code == justType;
        }
    }
}
