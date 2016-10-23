using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagOUtils.Guards.Messages;

namespace BagOUtils.Guards
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
        // Null Handlers
        //
        //-------------------------------------------------------------------------

        /// <summary>
        /// Check a string to see if it is null. If it is, return an empty string.
        /// </summary>
        public static string NullToEmpty(this string possibleNullString)
        {
            var fixedString = possibleNullString;
            if (possibleNullString == null)
            {
                fixedString = string.Empty;
            }
            return fixedString;
        }

        public static string NullToEmpty(this object possibleNullObject)
        {
            var objectString = string.Empty;
            if (possibleNullObject != null)
            {
                objectString = possibleNullObject.ToString();
            }
            return objectString;
        }
    }
}
