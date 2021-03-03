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

        private IEnumerator Start()
        {
            InitializeAudioSource();
            float distance;
            AudioClip clipToPlay;
            for(; ; )
            {
                distance = Vector3.Distance(MainCameraControl.main.transform.position, transform.position);
                clipToPlay = GetAudioClip(distance);
                audioSource.clip = clipToPlay;
                audioSource.Play();
                DoWaterDisplacement();
                float timeToWait = clipToPlay.length + Random.Range(delayMin, delayMax);
                yield return new WaitForSeconds(timeToWait);
            }
        }

        private AudioClip GetAudioClip(float distance)
        {
            if (distance < 150f)
            {
                return closeSounds.GetRandomClip();
            }
            else
            {
                return farSounds.GetRandomClip();
            }
        }

        private void InitializeAudioSource()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = ECCHelpers.GetECCVolume();
            audioSource.spatialBlend = 1f;
            audioSource.minDistance = 50f;
            audioSource.maxDistance = 600f;

            closeSounds = ECCAudio.CreateClipPool("garg_roar");
            farSounds = ECCAudio.CreateClipPool("garg_for_anth_distant");
        }

        private void DoWaterDisplacement()
        {
            WorldForces.AddExplosion(transform.position, DayNightCycle.main.timePassed, 100f, 25f);
        }
    }
}
