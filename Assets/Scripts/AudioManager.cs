using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private List<Sound> soundEffcts;
        [SerializeField] private List<Sound> musics;
        private AudioSource audioSource;


        private void Awake() {
            audioSource = GetComponent<AudioSource>();
            StopMusic();
        }

        public void StopMusic()
        {
            audioSource.Stop();
        }

        public void PlayMusic(string musicName)
        {
            if(musics.Count == 0) return;
            var sound = musics.Find(sound => sound.name == musicName);
            audioSource.clip = sound.clip;
            audioSource.priority = sound.priority;
            audioSource.volume = sound.volume;
            audioSource.pitch = sound.pitch;
            audioSource.loop = sound.loop;
            audioSource.Play();
        }

        public void PlaySFX(string soundName, Vector3 position = new Vector3())
        {
            if(soundEffcts.Count == 0) return;
            var sound = soundEffcts.Find(sound => sound.name == soundName);
            var obj = new GameObject(name: soundName, typeof(AudioSource));
            obj.transform.position = position;
            var source = obj.GetComponent<AudioSource>();

            source.clip = sound.clip;
            source.priority = sound.priority;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;
            source.Play();
            if(!source.loop)
            {
                Destroy(source.gameObject, source.clip.length/source.pitch);
            }
        }
    }
}
