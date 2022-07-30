using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private List<Item> playerItems;
        // Start is called before the first frame update

        public void AddItemToInventory(Item item)
        {
            playerItems.Add(item);
        }
        
        public void RemoveItemFromInventory(string itemName)
        {
            var itemToRemove = playerItems.Find(x => x.itemName == itemName);
            playerItems.Remove(itemToRemove);
        }

        public List<Item> GetPlayerItemsList()
        {
            return playerItems == null ? new List<Item>() : playerItems;
        }
    }
}
