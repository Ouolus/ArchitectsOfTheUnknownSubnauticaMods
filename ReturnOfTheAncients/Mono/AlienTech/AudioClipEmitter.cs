using ECCLibrary;
using System.Collections;
using UnityEngine;

namespace RotA.Mono.AlienTech
{
    public class AudioClipEmitter : MonoBehaviour
    {
        public string clipPoolPrefix;
        public float delayMin = 1f;
        public float delayMax = 2f;
        public float minDistance = 10f;
        public float maxDistance = 50f;

        ECCAudio.AudioClipPool clipPool;
        AudioSource audioSource;

        public void Start()
        {
            clipPool = ECCAudio.CreateClipPool(clipPoolPrefix);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
            audioSource.volume = ECCHelpers.GetECCVolume();
            audioSource.maxDistance = maxDistance;
            StartCoroutine(Play());
        }

        IEnumerator Play()
        {
            for (; ; )
            {
                AudioClip nextClip = clipPool.GetRandomClip();
                audioSource.clip = nextClip;
                audioSource.Play();
                yield return new WaitForSeconds(nextClip.length + Random.Range(delayMin, delayMax));

            }
        }
    }
}
