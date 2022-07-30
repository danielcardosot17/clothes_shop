using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private List<Item> playerItems;

        public List<Item> PlayerItems { get => playerItems; set => playerItems = value; }

        // Start is called before the first frame update

        public void AddItemToInventory(Item item)
        {
            playerItems.Add(item);
        }
        
        public void RemoveItemFromInventory(string itemName)
        {
            var itemToRemove = PlayerItems.Find(x => x.itemName == itemName);
            playerItems.Remove(itemToRemove);
        }
    }
}
