using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BagOUtils.Guards.Messages;

namespace BagOUtils.Guards.Unit.Tests
{
    [TestFixture]
    public class CollectionGuardTests
    {

        //-------------------------------------------------------------------------
        //
        // GuardHadElements Tests
        //
        //-------------------------------------------------------------------------

        [Test]
        public void GuardHadElements_WithOneElement_ReturnsCollection()
        {
            // Arrange
            var list = new List<int> { 1 };

            // Act
            var returnedList = list.GuardHasElements("any");

            // Assert
            Assert.AreSame(list, returnedList);
        }

        [Test]
        public void GuardHasElements_WithListNoElements_ThrowsException()
        {
            // Arrange
            var item = "anyItem";
            var errorMessage = ItemTemplate
                .NoElements
                .UsingItem(item)
                .Prepare();
            var emptyList = new List<int>();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => emptyList.GuardHasElements(item));

            // Assert
            StringAssert.Contains(expected: errorMessage, actual: ex.Message);
        }

        [Test]
        public void GuardHasElements_WithArrayListNoElements_ThrowsException()
        {
            // Arrange
            var item = "anyItem";
            var errorMessage = ItemTemplate
                .NoElements
                .UsingItem(item)
                .Prepare();
            var emptyArrayList = new ArrayList();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => emptyArrayList.GuardHasElements(item));

            // Assert
            StringAssert.Contains(expected: errorMessage, actual: ex.Message);
        }

        [Test]
        public void GuardHasElements_WithDictionaryNoElements_ThrowsException()
        {
            // Arrange
            var item = "anyItem";
            var errorMessage = ItemTemplate
                .NoElements
                .UsingItem(item)
                .Prepare();
            var emptyDictionary = new Dictionary<int, int>();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => emptyDictionary.GuardHasElements(item));

            // Assert
            StringAssert.Contains(expected: errorMessage, actual: ex.Message);
        }


        //-------------------------------------------------------------------------
        //
        // GuardHasItems Tests
        //
        //-------------------------------------------------------------------------


        [Test]
        public void GuardHasItems_WithOneItem_ReturnsList()
        {
            // Arrange
            var hashSet = new HashSet<int> { 1 };

            // Act
            var returnedHashSet = hashSet.GuardHasItems("any");

            // Assert
            Assert.AreSame(hashSet, returnedHashSet);
        }

        [Test]
        public void GuardHasItems_WithHashSetNoElements_ThrowsException()
        {
            // Arrange
            var item = "anyItem";
            var errorMessage = ItemTemplate
                .NoItems
                .UsingItem(item)
                .Prepare();
            var emptyHashSet = new HashSet<int>();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => emptyHashSet.GuardHasItems(item));

            // Assert
            StringAssert.Contains(expected: errorMessage, actual: ex.Message);
        }


        //-------------------------------------------------------------------------
        //
        // GuardHasAtLeast Tests
        //
        //-------------------------------------------------------------------------

        [Test]
        public void GuardHasAtLeast_WithListTooFewItems_ThrowsException()
        {
            // Arrange
            var minCount = 3;
            var item = "anyList";
            var list = new List<int> { 1, 2 };
            var errorMessage = CustomTemplate
                .TooFewElements
                .UsingItem(item)
                .UsingValue(list.Count)
                .ComparedTo(minCount)
                .Prepare();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(
                () => list.GuardHasAtLeast(minCount, item));

            // Assert
            StringAssert.Contains(errorMessage, ex.Message);
        }

        [Test]
        public void GuardHasAtLeast_WithListMinItems_ReturnsCollection()
        {
            // Arrange
            var minCount = 3;
            var item = "anyList";
            var list = new List<int> { 1, 2, 3 };

            // Act
            var returnedList = list.GuardHasAtLeast(minCount, item);

            // Assert
            Assert.AreSame(list, returnedList);
        }

        [Test]
        public void GuardHasAtLeast_WithArrayListTooFewItems_ThrowsException()
        {
            // Arrange
            var minCount = 3;
            var item = "anyArrayList";
            var arrayList = new ArrayList { 1, 2 };
            var errorMessage = CustomTemplate
                .TooFewElements
                .UsingItem(item)
                .UsingValue(arrayList.Count)
                .ComparedTo(minCount)
                .Prepare();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(
                () => arrayList.GuardHasAtLeast(minCount, item));

            // Assert
            StringAssert.Contains(errorMessage, ex.Message);
        }

        [Test]
        public void GuardHasAtLeast_WithArrayListMinItems_ReturnsArrayList()
        {
            // Arrange
            var minCount = 3;
            var item = "anyArrayList";
            var arrayList = new ArrayList { 1, 2, 3 };

            // Act
            var returned = arrayList.GuardHasAtLeast(minCount, item);

            // Assert
            Assert.AreSame(arrayList, returned);
        }

        [Test]
        public void GuardHasAtLeast_WithDictionaryTooFewItems_ThrowsException()
        {
            // Arrange
            var minCount = 3;
            var item = "any";
            var dictionary = new Dictionary<int, int> { { 1, 1 }, { 2, 2 } };
            var errorMessage = CustomTemplate
                .TooFewElements
                .UsingItem(item)
                .UsingValue(dictionary.Count)
                .ComparedTo(minCount)
                .Prepare();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(
                () => dictionary.GuardHasAtLeast(minCount, item));

            // Assert
            StringAssert.Contains(errorMessage, ex.Message);
        }

        [Test]
        public void GuardHasAtLeast_WithDictionaryMinItems_ReturnsDictionary()
        {
            // Arrange
            var minCount = 3;
            var item = "any";
            var dictionary = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } };

            // Act
            var returned = dictionary.GuardHasAtLeast(minCount, item);

            // Asswert
            Assert.AreSame(dictionary, returned);
        }


        //-------------------------------------------------------------------------
        //
        // GuardHasAtLeastItems Tests
        //
        //-------------------------------------------------------------------------

        [Test]
        public void GuardHasAtLeastItems_WithTwoItemHasSet_ThrowsException()
        {
            // Arrange
            var minCount = 3;
            var item = "any";
            var hashSet = new HashSet<int> { 1, 2 };
            var errorMessage = CustomTemplate
                .TooFewElements
                .UsingItem(item)
                .UsingValue(hashSet.Count)
                .ComparedTo(minCount)
                .Prepare();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(
                () => hashSet.GuardHasAtLeastItems(minCount, item));

            // Assert
            StringAssert.Contains(errorMessage, ex.Message);
        }

        [Test]
        public void GuardHasAtLeastItems_WithThreeItemHasSet_DoesNotThrowException()
        {
            // Arrange
            var minCount = 3;
            var item = "any";
            var hashSet = new HashSet<int> { 1, 2, 3 };

            // Act
            var returned = hashSet.GuardHasAtLeastItems(minCount, item);

            // Assert
            Assert.AreSame(hashSet, returned);
        }
    }
}
