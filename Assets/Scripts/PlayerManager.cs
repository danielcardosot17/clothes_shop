using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CalangoGames
{
    public class PlayerManager : MonoBehaviour
    {

        [SerializeField] private AudioManager audioManager;
        [SerializeField][Range(0.1f, 10f)] private float moveSpeed = 5;
        [SerializeField][Range(0.1f, 5f)] private float actionRadius = 2;
        [SerializeField] private ParticleSystem interactionParticleSystem;
        
        private InputActions inputActions;
        private InputAction move;
        private InputAction interact;
        private InputAction touch;
        private InputAction touchPosition;
        private Animator animator;
        private bool isTouching = false;
        private bool isWalking = false;
        private Camera mainCamera;
    
        private void Awake() {
            mainCamera = Camera.main;
            animator = GetComponent<Animator>();
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
            
            PlaySound("Whistle");
            PlayWhistleVFX();
            var colliders = Physics2D.OverlapCircleAll(transform.position, actionRadius);
            foreach(var collider in colliders)
            {
                var interactable = collider.GetComponent<IInteractable>();
                if(interactable != null)
                {
                    interactable.Interact();
                    return; // will get only first interactable
                }
            }

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

        private void Update() {
            MoveKeyboard();
            MoveTouch();
        }

        private void MoveKeyboard()
        {
            var direction = move.ReadValue<Vector2>();
            Move(direction);
        }

        private void Move(Vector2 direction)
        {
            if(direction.magnitude > 0)
            {
                transform.position += new Vector3(direction.x,direction.y,transform.position.z) * moveSpeed * Time.deltaTime;
                if(!isWalking)
                {
                    isWalking = true;
                    animator.SetBool("isWalking",isWalking);
                }
            }
            else{
                if(isWalking)
                {
                    isWalking = false;
                    animator.SetBool("isWalking",isWalking);
                }
            }
        }

        private void MoveTouch()
        {
            if(isTouching)
            {
                var position = touchPosition.ReadValue<Vector2>();
                var touchWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 0));
                var screenCenterPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, 0));
                var direction = (new Vector2(touchWorldPoint.x, touchWorldPoint.y) - new Vector2(screenCenterPoint.x, screenCenterPoint.y)).normalized;

                Move(direction);
            }
        }

        void OnDrawGizmosSelected()
        {
            // Display the explosion radius when selected
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, actionRadius);
        }

        private void PlaySound(string soundName)
        {
            audioManager.PlaySFX(soundName, transform.position);
        }

        public void PlayFootstepSound()
        {
            PlaySound("Footstep");
        }
        private void PlayWhistleVFX()
        {
            Instantiate(interactionParticleSystem, transform.position, Quaternion.identity);
        }
    }
}
