using System;
using VideoGameOrderSystem.Library;
using Xunit;

namespace VideoGameOrderSystem.Test
{
    public class LocationTest
    {
        [Fact]
        public void IsEmptyReturnsTrueWhenEmpty()
        {
            // Arrange
            Location loc = new Location();

            // Act

            // Assert
            Assert.True(loc.IsEmpty());
        }

        [Fact]
        public void IsEmptyReturnsFalseWhenNotEmpty()
        {
            // Arrange
            Location loc = new Location();
            Product p = new Product();

            // Act
            loc.AddProductToInventory(p);

            // Assert
            Assert.False(loc.IsEmpty());
        }

        [Fact]
        public void AddProductIsSuccessfulAndContainsReturnsTrue()
        {
            //Arrange
            Location loc = new Location();
            Product p = new Product();

            // Act
            loc.AddProductToInventory(p);

            // Assert
            Assert.True(loc.Contains(p.Id));
        }

        [Fact]
        public void RemoveProductIsSuccessfulAndContainsReturnsFalse()
        {
            //Arrange
            Location loc = new Location();
            Product p = new Product();

            // Act
            loc.AddProductToInventory(p);
            loc.RemoveProductFromInventory(p);

            // Assert
            Assert.False(loc.Contains(p.Id));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        public void AddItemsAndCheckInventoryAreSuccessful(int val)
        {
            //Arrange
            Location loc = new Location();
            Product p = new Product();
            p.Quantity = 1;
            int inc = val;

            // Act
            loc.AddProductToInventory(p);
            loc.AddItemsToInventory(p.Id, inc);

            // Assert
            Assert.True(loc.CheckInventory(p.Id) == inc + 1);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        public void RemoveItemsAndCheckInventoryAreSuccessful(int val)
        {
            //Arrange
            Location loc = new Location();
            Product p = new Product();
            p.Quantity = 10;
            int dec = val;

            // Act
            loc.AddProductToInventory(p);
            loc.RemoveItemsFromInventory(p.Id, dec);

            // Assert
            Assert.True(loc.CheckInventory(p.Id) == 10-dec);
        }


    }
}
