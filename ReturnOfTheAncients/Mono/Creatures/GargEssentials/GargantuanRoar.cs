namespace RotA.Mono.Creatures.GargEssentials
{
    using ECCLibrary;
    using UnityEngine;
    using static GargantuanConditions;
    
    class GargantuanRoar : MonoBehaviour
    {
        public AudioSource audioSource;
        public GargantuanBehaviour gargantuanBehaviour;
        ECCAudio.AudioClipPool closeSounds;
        ECCAudio.AudioClipPool farSounds;
        Creature creature;
        public float delayMin = 11f;
        public float delayMax = 18f;
        public string closeSoundsPrefix;
        public string distantSoundsPrefix;
        public float closeRoarThreshold = 150f;

        public float minDistance = 50f;
        public float maxDistance = 600f;
        public bool screenShake;
        public bool roarDoesDamage;

        float _timeRoarAgain = 0f;
        float _timeUpdateShakeAgain = 0f;

        private readonly float[] _clipSampleData = new float[1024];
        private float _clipLoudness = 0f;

        private const float kMAXDamageDistance = 200f;
        private const float kRoarMaxDamagePerSecond = 6f;
        private float timeStopDamaging = 0f;
        private bool _hasRoaredOnce;

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

            if (_hasRoaredOnce && gargantuanBehaviour.IsInStealthMode())
            {
                return;
            }
            if (Time.time > _timeRoarAgain)
            {
                PlayOnce(out float roarLength, RoarMode.Automatic);
            }
            if (screenShake)
            {
                if (Time.time > _timeUpdateShakeAgain && audioSource.isPlaying)
                {
                    if (PlayerIsKillable())
                    {
                        audioSource.clip.GetData(_clipSampleData, audioSource.timeSamples);
                        _clipLoudness = 0f;
                        foreach (var sample in _clipSampleData)
                        {
                            _clipLoudness += (Mathf.Abs(sample) * Mod.config.GetRoarScreenShakeNormalized);
                        }
                        if (_clipLoudness > 0.8f)
                        {
                            MainCameraControl.main.ShakeCamera(_clipLoudness / 50f, 1f, MainCameraControl.ShakeMode.Linear, 1f);
                        }
                        _timeUpdateShakeAgain = Time.time + 0.5f;
                    }
                }
            }
            if (roarDoesDamage && PlayerIsKillable() && Time.time < timeStopDamaging)
            {
                DoDamage();
            }
        }

        public void PlayOnce(out float roarLength, RoarMode roarMode)
        {
            float distance = Vector3.Distance(MainCameraControl.main.transform.position, transform.position);
            AudioClip clipToPlay = GetAudioClip(distance, roarMode);
            roarLength = clipToPlay.length;
            audioSource.clip = clipToPlay;
            audioSource.Play();
            creature.GetAnimator().SetFloat("random", Random.value);
            creature.GetAnimator().SetTrigger("roar");
            float timeToWait = roarLength + Random.Range(delayMin, delayMax);
            _timeRoarAgain = Time.time + timeToWait;
            timeStopDamaging = Time.time + 6f;
            _hasRoaredOnce = true;
        }

        void DoDamage()
        {
            return;
            float distance = Vector3.Distance(Player.main.transform.position, transform.position);
            if (distance < kMAXDamageDistance)
            {
                float distanceScalar = Mathf.Clamp(1f - (distance / kMAXDamageDistance), 0.01f, 1f);
                Player.main.liveMixin.TakeDamage(distanceScalar * Time.deltaTime * kRoarMaxDamagePerSecond, transform.position, DamageType.Normal, gameObject);
            }
        }

        public void DelayTimeOfNextRoar(float length)
        {
            _timeRoarAgain = Mathf.Max(_timeRoarAgain + length, _timeRoarAgain);
        }

        private AudioClip GetAudioClip(float distance, RoarMode roarMode)
        {
            if (roarMode == RoarMode.CloseOnly)
            {
                return closeSounds.GetRandomClip();
            }
            else if (roarMode == RoarMode.FarOnly)
            {
                return farSounds.GetRandomClip();
            }
            else
            {
                if (distance < closeRoarThreshold && PlayerIsKillable())
                {
                    return closeSounds.GetRandomClip();
                }
                else
                {
                    return farSounds.GetRandomClip();
                }
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

        public enum RoarMode
        {
            /// <summary>
            /// Determine roar sound based on distance
            /// </summary>
            Automatic,
            /// <summary>
            /// Always choose a "normal" or "close" roar
            /// </summary>
            CloseOnly,
            /// <summary>
            /// Always choose a "distant" roar
            /// </summary>
            FarOnly
        }
    }
}
