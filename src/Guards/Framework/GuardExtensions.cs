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
        //-------------------------------------------------------------------------
        //
        // Guard Initiators
        //
        //-------------------------------------------------------------------------

        /// <summary>
        /// Initiate a guard that will use an explicit message
        /// if the guard fails and an exception is thrown.
        /// </summary>
        /// <typeparam name="TValue">type of value being checked.</typeparam>
        /// <typeparam name="TException">Type of exception to throw.</typeparam>
        /// <param name="value">Value to check.</param>
        /// <returns>Guard to fluently configure.</returns>
        public static MessageTemplateComposer<TValue> ComposeMessageTemplateGuard<TValue>(this TValue value)
        {
            var guard = new MessageTemplateComposer<TValue>(value);
            return guard;
        }

        /// <summary>
        /// Initiate a guard that uses a template to inject the name of
        /// the item being checked.
        /// </summary>
        /// <typeparam name="TValue">type of value being checked.</typeparam>
        /// <typeparam name="TException">Type of exception to throw.</typeparam>
        /// <param name="value">Value to check.</param>
        /// <returns>Guard to fluently configure.</returns>
        public static ItemTemplateComposer<TValue> ComposeItemTemplateGuard<TValue>(this TValue value)
        {
            var composer = new ItemTemplateComposer<TValue>(value);
            return composer;
        }

        /// <summary>
        /// Initiate a guard that is checking on minimum or maximum limits
        /// of a value. A template is used to create a message indicating 
        /// if the value is too large or small.
        /// </summary>
        /// <typeparam name="TValue">type of value being checked.</typeparam>
        /// <typeparam name="TException">Type of exception to throw.</typeparam>
        /// <param name="value">Value to check.</param>
        /// <returns>Guard to fluently configure.</returns>
        public static LimitGuard<TValue, TException> PrepareLimitGuard<TValue, TException>(this TValue value)
            where TException : Exception
            where TValue : IComparable<TValue>
        {
            var guard = new LimitGuard<TValue, TException>(value);
            return guard;
        }

        /// <summary>
        /// Initiate a guard that uses an explicitly provided message for
        /// the exception that is thrown if the value does not pass the guard.
        /// </summary>
        /// <typeparam name="TValue">type of value being checked.</typeparam>
        /// <typeparam name="TException">Type of exception to throw.</typeparam>
        /// <param name="value">Value to check.</param>
        /// <returns>Guard to fluently configure.</returns>
        public static MessageComposer<TValue> PrepareMessageGuard<TValue>(this TValue value)
        {
            var composer = new MessageComposer<TValue>(value);
            return composer;
        }


        //-------------------------------------------------------------------------
        //
        // Comparison Methods
        //
        //-------------------------------------------------------------------------

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


        //-------------------------------------------------------------------------
        //
        // Limit Message Initiator
        //
        //-------------------------------------------------------------------------

        public static LimitMessageBuilder<T> BuildLimitMessage<T>(this T value)
          where T : IComparable<T>
        {
            return new LimitMessageBuilder<T>(value);
        }
    }
}
