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
        [SerializeField][Range(0, 300)] private int money = 100;

        public int Money { get => money; }

        public void SpendAmount(int amount)
        {
            money -= amount;
            if(money < 0 ) money = 0;
        }

        public void TryBuyItem(Item item)
        {
            if(money >= item.price)
            {
                SpendAmount(item.price);
            }
        }
    }
}
