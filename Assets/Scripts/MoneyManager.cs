using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalangoGames
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;
        [SerializeField][Range(0, 1000)] private int money = 100;

        public int Money { get => money; }

        private void Start() {
            DisplayMoney();
        }

        private void DisplayMoney()
        {
            moneyText.text = money.ToString();
        }

        public void SpendAmount(int amount)
        {
            money -= amount;
            if(money < 0 ) money = 0;
            DisplayMoney();
        }

        private void EarnAmount(int amount)
        {
            money += amount;
            DisplayMoney();
        }

        public void TryBuyItem(Item item, Shopkeeper shopkeeper, InventoryManager inventory)
        {
            if(money >= item.buyPrice)
            {
                SpendAmount(item.buyPrice);
                shopkeeper.ItemsPlayerCanBuy.Remove(item);
                inventory.AddItemToInventory(item);
                Debug.Log("You bought the " + item.itemName + "!!");
            }
        }

        public void SellItem(Item item, Shopkeeper shopkeeper, InventoryManager inventory)
        {
            EarnAmount(item.sellPrice);
            inventory.RemoveItemFromInventory(item.itemName);
            shopkeeper.ItemsPlayerCanBuy.Add(item);
            Debug.Log("You sold the " + item.itemName + "!!");
        }
    }
}
