using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalangoGames
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private Canvas shopCanvas;
        [SerializeField] private GameObject sellMenu;
        [SerializeField] private Transform buyItemList;
        [SerializeField] private Transform sellItemList;
        [SerializeField] private Transform itemTemplate;
        [SerializeField] private Animator shopAnimator;
        [SerializeField] private Camera zoomCamera;
        [SerializeField] private TMP_Text shopNameText;
        private MoneyManager moneyManager;
        private InventoryManager inventoryManager;
        private bool isOpen = false;

        private void Awake() {
            moneyManager = FindObjectOfType<MoneyManager>();
            inventoryManager = FindObjectOfType<InventoryManager>();
        }
        private void Start() {
            HideShop();
        }

        private void HideShop()
        {
            DisableZoomCamera();
            isOpen = false;
            shopAnimator.SetBool("isOpen", isOpen);
            DisableShopCanvas();
        }


        public void OpenShop(string shopName, bool shopHasSellOption, List<Item> itemsPlayerCanBuy)
        {
            EnableZoomCamera();
            SetShopName(shopName);
            PopulateItemList(itemsPlayerCanBuy, buyItemList);
            if(shopHasSellOption){
                EnableSellMenu();
                PopulateItemList(inventoryManager.GetPlayerItemsList(), sellItemList);
            }
            else
            {
                DisableSellMenu();
            }
            ShowShop();
        }

        private void PopulateItemList(List<Item> itemsPlayerCanBuy, Transform buyItemList)
        {
            return;
        }

        private void EnableSellMenu()
        {
            sellMenu.SetActive(true);
        }
        private void DisableSellMenu()
        {
            sellMenu.SetActive(false);
        }

        private void ShowShop()
        {
            EnableShopCanvas();
            isOpen = true;
            shopAnimator.SetBool("isOpen", isOpen);
        }

        private void SetShopName(string shopName)
        {
            shopNameText.text = shopName;
        }

        private void EnableZoomCamera()
        {
            zoomCamera.enabled = true;
        }

        private void DisableZoomCamera()
        {
            zoomCamera.enabled = false;
        }

        private void DisableShopCanvas()
        {
            shopCanvas.enabled = false;
        }

        private void EnableShopCanvas()
        {
            shopCanvas.enabled = true;
        }

    }
}
