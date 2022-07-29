using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class Shopkeeper : MonoBehaviour, IInteractable
    {
        private ShopManager shopManager;

        private void Awake() {
            shopManager = FindObjectOfType<ShopManager>();    
        }

        public void Interact()
        {
            Debug.Log("AAAAAAAAAAA");
        }
    }
}
