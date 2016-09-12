using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Guards
{
    /// <summary>
    /// Defend against invalid states/parameters/arguments when the
    /// state/parameter/argument is an object. Each of the methods
    /// defines a "guard". The guard validates that the
    /// state/parameter/argument does not have a particular type of
    /// potential error. If the guard finds the
    /// state/parameter/argument to be in error, an exception is
    /// thrown. If the value is validated, the value is returned,
    /// allowing for a fluent guard API.
    /// </summary>
    public static class ObjectGuards
    {
        /// <summary>
        /// Guard that the item is not null.
        /// </summary>
        /// <param name="item">Item guard is called on.</param>
        /// <param name="argumentName">
        /// Name of argument/parameter/state that is being guarded.
        /// </param>
        /// <param name="exceptionMessage">
        /// Exception message if the item is null.
        /// </param>
        /// <returns>
        /// The object is returned to allow a fluent interface with
        /// other guards.
        /// </returns>
        public static object GuardIsNotNull(this object item, string argumentName, string exceptionMessage)
        {
            if (item == null)
            {
                throw new ArgumentNullException(argumentName, exceptionMessage);
            }

            return item;
        }

        /// <summary>
        /// Guard that a required item is available for an operation.
        /// This tests that the item is null and if it is, throws an exception.
        /// </summary>
        /// <param name="item">Item guard is called on.</param>
        /// <param name="exceptionMessage">
        /// Message explaining why the operation is invalid.
        /// </param>
        /// <returns>
        /// The object is returned to allow a fluent interface with
        /// other guards.
        /// </returns>
        public static object GuardIsRequiredForOperation(this object item, string exceptionMessage)
        {
            if (item == null)
            {
                throw new InvalidOperationException(exceptionMessage);
            }

            return item;
        }

        /// <summary>
        /// Guard that an item is not the default for it's type.
        /// </summary>
        /// <param name="item">Item guard is called on.</param>
        /// <param name="paramName">
        /// Name provided to help consumer of exception identify the problem.
        /// </param>
        /// <returns>
        /// The object is returned to allow a fluent interface with
        /// other guards.
        /// </returns>
        /// <remarks>
        /// For reference types, this is essentially the same as the
        /// GuardIsNotNull() method.
        /// </remarks>
        public static T GuardIsNotDefault<T>(this T item, string paramName)
        {
            if (typeof(T).IsValueType)
            {
                if (item.Equals(default(T)))
                {
                    var exMessage = $"The item cannot be the default value for this type ({typeof(T).Name}).";
                    throw new ArgumentException(exMessage, paramName);
                }
            }
            else
            {
                if (item == null)
                {
                    throw new ArgumentNullException(paramName, "The item cannot be null.");
                }
            }

            return (T)item;
        }
    }
}
