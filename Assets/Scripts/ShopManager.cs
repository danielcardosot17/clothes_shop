using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalangoGames
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private Canvas shopCanvas;
        [SerializeField] private GameObject sellMenu;
        [SerializeField] private Transform buyItemBtnList;
        [SerializeField] private Transform sellItemBtnList;
        [SerializeField] private Transform itemButtonTemplate;
        [SerializeField] private Animator shopAnimator;
        [SerializeField] private Camera zoomCamera;
        [SerializeField] private TMP_Text shopNameText;
        private AudioManager audioManager;
        private PlayerManager playerManager;
        private MoneyManager moneyManager;
        private InventoryManager inventoryManager;
        private bool isOpen = false;

        private void Awake() {
            moneyManager = FindObjectOfType<MoneyManager>();
            playerManager = FindObjectOfType<PlayerManager>();
            inventoryManager = FindObjectOfType<InventoryManager>();
            audioManager = FindObjectOfType<AudioManager>();
        }
        private void Start() {
            if(isOpen)
            {
                HideShop();
            }
        }

        private void HideShop()
        {
            DisableZoomCamera();
            isOpen = false;
            shopAnimator.SetBool("isOpen", isOpen);
            audioManager.PlaySFX("CloseShop");
            DisableShopCanvas();
            playerManager.EnablePlayerMoveInput();
        }


        public void OpenShop(string shopName, bool shopHasSellOption, List<Item> itemsPlayerCanBuy)
        {
            EnableZoomCamera();
            SetShopName(shopName);
            PopulateItemList(itemsPlayerCanBuy, buyItemBtnList);
            if(shopHasSellOption){
                EnableSellMenu();
                PopulateItemList(inventoryManager.GetPlayerItemsList(), sellItemBtnList);
            }
            else
            {
                DisableSellMenu();
            }
            ShowShop();
        }

        private void PopulateItemList(List<Item> items, Transform buttonList)
        {
            ClearItemButtonList(buttonList);
            foreach(var item in items)
            {
                var newButton = Instantiate(itemButtonTemplate, buttonList);
                SetupItemButton(newButton, item);
            }
        }

        private void SetupItemButton(Transform newButton, Item item)
        {
            SetIcon(newButton.Find("Icon"), item.icon);
            SetName(newButton.Find("Name"), item.itemName);
            SetPrice(newButton.Find("Price"), item.price);
        }

        private void SetPrice(Transform priceTransform, int price)
        {
            var priceText = priceTransform.GetComponent<TMP_Text>();
            priceText.text = price.ToString();
        }

        private void SetName(Transform nameTransform, string itemName)
        {
            var nameText = nameTransform.GetComponent<TMP_Text>();
            nameText.text = itemName;
        }

        private void SetIcon(Transform iconTransform, Sprite icon)
        {
            var image = iconTransform.GetComponent<Image>();
            image.sprite = icon;
        }

        private void ClearItemButtonList(Transform buttonList)
        {
            foreach(Transform child in buttonList)
            {
                GameObject.Destroy(child.gameObject);
            }
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
            playerManager.DisablePlayerMoveInput();
            EnableShopCanvas();
            audioManager.PlaySFX("OpenShop");
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
