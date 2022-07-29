using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class AudioManager : MonoBehaviour
    {
        public List<Sound> sounds;
        public void Play(string soundName, Vector3 position)
        {
            if(sounds.Count == 0) return;
            var sound = sounds.Find(sound => sound.name == soundName);
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
