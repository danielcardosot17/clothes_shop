using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private List<Item> playerItems;
        [SerializeField] private Transform playerGraphics;

        public List<Item> PlayerItems { get => playerItems; set => playerItems = value; }

        // Start is called before the first frame update

        private void Start() {
            foreach(var item in playerItems)
            {
                EquipItem(item);
            }
        }

        public void AddItemToInventory(Item item)
        {
            EquipItem(item);
            playerItems.Add(item);
        }
        
        public void RemoveItemFromInventory(string itemName)
        {
            var itemToRemove = PlayerItems.Find(x => x.itemName == itemName);
            UnequipItem(itemToRemove);
            playerItems.Remove(itemToRemove);
        }

        private void UnequipItem(Item item)
        {
            item.IsEquiped = false;
            RemovePlayerSprite(item);
            EquipNextItemOfSameType(item.itemType);
        }

        private void EquipNextItemOfSameType(ItemType itemType)
        {
            var nextItem = playerItems.Find(x => (x.itemType == itemType) && (x.IsEquiped));
            if(nextItem != null)
            {
                EquipItem(nextItem);
            }
        }

        private void RemovePlayerSprite(Item item)
        {
            playerGraphics.Find(item.itemType.ToString()).GetComponent<SpriteRenderer>().sprite = null;
        }

        private void EquipItem(Item item)
        {
            item.IsEquiped = true;
            ChangePlayerSprite(item);
        }

        private void ChangePlayerSprite(Item item)
        {
            playerGraphics.Find(item.itemType.ToString()).GetComponent<SpriteRenderer>().sprite = item.onPlayerSprite;
        }
    }
}
