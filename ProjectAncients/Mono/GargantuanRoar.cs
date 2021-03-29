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
        Creature creature;
        public float delayMin = 11f;
        public float delayMax = 18f;
        public string closeSoundsPrefix;
        public string distantSoundsPrefix;

        public float minDistance = 50f;
        public float maxDistance = 600f;

        float timeRoarAgain = 0f;

        private void Start()
        {
            InitializeAudioSource();
            creature = GetComponent<Creature>();
        }

        void Update()
        {
            if (!creature.liveMixin.IsAlive())
            {
                Destroy(this);
                return;
            }
            if(Time.time > timeRoarAgain)
            {
                float distance = Vector3.Distance(MainCameraControl.main.transform.position, transform.position);
                AudioClip clipToPlay = GetAudioClip(distance);
                audioSource.clip = clipToPlay;
                audioSource.Play();
                creature.GetAnimator().SetFloat("random", Random.value);
                creature.GetAnimator().SetTrigger("roar");
                float timeToWait = clipToPlay.length + Random.Range(delayMin, delayMax);
                timeRoarAgain = Time.time + timeToWait;
            }
        }

        public void PlayOnce()
        {
            float distance = Vector3.Distance(MainCameraControl.main.transform.position, transform.position);
            AudioClip clipToPlay = GetAudioClip(distance);
            audioSource.clip = clipToPlay;
            audioSource.Play();
            creature.GetAnimator().SetFloat("random", Random.value);
            creature.GetAnimator().SetTrigger("roar");
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
            audioSource.minDistance = minDistance;
            audioSource.maxDistance = maxDistance;

            closeSounds = ECCAudio.CreateClipPool(closeSoundsPrefix);
            farSounds = ECCAudio.CreateClipPool(distantSoundsPrefix);
        }
    }
}
