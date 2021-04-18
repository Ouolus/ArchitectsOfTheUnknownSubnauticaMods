using ECCLibrary;
using ECCLibrary.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UWE;

namespace ProjectAncients.Mono
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
