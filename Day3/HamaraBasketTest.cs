using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace hamaraBasket
{
    [TestFixture]
    public class HamaraBasketTest
    {
        Factory domain;
        [SetUp]
        public void Setup()
        {
            this.domain = new Factory();
        }
        [Test]
        public void ShouldReduceSellInValueByOneEOD()
        {
            //arrange
            var Items = domain.PrepareItemList("foo",10,10);

            var newItems = domain.PrepareItemList("foo", 10, 10);
            //Action


            domain.UpdateQualityHelper(newItems);
            Assert.That(newItems[0].SellIn, Is.EqualTo(Items[0].SellIn-1));
        }
        [Test]
        public void ShouldReduceQualityByOneEOD()
        {
            //arrange
            var Items = domain.PrepareItemList("foo", 10, 10);

            var newItems = domain.PrepareItemList("foo", 10, 10);

            //Action


            domain.UpdateQualityHelper(newItems);

            Assert.That(newItems[0].Quality, Is.EqualTo(Items[0].Quality-1));
        }

        [Test]
        public void ShouldDegradeQualityTwiceAsFastAfterSellByDate()
        {

            var items = domain.PrepareItemList("foo", 0, 10);

            var newItems = domain.PrepareItemList("foo", 0, 10);
            // Act
            domain.UpdateQualityHelper(items);

            // Assert
            Assert.That(items[0].Quality, Is.EqualTo(newItems[0].Quality-2));
        }

        [Test]
        public void QualityShouldNeverBeNegative()
        {
            // Arrange

            var items = domain.PrepareItemList("foo", 10, 0);

            var newItems = domain.PrepareItemList("foo", 10, 0);
            // Act
            domain.UpdateQualityHelper(items);

            // Assert
            Assert.That(items[0].Quality, Is.EqualTo(0));
        }

        [Test]
        public void IndianWineShouldIncreaseInQuality()
        {
            // Arrange

            var items = domain.PrepareItemList("Indian Wine", 10, 10);

            var newItems = domain.PrepareItemList("Indian Wine", 10, 10);
            // Act
            domain.UpdateQualityHelper(newItems);

            // Assert
            Assert.That(newItems[0].Quality, Is.EqualTo(items[0].Quality+1));
        }

        [Test]
        public void QualityShouldNeverExceedFifty()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Indian Wine", SellIn = 10, Quality = 50 } };

            // Act
            domain.UpdateQualityHelper(items);

            // Assert
            Assert.That(items[0].Quality, Is.EqualTo(50));
        }

        [Test]
        public void ForestHoneyShouldNeverDecreaseInQuality()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Forest Honey", SellIn = 10, Quality = 10 } };

            // Act
            domain.UpdateQualityHelper(items);

            // Assert
            Assert.That(items[0].Quality, Is.EqualTo(10));
        }

        [Test]
        public void MovieTicketsShouldIncreaseInQualityByTwoWhenTenDaysOrLess()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Movie Tickets", SellIn = 10, Quality = 10 } };

            // Act
            domain.UpdateQualityHelper(items);

            // Assert
            Assert.That(items[0].Quality, Is.EqualTo(12));
        }

        [Test]
        public void MovieTicketsShouldIncreaseInQualityByThreeWhenFiveDaysOrLess()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Movie Tickets", SellIn = 5, Quality = 10 } };

            // Act
            domain.UpdateQualityHelper(items);

            // Assert
            Assert.That(items[0].Quality, Is.EqualTo(13));
        }

        [Test]
        public void MovieTicketsShouldDropToZeroAfterShow()
        {
            // Arrange
            var items = new List<Item> { new Item { Name = "Movie Tickets", SellIn = 0, Quality = 10 } };

            // Act
            domain.UpdateQualityHelper(items);

            // Assert
            Assert.That(items[0].Quality, Is.EqualTo(0));
        }
    }
}