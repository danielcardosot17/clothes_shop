using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class Shopkeeper : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<Item> itemsPlayerCanBuy;
        [SerializeField] private string shopName;
        [SerializeField] private bool shopHasSellOption = true;

        private ShopManager shopManager;

        public List<Item> ItemsPlayerCanBuy { get => itemsPlayerCanBuy; set => itemsPlayerCanBuy = value; }

        private void Awake() {
            shopManager = FindObjectOfType<ShopManager>();    
        }

        public void Interact()
        {
            shopManager.OpenShop(shopName, shopHasSellOption, ItemsPlayerCanBuy);
        }
    }
}
