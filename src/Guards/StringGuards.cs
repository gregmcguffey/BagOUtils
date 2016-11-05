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
    /// state/parameter/argument is a string. Each of the methods
    /// defines a "guard". The guard validates that the
    /// state/parameter/argument does not have a particular type of
    /// potential error. If the guard finds the
    /// state/parameter/argument to be in error, an exception is
    /// thrown. If the value is validated, the value is returned,
    /// allowing for a fluent guard API.
    /// </summary>
    public static class StringGuards
    {
        /// <summary>
        /// Guard that a string variable has been set. This means that
        /// is not null, empty or only white space.
        /// </summary>
        /// <param name="value">String being tested.</param>
        /// <param name="argumentName">
        /// Name of the argument/parameter/state that is being tested.
        /// </param>
        /// <returns>The string being tested, to allow fluent interface.</returns>
        /// <remarks>
        /// This uses the IsSet item template.
        /// </remarks>
        public static string GuardIsSet(this string value, string argumentName)
        {
            Func<string> messageBuilder = () =>
            {
                var message = ItemTemplate
                .IsSet
                .UsingItem(argumentName)
                .Prepare();
                return message;
            };

            return value
                .GuardIsSetWithMessage(argumentName, messageBuilder);
        }

        /// <summary>
        /// Guard that a string variable has been set. This means that
        /// is not null, empty or only white space.
        /// </summary>
        /// <param name="value">String being tested.</param>
        /// <param name="argumentName">
        /// Name of the argument/parameter/state that is being tested.
        /// </param>
        /// <returns>The string being tested, to allow fluent interface.</returns>
        /// <remarks>
        /// This uses a custom message.
        /// </remarks>
        public static string GuardIsSetWithMessage(this string value, string argumentName, Func<string> messageBuilder)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(messageBuilder(), argumentName);
            }

            return value;
        }

        /// <summary>
        /// Guard that a string is of the required length. This assumes
        /// that the length must be greater than zero.
        /// </summary>
        /// <param name="value">String being tested.</param>
        /// <param name="argumentName">
        /// Name of the argument/parameter/state that is being tested.
        /// </param>
        /// <param name="requiredLength">Required length of text.</param>
        /// <returns>The string being tested, to allow fluent interface.</returns>
        /// <remarks>
        /// This uses the NotRequiredSize item template.
        /// </remarks>
        public static string GuardRequiredLength(this string value, string argumentName, int requiredLength)
        {
            Func<string> messageBuilder = () =>
            {
                var message = CustomTemplate
                .NotRequiredSize
                .UsingItem(argumentName)
                .UsingValue(value)
                .RequiringLength(requiredLength)
                .Prepare();
                return message;
            };

            return value
                .GuardRequiredLengthWithMessage(argumentName, requiredLength, messageBuilder);
        }

        /// <summary>
        /// Guard that a string is of the required length. This assumes
        /// that the length must be greater than zero.
        /// </summary>
        /// <param name="value">String being tested.</param>
        /// <param name="argumentName">
        /// Name of the argument/parameter/state that is being tested.
        /// </param>
        /// <param name="requiredLength">Required length of text.</param>
        /// <returns>The string being tested, to allow fluent interface.</returns>
        /// <remarks>
        /// This uses the NotRequiredSize item template.
        /// </remarks>
        public static string GuardRequiredLengthWithMessage(this string value, string argumentName, int requiredLength, Func<string> messageBuilder)
        {
            requiredLength.GuardMinimumWithMessage(nameof(requiredLength), 1, Message.BadGuardRequiredLength.ToLambda());

            // Null strings are converted to an empty string.
            var testedValue = value.NullToEmpty();

            int actualLength = testedValue.Length;
            if (actualLength != requiredLength)
            {
                throw new ArgumentOutOfRangeException(argumentName, messageBuilder());
            }

            return value;
        }

        /// <summary>
        /// Guard that a string is a valid size.
        /// </summary>
        /// <param name="value">String being tested.</param>
        /// <param name="argumentName">
        /// Name of the argument/parameter/state that is being tested.
        /// </param>
        /// <param name="min">Minimum length.</param>
        /// <param name="max">Maximum length.</param>
        /// <returns>The string being tested, to allow fluent interface.</returns>
        /// <remarks>
        /// This uses the TextSizeOutOfRange custom template message.
        /// </remarks>
        public static string GuardSize(this string value, string argumentName, int min, int max)
        {
            Func<string> messageBuilder = () =>
            {
                var message = CustomTemplate
                .TextSizeOutOfRange
                .UsingItem(argumentName)
                .UsingValue(value)
                .WithMinimum(min)
                .WithMaximum(max)
                .Prepare();
                return message;
            };

            return value
                .GuardSizeWithMessage(argumentName, min, max, messageBuilder);
        }

        /// <summary>
        /// Guard that a string is a valid size.
        /// </summary>
        /// <param name="value">String being tested.</param>
        /// <param name="argumentName">
        /// Name of the argument/parameter/state that is being tested.
        /// </param>
        /// <param name="min">Minimum length.</param>
        /// <param name="max">Maximum length.</param>
        /// <returns>The string being tested, to allow fluent interface.</returns>
        /// <remarks>
        /// This uses the TextSizeOutOfRange custom template message.
        /// </remarks>
        public static string GuardSizeWithMessage(this string value, string argumentName, int min, int max, Func<string> messageBuilder)
        {
            int actualSize = value?.Length ?? 0;
            var isCorrectSize = min <= actualSize && actualSize <= max;
            if (!isCorrectSize)
            {
                throw new ArgumentException(messageBuilder(), argumentName);
            }

            return value;
        }
    }
}
