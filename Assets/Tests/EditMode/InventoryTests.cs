using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CalangoGames
{
    public class InventoryTests
    {
        InventoryManager inventoryManager;
        List<Item> playerItems;
        Item item1;
        Item item2;
        Item item3;

        GameObject gameObject;

        [SetUp]
        public void BeforeEveryTest()
        {
            gameObject = new GameObject();
            inventoryManager = gameObject.AddComponent<InventoryManager>();
            playerItems = new List<Item>();
            item1 = new Item("item1", 100);
            item2 = new Item("item2", 150);
            item3 = new Item("item3", 100, 10);

            playerItems.Add(item1);
            playerItems.Add(item2);

            inventoryManager.PlayerItems = playerItems;
        }

        [TearDown]
        public void AfterEveryTest()
        {
            
        }

        [Test]
        public void WhenItemIsRemovedFromInventoryThenItemIsUnequiped()
        {
            
            // Assing
            // Act
            inventoryManager.RemoveItemFromInventory(item1);
            // Assert
            Assert.AreEqual(expected: true, actual: item1.IsEquiped);
        }
    }
}
