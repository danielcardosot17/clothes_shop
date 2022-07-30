using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CalangoGames
{
    public class ShopTests
    {
        MoneyManager moneyManager;
        ShopManager shopManager;
        InventoryManager inventoryManager;
        List<Item> buyItems;
        List<Item> sellItems;
        Item item1;
        Item item2;
        Item item3;

        [SetUp]
        public void BeforeEveryTest()
        {
            moneyManager = new MoneyManager();
            shopManager = new ShopManager();
            inventoryManager = new InventoryManager();
            buyItems = new List<Item>();
            sellItems = new List<Item>();
            item1 = new Item();
            item2 = new Item();
            item3 = new Item();

            buyItems.Add(item1);
            buyItems.Add(item2);
            sellItems.Add(item3);
        }

        [TearDown]
        public void AfterEveryTest()
        {
            
        }
        // A Test behaves as an ordinary method
        [Test]
        public void WhenBuyItemThenMoneyIsSpent()
        {
            // Assing
            // Act
            shopManager.TryBuyItem(item1);
            // Assert
            Assert.AreEqual(expected: 0, actual: moneyManager.Money);
        }
    }
}
