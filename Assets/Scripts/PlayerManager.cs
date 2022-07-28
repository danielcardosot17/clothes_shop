using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CalangoGames
{
    public class PlayerManager : MonoBehaviour
    {

        [SerializeField][Range(0.1f, 10f)] private float moveSpeed = 5;
        [SerializeField][Range(0.1f, 5f)] private float actionRadious = 2;
        [SerializeField] private ParticleSystem actionParticleSystem;
        
        private InputActions inputActions;
        private InputAction move;
        private InputAction interact;
        private InputAction touch;
        private InputAction touchPosition;
        private Animator animator;
        private bool isTouching = false;
        private bool isWalking = false;
    
        private void Awake() {
            inputActions = new InputActions();
            move = inputActions.Player.MoveKeyboard;
            interact = inputActions.Player.Interact;
            touch = inputActions.Player.Touch;
            touchPosition = inputActions.Player.TouchPosition;
        }

        private void OnEnable() {
            interact.performed += OnInteract;
            touch.performed += OnTouchStart;
            touch.canceled += OnTouchEnd;
            inputActions.Player.Enable();
        }

        private void OnInteract(InputAction.CallbackContext obj)
        {
            Debug.Log("OnInteract");
        }

        private void OnDisable() {
            interact.performed -= OnInteract;
            touch.performed -= OnTouchStart;
            touch.canceled -= OnTouchEnd;
            inputActions.Player.Disable();
        }

        private void OnTouchStart(InputAction.CallbackContext obj)
        {
            isTouching = true;
            Debug.Log("OnTouchStart");
        }

        private void OnTouchEnd(InputAction.CallbackContext obj)
        {
            isTouching = false;
            Debug.Log("OnTouchEnd");
        }


    }
}
