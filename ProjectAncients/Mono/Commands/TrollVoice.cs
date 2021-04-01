using ECCLibrary;
using UnityEngine;

namespace ProjectAncients.Mono.Commands
{
    public class TrollVoice : MonoBehaviour
    {
        AudioSource clipSource;
        ECCAudio.AudioClipPool clipPool;

        void Start()
        {
            clipPool = ECCAudio.CreateClipPool("Troll");
            clipSource.volume = ECCHelpers.GetECCVolume();
        }

        void Update()
        {
            if (!clipSource.isPlaying)
            {
                clipSource.clip = clipPool.GetRandomClip();
                clipSource.Play();
            }
        }
    }
}
