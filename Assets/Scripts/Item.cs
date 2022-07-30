using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    [System.Serializable]
    public class Item
    {
        public string itemName;
        public int price;
        public Sprite onPlayerSprite;
        public Sprite icon;

        public Item(string itemName, int price, Sprite onPlayerSprite = null, Sprite icon = null)
        {
            this.itemName = itemName;
            this.price = price;
            this.onPlayerSprite = onPlayerSprite;
            this.icon = icon;
        }
    }
}
