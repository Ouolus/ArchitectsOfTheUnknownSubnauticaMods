using ECCLibrary;
using UnityEngine;

namespace RotA.Mono
{
    public class StoryHandTargetPlayAudioClip : MonoBehaviour
    {
        public string clipPrefix;
        public string subtitlesKey;

        public void OnStoryHandTarget()
        {
            if (string.IsNullOrEmpty(clipPrefix))
            {
                return;
            }
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = ECCAudio.CreateClipPool(clipPrefix).GetRandomClip();
            audioSource.volume = ECCHelpers.GetECCVolume() * 0.75f; //arbitrary volume scale
            audioSource.Play();
            if (!string.IsNullOrEmpty(subtitlesKey))
            {
                Subtitles.main.Add(subtitlesKey);
            }
            Destroy(this);
        }
    }
}
