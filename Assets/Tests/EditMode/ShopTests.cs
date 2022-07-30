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
        Shopkeeper shopkeeper;
        InventoryManager inventoryManager;
        List<Item> buyItems;
        List<Item> sellItems;
        Item item1;
        Item item2;
        Item item3;

        GameObject gameObject;

        [SetUp]
        public void BeforeEveryTest()
        {
            gameObject = new GameObject();
            moneyManager = gameObject.AddComponent<MoneyManager>();
            shopManager = gameObject.AddComponent<ShopManager>();
            inventoryManager = gameObject.AddComponent<InventoryManager>();
            shopkeeper = gameObject.AddComponent<Shopkeeper>();
            buyItems = new List<Item>();
            sellItems = new List<Item>();
            item1 = new Item("item1", 100);
            item2 = new Item("item2", 150);
            item3 = new Item("item3", 100);

            buyItems.Add(item1);
            buyItems.Add(item2);
            sellItems.Add(item3);

            shopkeeper.SetItemList(buyItems);
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
            moneyManager.TryBuyItem(item1);
            // Assert
            Assert.AreEqual(expected: 0, actual: moneyManager.Money);
        }

        [Test]
        public void WhenTryBuyItemIfMoneyIsNotEnoughThenMoneyIsNotSpent()
        {
            // Assing
            // Act
            moneyManager.TryBuyItem(item2);
            // Assert
            Assert.AreEqual(expected: 100, actual: moneyManager.Money);
        }
        
        [Test]
        public void WhenBuyItemThenRemoveFromShopkeeperList()
        {
            // Assing
            // Act
            moneyManager.TryBuyItem(item1);
            // Assert
            Assert.AreEqual(expected: false, actual: shopkeeper.GetItemList().Contains(item1));
        }
    }
}
