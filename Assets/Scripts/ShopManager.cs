using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private Canvas shopCanvas;
        private MoneyManager moneyManager;

        private void Awake() {
            moneyManager = FindObjectOfType<MoneyManager>();
        }
    }
}
