using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    [System.Serializable]
    public class Item
    {
        public string itemName;
        public int buyPrice;
        public int sellPrice;
        public Sprite onPlayerSprite;
        public Sprite icon;

        public Item(string itemName, int buyPrice = 10, int sellPrice = 0, Sprite onPlayerSprite = null, Sprite icon = null)
        {
            this.itemName = itemName;
            this.buyPrice = buyPrice;
            this.sellPrice = sellPrice;
            this.onPlayerSprite = onPlayerSprite;
            this.icon = icon;
        }
    }
}
