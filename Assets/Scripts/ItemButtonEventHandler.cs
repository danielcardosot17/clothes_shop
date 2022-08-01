using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CalangoGames
{
    public class ItemButtonEventHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        private ShopManager shopManager;
        private RectTransform buttonRect;
        private RectTransform scrollRect;
        private RectTransform contentRect;

        public RectTransform ScrollRect { get => scrollRect; set => scrollRect = value; }
        public RectTransform ContentRect { get => contentRect; set => contentRect = value; }

        private void Awake() {
            shopManager = FindObjectOfType<ShopManager>();
            buttonRect = GetComponent<RectTransform>();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            shopManager.PlayUIButtonSelectSFX();
        }

        public void OnSelect(BaseEventData eventData)
        {
            shopManager.SnapScrollToView(buttonRect, scrollRect, contentRect);
        }
    }
}
