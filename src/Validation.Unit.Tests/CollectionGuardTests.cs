using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Gpm.Validation;

namespace Gpm.Validation.Unit.Tests
{
    [TestFixture]
    public class CollectionGuardTests
    {
        [Test]
        public void GuardHasElements_WithListNoElements_ThrowsException()
        {
            // Arrange
            var errorMessage = "any";
            var emptyList = new List<int>();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => emptyList.GuardHasElements(errorMessage));

            // Assert
            StringAssert.Contains(expected: errorMessage, actual: ex.Message);
        }

        [Test]
        public void GuardHasElements_WithArrayListNoElements_ThrowsException()
        {
            // Arrange
            var errorMessage = "any";
            var emptyArrayList = new ArrayList();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => emptyArrayList.GuardHasElements(errorMessage));

            // Assert
            StringAssert.Contains(expected: errorMessage, actual: ex.Message);
        }

        [Test]
        public void GuardHasElements_WithDictionaryNoElements_ThrowsException()
        {
            // Arrange
            var errorMessage = "any";
            var emptyDictionary = new Dictionary<int, int>();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => emptyDictionary.GuardHasElements(errorMessage));

            // Assert
            StringAssert.Contains(expected: errorMessage, actual: ex.Message);
        }

        [Test]
        public void GuardHasItems_WithHashSetNoElements_ThrowsException()
        {
            // Arrange
            var errorMessage = "any";
            var emptyHashSet = new HashSet<int>();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => emptyHashSet.GuardHasItems(errorMessage));

            // Assert
            StringAssert.Contains(expected: errorMessage, actual: ex.Message);
        }

        [Test]
        public void GuardHasAtLeast_WithTwoItemList_ThrowsException()
        {
            // Arrange
            var minCount = 3;
            var errorMessage = "any";
            var list = new List<int> { 1, 2 };

            // Act
            var ex = Assert.Throws<InvalidOperationException>(
                () => list.GuardHasAtLeast(minCount, errorMessage));

            // Assert
            StringAssert.Contains(expected: errorMessage, actual: ex.Message);
        }

        [Test]
        public void GuardHasAtLeast_WithThreeItemList_DoesNotThrowException()
        {
            // Arrange
            var minCount = 3;
            var errorMessage = "any";
            var list = new List<int> { 1, 2, 3 };

            // Act and Assert
            Assert.DoesNotThrow(
                () => list.GuardHasAtLeast(minCount, errorMessage));
        }

        [Test]
        public void GuardHasAtLeast_WithTwoItemArrayList_ThrowsException()
        {
            // Arrange
            var minCount = 3;
            var errorMessage = "any";
            var arrayList = new ArrayList { 1, 2 };

            // Act
            var ex = Assert.Throws<InvalidOperationException>(
                () => arrayList.GuardHasAtLeast(minCount, errorMessage));

            // Assert
            StringAssert.Contains(expected: errorMessage, actual: ex.Message);
        }

        [Test]
        public void GuardHasAtLeast_WithThreeItemArrayList_DoesNotThrowException()
        {
            // Arrange
            var minCount = 3;
            var errorMessage = "any";
            var arrayList = new ArrayList { 1, 2, 3 };

            // Act and Assert
            Assert.DoesNotThrow(
                () => arrayList.GuardHasAtLeast(minCount, errorMessage));
        }

        [Test]
        public void GuardHasAtLeast_WithTwoItemDictionary_ThrowsException()
        {
            // Arrange
            var minCount = 3;
            var errorMessage = "any";
            var dictionary = new Dictionary<int, int> { { 1, 1 }, { 2, 2 } };

            // Act
            var ex = Assert.Throws<InvalidOperationException>(
                () => dictionary.GuardHasAtLeast(minCount, errorMessage));

            // Assert
            StringAssert.Contains(expected: errorMessage, actual: ex.Message);
        }

        [Test]
        public void GuardHasAtLeast_WithThreeItemDictionary_DoesNotThrowException()
        {
            // Arrange
            var minCount = 3;
            var errorMessage = "any";
            var dictionary = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 3 } };

            // Act and Assert
            Assert.DoesNotThrow(
                () => dictionary.GuardHasAtLeast(minCount, errorMessage));
        }

        [Test]
        public void GuardHasAtLeastItems_WithTwoItemHasSet_ThrowsException()
        {
            // Arrange
            var minCount = 3;
            var errorMessage = "any";
            var hashSet = new HashSet<int> { 1, 2 };

            // Act
            var ex = Assert.Throws<InvalidOperationException>(
                () => hashSet.GuardHasAtLeastItems(minCount, errorMessage));

            // Assert
            StringAssert.Contains(expected: errorMessage, actual: ex.Message);
        }

        [Test]
        public void GuardHasAtLeastItems_WithThreeItemHasSet_DoesNotThrowException()
        {
            // Arrange
            var minCount = 3;
            var errorMessage = "any";
            var hashSet = new HashSet<int> { 1, 2, 3 };

            // Act And Assert
            Assert.DoesNotThrow(
                () => hashSet.GuardHasAtLeastItems(minCount, errorMessage));
        }
    }
}
