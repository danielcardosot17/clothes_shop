using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace CalangoGames
{
    public class SceneLayoutTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ThereIsOnlyOnePlayerInScene()
        {
            List<PlayerManager> playersInScene = new List<PlayerManager>();

            foreach (PlayerManager player in Resources.FindObjectsOfTypeAll(typeof(PlayerManager)) as PlayerManager[])
            {
                if (!EditorUtility.IsPersistent(player.transform.root.gameObject) && !(player.hideFlags == HideFlags.NotEditable || player.hideFlags == HideFlags.HideAndDontSave))
                    playersInScene.Add(player);
            }
            
            Assert.AreEqual(expected: 1, actual: playersInScene.Count);
        }
        
    }
}
