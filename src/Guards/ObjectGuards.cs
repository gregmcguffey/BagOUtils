using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagOUtils.Guards.Messages;

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
        public static object GuardIsNotNull(this object item, string argumentName)
        {
            var message = ItemTemplate
                .IsNull
                .UsingItem(argumentName)
                .Prepare();

            return item
                .GuardIsNotNullWithMessage(argumentName, message);
        }

        /// <summary>
        /// Guard that the item is not null.
        /// </summary>
        /// <param name="item">Item guard is called on.</param>
        /// <param name="argumentName">
        /// Name of argument/parameter/state that is being guarded.
        /// </param>
        /// <param name="message">
        /// Exception message if the item is null.
        /// </param>
        /// <returns>
        /// The object is returned to allow a fluent interface with
        /// other guards.
        /// </returns>
        public static object GuardIsNotNullWithMessage(this object item, string argumentName, string message)
        {
            if (item == null)
            {
                throw new ArgumentNullException(argumentName, message);
            }

            return item;
        }

        /// <summary>
        /// Guard that a required item is available for an operation.
        /// This tests that the item is null and if it is, throws an exception.
        /// </summary>
        /// <param name="item">Item guard is called on.</param>
        /// <param name="argumentName">Name of argument.</param>
        /// <param name="operation">The operation requiring a value.</param>
        /// <returns>
        /// The object is returned to allow a fluent interface with
        /// other guards.
        /// </returns>
        public static object GuardIsRequiredForOperation(this object item, string argumentName, string operation)
        {
            var message = ItemValueTemplate
                .MissingForOperation
                .UsingItem(argumentName)
                .UsingValue(operation)
                .Prepare();

            return item
                .GuardIsRequiredForOperationWithMessage(message);
        }

        /// <summary>
        /// Guard that a required item is available for an operation.
        /// This tests that the item is null and if it is, throws an exception.
        /// </summary>
        /// <param name="item">Item guard is called on.</param>
        /// <param name="message">
        /// Message explaining why the operation is invalid.
        /// </param>
        /// <returns>
        /// The object is returned to allow a fluent interface with
        /// other guards.
        /// </returns>
        public static object GuardIsRequiredForOperationWithMessage(this object item, string message)
        {
            if (item == null)
            {
                throw new InvalidOperationException(message);
            }

            return item;
        }

        /// <summary>
        /// Guard that an item is not the default for it's type.
        /// </summary>
        /// <param name="item">Item guard is called on.</param>
        /// <param name="argumentName">
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
        public static T GuardIsNotDefault<T>(this T item, string argumentName)
        {
            var message = ItemValueTemplate
                .DefaultNotAllowed
                .UsingItem(argumentName)
                .UsingValue(typeof(T).Name)
                .Prepare();

            return item
                .GuardIsNotDefaultWithMessage(argumentName, message);
        }

        /// <summary>
        /// Guard that an item is not the default for it's type.
        /// </summary>
        /// <param name="item">Item guard is called on.</param>
        /// <param name="argumentName">
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
        public static T GuardIsNotDefaultWithMessage<T>(this T item, string argumentName, string message)
        {
            if (typeof(T).IsValueType)
            {
                if (item.Equals(default(T)))
                {
                    throw new ArgumentException(message, argumentName);
                }
            }
            else
            {
                if (item == null)
                {
                    throw new ArgumentNullException(argumentName, "The item cannot be null.");
                }
            }

            return (T)item;
        }
    }
}
