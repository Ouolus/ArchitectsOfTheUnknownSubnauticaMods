﻿using UnityEngine;
using System.Collections;
using ECCLibrary;

namespace ProjectAncients.Mono
{
    public class GargantuanRoar : MonoBehaviour
    {
        AudioSource audioSource;
        ECCAudio.AudioClipPool closeSounds;
        ECCAudio.AudioClipPool farSounds;
        Transform currentSpawn;
        Creature creature;
        const float delayMin = 11f;
        const float delayMax = 18f;

        private IEnumerator Start()
        {
            InitializeAudioSource();
            creature = GetComponent<Creature>();
            currentSpawn = gameObject.SearchChild("CurrentSpawn").transform;
            float distance;
            AudioClip clipToPlay;
            for(; ; )
            {
                if (!gameObject.GetComponent<LiveMixin>().IsAlive())
                {
                    Destroy(this);
                    yield break;
                }
                distance = Vector3.Distance(MainCameraControl.main.transform.position, transform.position);
                clipToPlay = GetAudioClip(distance);
                audioSource.clip = clipToPlay;
                audioSource.Play();
                creature.GetAnimator().SetFloat("random", Random.value);
                creature.GetAnimator().SetTrigger("roar");
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
            WorldForces.AddExplosion(currentSpawn.position, DayNightCycle.main.timePassed, 100f, 25f);
        }
    }
}
