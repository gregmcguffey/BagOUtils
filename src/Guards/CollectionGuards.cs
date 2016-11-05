using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagOUtils.Guards.Messages;

namespace BagOUtils.Guards
{
    /// <summary>
    /// Defend against invalid objects that implement ICollection
    /// (normal or generic) by validating counts and contents. This
    /// includes ArrayList, List, Dictionary, Queue, Stack, LinkedList,
    /// Hashtable, SortedList, SortedSet, etc.
    /// </summary>
    public static class CollectionGuards
    {
        /// <summary>
        /// Guard that a collection has at least one element.
        /// </summary>
        /// <param name="collection">Collection to test.</param>
        /// <param name="itemName">
        /// Message if the collection fails the test.
        /// </param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <remarks>
        /// This uses an item template for the exception message.
        /// </remarks>
        public static ICollection GuardHasElements(this ICollection collection, string itemName)
        {
            Func<string> messageBuilder = () =>
            {
                var message = ItemTemplate
                 .NoElements
                 .UsingItem(itemName)
                 .Prepare();

                return message;
            };

            return collection.GuardHasElementsWithMessage(messageBuilder);
        }

        /// <summary>
        /// Guard that a collection has at least one element.
        /// </summary>
        /// <param name="collection">Collection to test.</param>
        /// <param name="message">
        /// Message if the collection fails the test.
        /// </param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <remarks>
        /// This allows a custom message as the exception message.
        /// </remarks>
        public static ICollection GuardHasElementsWithMessage(this ICollection collection, Func<string> messageBuilder)
        {
            if (collection.Count == 0)
            {
                throw new InvalidOperationException(messageBuilder());
            }
            return collection;
        }

        /// <summary>
        /// Guard that a collection has at least one element. This
        /// handles HashSet. This is named differently than the one for
        /// ICollection because most of the classes implement both
        /// standard and generic versions ICollection. By naming them
        /// differently, no casts are needed to use the guards.
        /// </summary>
        /// <param name="collection">Collection to test.</param>
        /// <param name="itemName">
        /// Message if the collection fails the test.
        /// </param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <remarks>
        /// This uses an item template for the exception message.
        /// </remarks>
        public static ICollection<T> GuardHasItems<T>(this ICollection<T> collection, string itemName)
        {
            Func<string> messageBuilder = () =>
            {
                var message = ItemTemplate
                .NoItems
                .UsingItem(itemName)
                .Prepare();
                return message;
            };

            return collection.GuardHasItemsWithMessage(messageBuilder);
        }

        /// <summary>
        /// Guard that a collection has at least one element. This
        /// handles HashSet. This is named differently than the one for
        /// ICollection because most of the classes implement both
        /// standard and generic versions ICollection. By naming them
        /// differently, no casts are needed to use the guards.
        /// </summary>
        /// <param name="collection">Collection to test.</param>
        /// <param name="message">
        /// Message if the collection fails the test.
        /// </param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <remarks>
        /// This allows a custom message as the exception message.
        /// </remarks>
        public static ICollection<T> GuardHasItemsWithMessage<T>(this ICollection<T> collection, Func<string> messageBuilder)
        {
            if (collection.Count == 0)
            {
                throw new InvalidOperationException(messageBuilder());
            }
            return collection;
        }

        /// <summary>
        /// Guard that a collection has at least some required minimum
        /// number of elements.
        /// </summary>
        /// <param name="collection">Collection to test.</param>
        /// <param name="minCount">Minimum number of elements.</param>
        /// <param name="message">
        /// Message if the collection fails the test.
        /// </param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <remarks>
        /// This uses an item template for the exception message.
        /// </remarks>
        public static ICollection GuardHasAtLeast(this ICollection collection, int minCount, string itemName)
        {
            Func<string> messageBuilder = () =>
            {
                var message = CustomTemplate
                .TooFewElements
                .UsingItem(itemName)
                .UsingValue(collection.Count)
                .ComparedTo(minCount)
                .Prepare();
                return message;
            };

            return collection.GuardHasAtLeastWithMessage(minCount, messageBuilder);
        }

        /// <summary>
        /// Guard that a collection has at least some required minimum
        /// number of elements.
        /// </summary>
        /// <param name="collection">Collection to test.</param>
        /// <param name="minCount">Minimum number of elements.</param>
        /// <param name="message">
        /// Message if the collection fails the test.
        /// </param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <remarks>
        /// This allows a custom message as the exception message.
        /// </remarks>
        public static ICollection GuardHasAtLeastWithMessage(this ICollection collection, int minCount, Func<string> messageBuilder)
        {
            if (collection.Count < minCount)
            {
                throw new InvalidOperationException(messageBuilder());
            }
            return collection;
        }

        /// <summary>
        /// Guard that a collection has at least some required minimum
        /// number of elements. This is named differently than the one
        /// for ICollection because most of the classes implement both
        /// standard and generic versions ICollection. By naming them
        /// differently, no casts are needed to use the guards.
        /// </summary>
        /// <param name="collection">Collection to test.</param>
        /// <param name="minCount">Minimum number of elements.</param>
        /// <param name="message">
        /// Message if the collection fails the test.
        /// </param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <remarks>
        /// This uses an item template for the exception message.
        /// </remarks>
        public static ICollection<T> GuardHasAtLeastItems<T>(this ICollection<T> collection, int minCount, string itemName)
        {
            Func<string> messageBuilder = () =>
            {
                var message = CustomTemplate
                .TooFewElements
                .UsingItem(itemName)
                .UsingValue(collection.Count)
                .ComparedTo(minCount)
                .Prepare();
                return message;
            };

            return collection.GuardHasAtLeastItemsWithMessage(minCount, messageBuilder);
        }

        /// <summary>
        /// Guard that a collection has at least some required minimum
        /// number of elements. This is named differently than the one
        /// for ICollection because most of the classes implement both
        /// standard and generic versions ICollection. By naming them
        /// differently, no casts are needed to use the guards.
        /// </summary>
        /// <param name="collection">Collection to test.</param>
        /// <param name="minCount">Minimum number of elements.</param>
        /// <param name="message">
        /// Message if the collection fails the test.
        /// </param>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <remarks>
        /// This allows a custom message as the exception message.
        /// </remarks>
        public static ICollection<T> GuardHasAtLeastItemsWithMessage<T>(this ICollection<T> collection, int minCount, Func<string> messageBuilder)
        {
            if (collection.Count < minCount)
            {
                throw new InvalidOperationException(messageBuilder());
            }
            return collection;
        }
    }
}
