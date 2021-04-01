using ECCLibrary;
using UnityEngine;

namespace ProjectAncients.Mono.Commands
{
    public class TrollVoice : MonoBehaviour
    {
        AudioSource audioSource;
        ECCAudio.AudioClipPool clipPool;

        void Start()
        {
            clipPool = ECCAudio.CreateClipPool("Troll");
            audioSource = gameObject.EnsureComponent<AudioSource>();
            audioSource.volume = ECCHelpers.GetECCVolume();
        }

        void Update()
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clipPool.GetRandomClip();
                audioSource.Play();
            }
        }
    }
}
