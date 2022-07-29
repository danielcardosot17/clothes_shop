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
    }
}
