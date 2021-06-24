using ECCLibrary;
using System.Collections;
using UnityEngine;

namespace RotA.Mono.Creatures.GargEssentials
{
    public class GargantuanSwimAmbience : MonoBehaviour
    {
        public string swimSoundPrefix = "garg_swim_loop_2";
        public int audioSourceCount = 2;
        public float delay = 26.742f;

        ECCAudio.AudioClipPool clipPool;
        AudioSource[] myAudioSources;

        void Awake()
        {
            clipPool = ECCAudio.CreateClipPool(swimSoundPrefix);
            myAudioSources = new AudioSource[audioSourceCount];
            for (int i = 0; i < audioSourceCount; i++)
            {
                myAudioSources[i] = AddSource();
            }

        }

        AudioSource AddSource()
        {
            AudioSource source;
            source = gameObject.AddComponent<AudioSource>();
            source.volume = ECCHelpers.GetECCVolume();
            source.minDistance = 30f;
            source.maxDistance = 60f;
            source.spatialBlend = 1f;
            return source;
        }

        AudioSource GetAvailableSource()
        {
            for (int i = 0; i < audioSourceCount; i++)
            {
                if (!myAudioSources[i].isPlaying)
                {
                    return myAudioSources[i];
                }
            }
            return null;
        }

        IEnumerator Start()
        {
            for (; ; )
            {
                AudioSource nextSource = GetAvailableSource();
                if (nextSource)
                {
                    nextSource.clip = clipPool.GetRandomClip();
                    nextSource.Play();
                }
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
