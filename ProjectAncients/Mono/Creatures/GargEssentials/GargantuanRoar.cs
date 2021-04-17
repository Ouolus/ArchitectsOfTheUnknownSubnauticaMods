using UnityEngine;
using System.Collections;
using ECCLibrary;

namespace ProjectAncients.Mono
{
    public class GargantuanRoar : MonoBehaviour
    {
        public AudioSource audioSource;
        ECCAudio.AudioClipPool closeSounds;
        ECCAudio.AudioClipPool farSounds;
        Creature creature;
        public float delayMin = 11f;
        public float delayMax = 18f;
        public string closeSoundsPrefix;
        public string distantSoundsPrefix;

        public float minDistance = 50f;
        public float maxDistance = 600f;
        public bool screenShake;

        float timeRoarAgain = 0f;
        float timeUpdateShakeAgain = 0f;

        private float[] clipSampleData = new float[1024];
        private float clipLoudness = 0f;

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
                PlayOnce(out float roarLength);
                float timeToWait = roarLength + Random.Range(delayMin, delayMax);
                timeRoarAgain = Time.time + timeToWait;
            }
            if (screenShake)
            {
                if (Time.time > timeUpdateShakeAgain && audioSource.isPlaying)
                {
                    audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
                    clipLoudness = 0f;
                    foreach (var sample in clipSampleData)
                    {
                        clipLoudness += (Mathf.Abs(sample) * Mod.config.RoarScreenShakeIntensity);
                    }
                    if (clipLoudness > 0.8f)
                    {
                        MainCameraControl.main.ShakeCamera(clipLoudness / 50f, 1f, MainCameraControl.ShakeMode.Linear, 1f);
                    }
                    timeUpdateShakeAgain = Time.time + 0.5f;
                }
            }
        }

        public void PlayOnce(out float roarLength)
        {
            float distance = Vector3.Distance(MainCameraControl.main.transform.position, transform.position);
            AudioClip clipToPlay = GetAudioClip(distance);
            roarLength = clipToPlay.length;
            audioSource.clip = clipToPlay;
            audioSource.Play();
            creature.GetAnimator().SetFloat("random", Random.value);
            creature.GetAnimator().SetTrigger("roar");
        }

        private AudioClip GetAudioClip(float distance)
        {
            if (distance < 150f && GargantuanBehaviour.PlayerIsKillable())
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
            audioSource.playOnAwake = false;

            closeSounds = ECCAudio.CreateClipPool(closeSoundsPrefix);
            farSounds = ECCAudio.CreateClipPool(distantSoundsPrefix);
        }
    }
}
