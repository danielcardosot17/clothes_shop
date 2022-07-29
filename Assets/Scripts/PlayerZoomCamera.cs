using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class PlayerZoomCamera : MonoBehaviour
    {
        private PlayerManager player;
        // Update is called once per frame

        private void Awake() {
            player = FindObjectOfType<PlayerManager>();
        }
        private void LateUpdate() {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
}
