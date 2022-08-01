using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class DoorBell : MonoBehaviour
    {
        private AudioManager audioManager;

        private void Awake() {
            audioManager = FindObjectOfType<AudioManager>();
        }
        private void OnTriggerEnter2D(Collider2D other) {
            audioManager.PlaySFX("DoorBellSFX");
        }
    }
}
