using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using ECCLibrary;

namespace ProjectAncients.Mono
{
    public class GargantuanRoar : MonoBehaviour
    {
        AudioSource audioSource;
        ECCAudio.AudioClipPool closeSounds;
        ECCAudio.AudioClipPool farSounds;
        const float delayMin = 10f;
        const float delayMax = 25f;

        IEnumerator Start()
        {
            InitializeAudioSource();
            float distance;
            AudioClip clipToPlay;
            for(; ; )
            {
                distance = Vector3.Distance(MainCameraControl.main.transform.position, transform.position);
                if(distance < 150f)
                {
                    clipToPlay = closeSounds.GetRandomClip();
                }
                else
                {
                    clipToPlay = farSounds.GetRandomClip();
                }
                audioSource.clip = clipToPlay;
                audioSource.Play();
                float timeToWait = clipToPlay.length + Random.Range(delayMin, delayMax);
                yield return new WaitForSeconds(timeToWait);
            }
        }

        void InitializeAudioSource()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = ECCHelpers.GetECCVolume();
            audioSource.spatialBlend = 1f;
            audioSource.minDistance = 50f;
            audioSource.maxDistance = 600f;

            closeSounds = ECCAudio.CreateClipPool("garg_roar");
            farSounds = ECCAudio.CreateClipPool("garg_for_anth_distant");
        }
    }
}
