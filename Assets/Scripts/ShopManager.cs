using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CalangoGames
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private Canvas shopCanvas;
        [SerializeField] private GameObject sellMenu;
        [SerializeField] private GameObject buyMenu;
        [SerializeField] private Transform buyItemBtnList;
        [SerializeField] private Transform sellItemBtnList;
        [SerializeField] private Transform itemButtonTemplate;
        [SerializeField] private Animator shopAnimator;
        [SerializeField] private Camera zoomCamera;
        [SerializeField] private TMP_Text shopNameText;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button sellButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private RectTransform buyScrollRect;
        [SerializeField] private RectTransform sellScrollRect;
        [SerializeField] private RectTransform buyContentRect;
        [SerializeField] private RectTransform sellContentRect;
        private Button lastBuyButton;
        private Button lastSellButton;
        private AudioManager audioManager;
        private PlayerManager playerManager;
        private MoneyManager moneyManager;
        private InventoryManager inventoryManager;
        private EventSystem eventSystem;
        private bool isOpen = false;
        private RectTransform oldRect;

        private void Awake() {
            moneyManager = FindObjectOfType<MoneyManager>();
            playerManager = FindObjectOfType<PlayerManager>();
            inventoryManager = FindObjectOfType<InventoryManager>();
            audioManager = FindObjectOfType<AudioManager>();
            eventSystem = FindObjectOfType<EventSystem>();
        }

        private void Start() {
            DisableEventSystem();
        }

        public void HideShop()
        {
            DisableEventSystem();
            DisableZoomCamera();
            isOpen = false;
            shopAnimator.SetBool("isOpen", isOpen);
            audioManager.PlaySFX("CloseShop");
            playerManager.EnablePlayerMoveInput();
        }

        private void DisableEventSystem()
        {
            eventSystem.enabled = false;
        }

        private void EnableEventSystem()
        {
            eventSystem.enabled = true;
        }

        public void OpenShop(Shopkeeper shopkeeper, bool shopHasSellOption, List<Item> itemsPlayerCanBuy)
        {
            EnableZoomCamera();
            SetShopName(shopkeeper.ShopName);
            ClearItemButtonList(buyItemBtnList);
            lastBuyButton = buyButton;
            PopulateItemList(shopkeeper, itemsPlayerCanBuy);
            if(shopHasSellOption){
                EnableSellMenu();
                ClearItemButtonList(sellItemBtnList);
                lastSellButton = sellButton;
                PopulateItemList(shopkeeper, inventoryManager.PlayerItems, false);
            }
            else
            {
                DisableSellMenu();
            }
            ShowShop();
        }

        private void PopulateItemList(Shopkeeper shopkeeper, List<Item> items, bool isBuy = true)
        {
            foreach(var item in items)
            {
                AddItemButton(shopkeeper, item, isBuy);
            }
        }

        public void AddItemButton(Shopkeeper shopkeeper, Item item, bool isBuy = true)
        {
            if(isBuy)
            {
                var newButtonTransform = Instantiate(itemButtonTemplate, buyItemBtnList);
                SetupItemButton(newButtonTransform, item.icon, item.itemName, item.buyPrice);
                var eventHandler = newButtonTransform.GetComponent<ItemButtonEventHandler>();
                SetUpButtonEventHandler(eventHandler, buyScrollRect, buyContentRect);

                var newButton = newButtonTransform.GetComponent<Button>();
                var currentButtonNavigation = lastBuyButton.GetComponent<ButtonNavigationManager>();
                var newButtonNavigation = newButtonTransform.GetComponent<ButtonNavigationManager>();

                newButton.onClick.AddListener(() => {
                    if(moneyManager.TryBuyItem(item, shopkeeper, inventoryManager))
                    {
                        // newButtonNavigation.RemoveFromNavigation();
                        newButtonNavigation.AdjustPreviousButtonNavigation(newButtonNavigation.PreviousBtn);
                        if(lastBuyButton == newButton)
                        {
                            lastBuyButton = newButtonNavigation.PreviousBtn;
                        }
                        else
                        {
                            newButtonNavigation.AdjustNextButtonNavigation(newButtonNavigation.NextBtn);
                        }
                        GameObject.Destroy(newButtonTransform.gameObject);
                        newButtonNavigation.SelectNextButton();
                        audioManager.PlaySFX("BuyItemSFX");
                    }
                    else{
                        audioManager.PlaySFX("NotEnoughMoneySFX");
                    }
                });
                currentButtonNavigation.SetNextDownButton(newButton);
                
                newButtonNavigation.SetPreviousUpButton(lastBuyButton);
                newButtonNavigation.SetNextDownButton(exitButton);
                newButtonNavigation.SetLeftRightButton(buyButton, sellButton);

                lastBuyButton = newButton;
            }
            else
            {
                var newButtonTransform = Instantiate(itemButtonTemplate, sellItemBtnList);
                SetupItemButton(newButtonTransform, item.icon, item.itemName, item.sellPrice);
                var eventHandler = newButtonTransform.GetComponent<ItemButtonEventHandler>();
                SetUpButtonEventHandler(eventHandler, sellScrollRect, sellContentRect);

                var newButton = newButtonTransform.GetComponent<Button>();
                var currentButtonNavigation = lastSellButton.GetComponent<ButtonNavigationManager>();
                var newButtonNavigation = newButtonTransform.GetComponent<ButtonNavigationManager>();

                newButton.onClick.AddListener(() => {
                    moneyManager.SellItem(item, shopkeeper, inventoryManager);
                    newButtonNavigation.AdjustPreviousButtonNavigation(newButtonNavigation.PreviousBtn);
                    if(lastSellButton == newButton)
                    {
                        lastSellButton = newButtonNavigation.PreviousBtn;
                    }
                    else
                    {
                        newButtonNavigation.AdjustNextButtonNavigation(newButtonNavigation.NextBtn);
                    }
                    GameObject.Destroy(newButtonTransform.gameObject);
                    newButtonNavigation.SelectNextButton();
                    audioManager.PlaySFX("SellItemSFX");
                });
                currentButtonNavigation.SetNextDownButton(newButton);
                
                newButtonNavigation.SetPreviousUpButton(lastSellButton);
                newButtonNavigation.SetNextDownButton(exitButton);
                newButtonNavigation.SetLeftRightButton(buyButton, sellButton);
                
                lastSellButton = newButton;
            }
        }

        private void SetUpButtonEventHandler(ItemButtonEventHandler eventHandler, RectTransform scrollRect, RectTransform contentRect)
        {
            eventHandler.ScrollRect = scrollRect;
            eventHandler.ContentRect = contentRect;
        }

        public void SnapScrollToView(RectTransform currentRect, RectTransform scrollRect, RectTransform contentRect)
        {
            Vector2 v = currentRect.position;
            bool inView = RectTransformUtility.RectangleContainsScreenPoint(scrollRect, v);
            var incrimentSize = currentRect.rect.height + contentRect.GetComponent<VerticalLayoutGroup>().spacing/2;
            if(!inView)
            {
                if(oldRect != null)
                {
                    if (oldRect.localPosition.y < currentRect.localPosition.y)
                    {
                        contentRect.anchoredPosition += new Vector2(0, -incrimentSize);
                    }
                    else if (oldRect.localPosition.y > currentRect.localPosition.y)
                    {
                        contentRect.anchoredPosition += new Vector2(0, incrimentSize);
                    }
                }
            }
            oldRect = currentRect;
        }

        public void ResetBuyContentPosition()
        {
            buyContentRect.anchoredPosition = Vector2.zero;
        }
        public void ResetSellContentPosition()
        {
            sellContentRect.anchoredPosition = Vector2.zero;
        }

        private void SetupItemButton(Transform newButton, Sprite icon, string itemName, int price)
        {
            SetIcon(newButton.Find("Icon"), icon);
            SetName(newButton.Find("Name"), itemName);
            SetPrice(newButton.Find("Price"), price);
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
            EnableEventSystem();
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

        public void PlayUIButtonSelectSFX()
        {
            audioManager.PlaySFX("UIButtonSelect");
        }

        public void BringSellMenuForward()
        {
            sellMenu.transform.SetAsLastSibling();
        }
        public void BringBuyMenuForward()
        {
            buyMenu.transform.SetAsLastSibling();
        }

    }
}
