using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagOUtils.Guards.Messages;

namespace BagOUtils.Guards.Framework
{
    /// <summary>
    /// Extensions methods that allow fluent comparison of numbers. This
    /// is needed so we can have generic extensions methods that allow
    /// comparison of numbers. We have to define a type in order for the
    /// extension methods to be associated with the various numeric
    /// types. This allows us to use IComparable, to get a whole class
    /// of types that can have extension methods that do comparisons.
    /// </summary>
    public static class GuardExtensions
    {
        public static MessageTemplateGuard<TValue,TException> PrepareMessageTemplateGuard<TValue, TException>(this TValue value)
            where TException : Exception
            where TValue : IComparable<TValue>
        {
            var guard = new MessageTemplateGuard<TValue, TException>(value);
            return guard;
        }

        public static ItemTemplateGuard<TValue, TException> PrepareItemTemplateGuard<TValue, TException>(this TValue value)
            where TException : Exception
            where TValue : IComparable<TValue>
        {
            var guard = new ItemTemplateGuard<TValue, TException>(value);
            return guard;
        }

        public static LimitGuard<TValue, TException> PrepareLimitGuard<TValue, TException>(this TValue value)
            where TException : Exception
            where TValue : IComparable<TValue>
        {
            var guard = new LimitGuard<TValue, TException>(value);
            return guard;
        }

        public static MessageGuard<TValue, TException> PrepareMessageGuard<TValue, TException>(this TValue value)
            where TException : Exception
            where TValue : IComparable<TValue>
        {
            var guard = new MessageGuard<TValue, TException>(value);
            return guard;
        }

        /// <summary>
        /// Determine if the other value is less than 
        /// the base value.
        /// </summary>
        /// <typeparam name="T">Any comparable type</typeparam>
        /// <param name="baseValue">the extended value</param>
        /// <param name="otherValue">the value to test</param>
        /// <returns>
        /// True if the base value is less than the other value.
        /// </returns>
        public static bool LessThan<T>(this T baseValue, T otherValue)
          where T : IComparable<T>
        {
            return baseValue.CompareTo(otherValue) < 0;
        }

        /// <summary>
        /// Determine if the other value is less than 
        /// the base value.
        /// </summary>
        /// <typeparam name="T">Any comparable type</typeparam>
        /// <param name="baseValue">the extended value</param>
        /// <param name="otherValue">the value to test</param>
        /// <returns>
        /// True if the base value is less than the other value.
        /// </returns>
        public static bool LessThanOrEqual<T>(this T baseValue, T otherValue)
          where T : IComparable<T>
        {
            return baseValue.CompareTo(otherValue) <= 0;
        }

        /// <summary>
        /// Determine if the other value is less than 
        /// the base value.
        /// </summary>
        /// <typeparam name="T">Any comparable type</typeparam>
        /// <param name="baseValue">the extended value</param>
        /// <param name="otherValue">the value to test</param>
        /// <returns>
        /// True if the base value is less than the other value.
        /// </returns>
        public static bool EqualTo<T>(this T baseValue, T otherValue)
          where T : IComparable<T>
        {
            return baseValue.CompareTo(otherValue) == 0;
        }

        /// <summary>
        /// Determine if the other value is less than 
        /// the base value.
        /// </summary>
        /// <typeparam name="T">Any comparable type</typeparam>
        /// <param name="baseValue">the extended value</param>
        /// <param name="otherValue">the value to test</param>
        /// <returns>
        /// True if the base value is less than the other value.
        /// </returns>
        public static bool GreaterThanOrEqual<T>(this T baseValue, T otherValue)
          where T : IComparable<T>
        {
            return baseValue.CompareTo(otherValue) >= 0;
        }

        /// <summary>
        /// Determine if the other value is less than 
        /// the base value.
        /// </summary>
        /// <typeparam name="T">Any comparable type</typeparam>
        /// <param name="baseValue">the extended value</param>
        /// <param name="otherValue">the value to test</param>
        /// <returns>
        /// True if the base value is less than the other value.
        /// </returns>
        public static bool GreaterThan<T>(this T baseValue, T otherValue)
          where T : IComparable<T>
        {
            return baseValue.CompareTo(otherValue) > 0;
        }

        public static LimitMessageBuilder<T> BuildLimitMessage<T>(this T value)
          where T : IComparable<T>
        {
            return new LimitMessageBuilder<T>(value);
        }
    }
}
