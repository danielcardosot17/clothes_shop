using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CalangoGames
{
    public class ButtonNavigationManager : MonoBehaviour
    {
        private Button currentButton;
        private Button previousBtn;
        private Button nextBtn;
        private Button leftBtn;
        private Button rightBtn;

        public Button PreviousBtn { get => previousBtn; }
        public Button NextBtn { get => nextBtn; }

        private void Awake() {
            currentButton = gameObject.GetComponent<Button>();
        }

        public void SetPreviousUpButton(Button button)
        {
            Navigation navigation = currentButton.navigation;
            navigation.selectOnUp = button;
            currentButton.navigation = navigation;
            previousBtn = button;
        }

        public void SetNextDownButton(Button button)
        {
            Navigation navigation = currentButton.navigation;
            navigation.selectOnDown = button;
            currentButton.navigation = navigation;
            nextBtn = button;
        }

        public void SetLeftRightButton(Button buyButton, Button sellButton)
        {
            Navigation navigation = currentButton.navigation;
            navigation.selectOnLeft = buyButton;
            navigation.selectOnRight = sellButton;
            currentButton.navigation = navigation;
            leftBtn = buyButton;
            rightBtn = sellButton;
        }

        public void AdjustPreviousButtonNavigation(Button button)
        {
            var previousBtnNavigation = button.GetComponent<ButtonNavigationManager>();
            previousBtnNavigation.SetNextDownButton(nextBtn);
        }

        public void AdjustNextButtonNavigation(Button button)
        {
            var nextBtnNavigation = button.GetComponent<ButtonNavigationManager>();
            nextBtnNavigation.SetPreviousUpButton(previousBtn);
        }

        public void SelectNextButton()
        {
            nextBtn.Select();
        }
    }
}
