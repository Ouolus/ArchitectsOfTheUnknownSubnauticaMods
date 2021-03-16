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

        public void OnStoryHandTarget()
        {
            if (string.IsNullOrEmpty(clipPrefix))
            {
                return;
            }
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = ECCAudio.CreateClipPool(clipPrefix).GetRandomClip();
            audioSource.volume = ECCHelpers.GetECCVolume();
            audioSource.Play();
            Destroy(this);
        }
    }
}
