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

        [Test]
        public void ThereIsOnlyOneAudioManagerInScene()
        {
            List<AudioManager> audioManagersInScene = new List<AudioManager>();

            foreach (AudioManager audioManager in Resources.FindObjectsOfTypeAll(typeof(AudioManager)) as AudioManager[])
            {
                if (!EditorUtility.IsPersistent(audioManager.transform.root.gameObject) && !(audioManager.hideFlags == HideFlags.NotEditable || audioManager.hideFlags == HideFlags.HideAndDontSave))
                    audioManagersInScene.Add(audioManager);
            }
            
            Assert.AreEqual(expected: 1, actual: audioManagersInScene.Count);
        }

        [Test]
        public void ThereIsOnlyOneGameMasterInScene()
        {
            List<GameMaster> gameMastersInScene = new List<GameMaster>();

            foreach (GameMaster gameMaster in Resources.FindObjectsOfTypeAll(typeof(GameMaster)) as GameMaster[])
            {
                if (!EditorUtility.IsPersistent(gameMaster.transform.root.gameObject) && !(gameMaster.hideFlags == HideFlags.NotEditable || gameMaster.hideFlags == HideFlags.HideAndDontSave))
                    gameMastersInScene.Add(gameMaster);
            }
            
            Assert.AreEqual(expected: 1, actual: gameMastersInScene.Count);
        }
    }
}
