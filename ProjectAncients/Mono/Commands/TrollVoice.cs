using ECCLibrary;
using UnityEngine;

namespace ProjectAncients.Mono.Commands
{
    public class TrollVoice : MonoBehaviour
    {
        AudioSource audioSource;
        ECCAudio.AudioClipPool clipPool;
        float timePlayAgain = 0f;

        void Start()
        {
            clipPool = ECCAudio.CreateClipPool("Troll");
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = ECCHelpers.GetECCVolume();
        }

        void Update()
        {
            if(Time.time > timePlayAgain)
            {
                AudioClip nextClip = clipPool.GetRandomClip();
                PlayAudio(nextClip);
                timePlayAgain = Time.time + 2f + nextClip.length;
            }
            if (!audioSource.isPlaying)
            {
                Invoke(nameof(PlayAudio), 2f);
            }
        }

        void PlayAudio(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
