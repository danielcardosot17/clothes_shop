using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class GameMaster : MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager;
        // Start is called before the first frame update
        void Start()
        {
            audioManager.PlayMusic("Music1");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
