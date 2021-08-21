namespace RotA.Mono.Creatures.GargEssentials
{
    using ECCLibrary;
    using ECCLibrary.Internal;
    using Prefabs.Creatures;
    using UnityEngine;

    public class GargantuanGrab : MonoBehaviour
    {
        public float vehicleDamagePerSecond;
        public GargGrabFishMode grabFishMode;
        public string attachBoneName;
        
        public Vehicle HeldVehicle { get; private set; }

        private Animator gargAnimator;
        Creature creature;
        Collider[] subrootStoredColliders;
        GargantuanBehaviour behaviour;
        GargantuanMouthAttack mouthAttack;
        SubRoot heldSubroot;
        GameObject heldFish;
        Quaternion vehicleInitialRotation;
        Vector3 vehicleInitialPosition;
        GrabType currentlyGrabbing;
        ECCAudio.AudioClipPool seamothSounds;
        ECCAudio.AudioClipPool exosuitSounds;
        ECCAudio.AudioClipPool cyclopsSounds;
        AudioSource vehicleGrabSound;
        AudioSource leviathanGrabSound;
        Transform vehicleHoldPoint;
        float timeVehicleGrabbed;
        float timeVehicleReleased;
        float currentGrabRandom;
        
        void Start()
        {
            creature = GetComponent<Creature>();
            behaviour = GetComponent<GargantuanBehaviour>();
            vehicleGrabSound = AddGrabSound(50f, 200f);
            leviathanGrabSound = AddGrabSound(15f, 150f);
            vehicleHoldPoint = gameObject.SearchChild(attachBoneName).transform;
            seamothSounds = ECCAudio.CreateClipPool("GargVehicleAttack");
            exosuitSounds = ECCAudio.CreateClipPool("GargVehicleAttack");
            cyclopsSounds = ECCAudio.CreateClipPool("GargCyclopsAttack");
            mouthAttack = GetComponent<GargantuanMouthAttack>();
            gargAnimator = creature.GetAnimator();
        }

        void Update()
        {
            SafeAnimator.SetBool(gargAnimator, "cin_vehicle", IsHoldingGenericSub() || IsHoldingExosuit());
            SafeAnimator.SetBool(gargAnimator, "cin_cyclops", IsHoldingLargeSub());
            bool ghostAnim = false;
            bool deathRollAnim = false;
            if (IsHoldingFish())
            {
                if (currentGrabRandom < 0.5f)
                {
                    ghostAnim = true;
                }
                else
                {
                    deathRollAnim = true;
                }
            }
            SafeAnimator.SetBool(gargAnimator, "cin_ghostleviathanattack", ghostAnim);
            SafeAnimator.SetBool(gargAnimator, "cin_deathroll", deathRollAnim);

            if (CurrentHeldObject == null)
                return;

            Transform held = CurrentHeldObject.transform;
            Transform holdPoint = GetHoldPoint();
            float num = Mathf.Clamp01(Time.time - timeVehicleGrabbed);
            if (num >= 1f)
            {
                if (IsGargJuvenile() && IsHoldingFish())
                {
                    held.transform.position = behaviour.FixJuvenileFishHoldPosition(holdPoint, holdPoint.position);
                }
                else if (IsHoldingLargeSub())
                {
                    held.transform.position = holdPoint.position + (holdPoint.forward * -15f);
                }
                else
                {
                    held.transform.position = holdPoint.position;
                }

                if (IsHoldingLargeSub())
                {
                    held.transform.rotation =
                        behaviour.InverseRotation(holdPoint.transform.rotation); // cyclops faces backwards for whatever reason so we need to invert the rotation
                }
                else if (IsHoldingPickupableFish())
                {
                    held.transform.rotation =
                        behaviour.FixSmallFishRotation(holdPoint.transform.rotation); // cyclops faces backwards for whatever reason so we need to invert the rotation
                }
                else
                {
                    held.transform.rotation = holdPoint.transform.rotation;
                }

                // blood vfx
                if (Time.time > behaviour.timeSpawnBloodAgain && behaviour.CachedBloodPrefab != null)
                {
                    if (IsHoldingFish() && grabFishMode != GargGrabFishMode.PickupableOnlyAndSwalllow)
                    {
                        behaviour.timeSpawnBloodAgain = Time.time + 0.5f;
                        GameObject blood = Instantiate(behaviour.CachedBloodPrefab, held.transform.position,
                            Quaternion.identity);
                        blood.SetActive(true);
                        Destroy(blood, behaviour.bloodDestroyTime);
                        Creature creatureComponent = CurrentHeldObject.GetComponent<Creature>();
                        if (creatureComponent)
                        {
                            creatureComponent.flinch = 10f;
                        }
                    }
                }

                return;
            }

            if (IsGargJuvenile() && IsHoldingFish())
            {
                held.transform.position = (behaviour.FixJuvenileFishHoldPosition(holdPoint, holdPoint.position) - this.vehicleInitialPosition) * num + this.vehicleInitialPosition;
            }
            else
            {
                held.transform.position = (holdPoint.position - this.vehicleInitialPosition) * num + this.vehicleInitialPosition;
            }

            if (IsHoldingLargeSub())
            {
                held.transform.rotation = Quaternion.Lerp(this.vehicleInitialRotation,
                    behaviour.InverseRotation(holdPoint.rotation), num); // cyclops faces backwards for whatever reason so we need to invert the rotation
            }
            else if (IsHoldingPickupableFish())
            {
                held.transform.rotation = Quaternion.Lerp(this.vehicleInitialRotation,
                    behaviour.FixSmallFishRotation(holdPoint.rotation), num); // cyclops faces backwards for whatever reason so we need to invert the rotation
            }
            else
            {
                held.transform.rotation = Quaternion.Lerp(this.vehicleInitialRotation, holdPoint.rotation, num);
            }
        }


        void OnDisable()
        {
            if (HeldVehicle != null)
            {
                ReleaseHeld();
            }
        }

        AudioSource AddGrabSound(float min, float max)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.volume = ECCHelpers.GetECCVolume();
            source.minDistance = min;
            source.maxDistance = max;
            source.spatialBlend = 1f;
            return source;
        }

        GameObject CurrentHeldObject =>
            HeldVehicle ? HeldVehicle.gameObject :
            heldSubroot ? heldSubroot.gameObject :
            heldFish ? heldFish : null;

        Transform GetHoldPoint()
        {
            return vehicleHoldPoint;
        }

        public bool IsHoldingVehicle()
        {
            return currentlyGrabbing != GrabType.None;
        }

        /// <summary>
        /// Holding Seamoth or Seatruck.
        /// </summary>
        /// <returns></returns>
        public bool IsHoldingGenericSub()
        {
            return currentlyGrabbing == GrabType.GenericVehicle;
        }

        /// <summary>
        /// Holding a Cyclops.
        /// </summary>
        /// <returns></returns>
        public bool IsHoldingLargeSub()
        {
            return currentlyGrabbing == GrabType.Cyclops;
        }

        public bool IsHoldingPickupableFish()
        {
            return currentlyGrabbing == GrabType.Fish && grabFishMode == GargGrabFishMode.PickupableOnlyAndSwalllow;
        }

        public bool IsGargJuvenile()
        {
            return grabFishMode == GargGrabFishMode.LeviathansOnlyNoSwallow;
        }

        public bool IsHoldingExosuit()
        {
            return currentlyGrabbing == GrabType.Exosuit;
        }

        public bool IsHoldingFish()
        {
            return currentlyGrabbing == GrabType.Fish;
        }

        public bool IsHoldingGhostLeviathan()
        {
            if (currentlyGrabbing != GrabType.Fish)
            {
                return false;
            }

            if (heldFish == null) 
                return false;
            
            if (heldFish.GetComponent<GhostLeviathan>() is not null || heldFish.GetComponent<GhostLeviatanVoid>() is not null)
            {
                return true;
            }

            return false;
        }

        public bool IsHoldingReaperLeviathan()
        {
            if (currentlyGrabbing != GrabType.Fish)
            {
                return false;
            }

            if (heldFish != null)
            {
                if (heldFish.GetComponent<ReaperLeviathan>() is not null)
                {
                    return true;
                }
            }

            return false;
        }

        enum GrabType
        {
            None,
            Exosuit,
            GenericVehicle,
            Cyclops,
            Fish
        }

        public void GrabLargeSub(SubRoot subRoot)
        {
            GrabSubRoot(subRoot);
        }

        public void GrabGenericSub(Vehicle vehicle)
        {
            GrabVehicle(vehicle, GrabType.GenericVehicle);
        }

        public void GrabExosuit(Vehicle exosuit)
        {
            GrabVehicle(exosuit, GrabType.Exosuit);
        }

        public bool GetCanGrabVehicle()
        {
            return timeVehicleReleased + 10f < Time.time && !IsHoldingVehicle();
        }

        void GrabSubRoot(SubRoot subRoot)
        {
            currentGrabRandom = Random.value;
            heldSubroot = subRoot;
            currentlyGrabbing = GrabType.Cyclops;
            timeVehicleGrabbed = Time.time;
            var subRootTransform = subRoot.transform;
            vehicleInitialRotation = subRootTransform.rotation;
            vehicleInitialPosition = subRootTransform.position;
            vehicleGrabSound.clip = cyclopsSounds.GetRandomClip();
            vehicleGrabSound.Play();
            FreezeRigidbodyWhenFar freezeRb = subRoot.GetComponent<FreezeRigidbodyWhenFar>();
            if (freezeRb)
            {
                freezeRb.enabled = false;
            }
            Stabilizer stabilizer = subRoot.GetComponent<Stabilizer>();
            if (stabilizer)
            {
                stabilizer.enabled = false;
            }

            subrootStoredColliders = subRoot.GetComponentsInChildren<Collider>(false);
            ToggleSubrootColliders(false);
            subRoot.rigidbody.isKinematic = true;
            InvokeRepeating(nameof(DamageVehicle), 1f, 1f);
            float attackLength = 12f;
            Invoke(nameof(ReleaseHeld), attackLength);
            MainCameraControl.main.ShakeCamera(7f, attackLength, MainCameraControl.ShakeMode.BuildUp, 1.2f);
            behaviour.timeCanAttackAgain = Time.time + attackLength + 1f;
        }

        private void GrabVehicle(Vehicle vehicle, GrabType vehicleType)
        {
            currentGrabRandom = Random.value;
            vehicle.useRigidbody.isKinematic = true;
            vehicle.collisionModel.SetActive(false);
            HeldVehicle = vehicle;
            currentlyGrabbing = vehicleType;
            if (currentlyGrabbing == GrabType.Exosuit)
            {
                SafeAnimator.SetBool(vehicle.mainAnimator, "reaper_attack", true);
                Exosuit component = vehicle.GetComponent<Exosuit>();
                if (component != null)
                {
                    component.cinematicMode = true;
                }
            }

            timeVehicleGrabbed = Time.time;
            var vehicleTransform = vehicle.transform;
            vehicleInitialRotation = vehicleTransform.rotation;
            vehicleInitialPosition = vehicleTransform.position;
            if (currentlyGrabbing == GrabType.GenericVehicle)
            {
                vehicleGrabSound.clip = seamothSounds.GetRandomClip();
            }
            else if (currentlyGrabbing == GrabType.Exosuit)
            {
                vehicleGrabSound.clip = exosuitSounds.GetRandomClip();
            }
            else
            {
                ECCLog.AddMessage("Unknown Vehicle Type detected");
            }

            foreach (Collider col in vehicle.GetComponentsInChildren<Collider>())
            {
                col.enabled = false;
            }

            vehicleGrabSound.Play();
            InvokeRepeating(nameof(DamageVehicle), 1f, 1f);
            float attackLength = 4f;
            Invoke(nameof(ReleaseHeld), attackLength);
            if (Player.main.GetVehicle() == HeldVehicle)
            {
                MainCameraControl.main.ShakeCamera(4f, attackLength, MainCameraControl.ShakeMode.BuildUp, 1.2f);
            }
        }

        public void GrabFish(GameObject fish)
        {
            currentGrabRandom = Random.value;
            fish.GetComponent<Rigidbody>().isKinematic = true;
            heldFish = fish;
            currentlyGrabbing = GrabType.Fish;
            timeVehicleGrabbed = Time.time;

            vehicleInitialRotation = fish.transform.rotation;
            vehicleInitialPosition = fish.transform.position;

            if (IsHoldingGhostLeviathan())
            {
                leviathanGrabSound.clip = ECCAudio.LoadAudioClip("GargGhostLeviathanAttack");
                leviathanGrabSound.Play();
            }
            else if (IsHoldingReaperLeviathan())
            {
                leviathanGrabSound.clip = ECCAudio.LoadAudioClip("GargReaperAttack");
                leviathanGrabSound.Play();
            }

            foreach (Collider col in fish.GetComponentsInChildren<Collider>(true))
            {
                col.enabled = false;
            }

            if (grabFishMode == GargGrabFishMode.LeviathansOnlyAndSwallow)
            {
                behaviour.GetBloodEffectFromCreature(fish, 40f, 4f);
                behaviour.timeSpawnBloodAgain = Time.time + 1f;
            }

            if (grabFishMode == GargGrabFishMode.LeviathansOnlyNoSwallow)
            {
                behaviour.GetBloodEffectFromCreature(fish, 20f, 4f);
                behaviour.timeSpawnBloodAgain = Time.time + 1f;
            }

            Invoke(nameof(ReleaseHeld), 5f);
        }

        /// <summary>
        /// Try to deal damage to the held vehicle or subroot
        /// </summary>
        void DamageVehicle()
        {
            if (HeldVehicle != null)
            {
                float dps = vehicleDamagePerSecond;
                HeldVehicle.liveMixin.TakeDamage(dps, type: DamageType.Normal, dealer: gameObject);
                if (!HeldVehicle.liveMixin.IsAlive())
                {
                    if (Player.main.currentMountedVehicle == HeldVehicle)
                    {
                        Player.main.liveMixin.Kill(DamageType.Cold);
                    }
                }
            }

            if (heldSubroot != null)
            {
                const float cyclopsDps = 100f;
                heldSubroot.live.TakeDamage(cyclopsDps, type: DamageType.Normal);
            }
        }

        /// <summary>
        /// Try to release the held vehicle or subroot
        /// </summary>
        public void ReleaseHeld()
        {
            if (HeldVehicle != null)
            {
                if (currentlyGrabbing == GrabType.Exosuit)
                {
                    SafeAnimator.SetBool(HeldVehicle.mainAnimator, "reaper_attack", false);
                    Exosuit component = HeldVehicle.GetComponent<Exosuit>();
                    if (component != null)
                    {
                        component.cinematicMode = false;
                    }
                }

                HeldVehicle.useRigidbody.isKinematic = false;
                HeldVehicle.collisionModel.SetActive(true);
                HeldVehicle = null;
            }

            if (heldSubroot != null)
            {
                FreezeRigidbodyWhenFar freezeRb = heldSubroot.GetComponent<FreezeRigidbodyWhenFar>();
                if (freezeRb)
                {
                    freezeRb.enabled = true;
                }
                Stabilizer stabilizer = heldSubroot.GetComponent<Stabilizer>();
                if (stabilizer)
                {
                    stabilizer.enabled = true;
                }

                heldSubroot.rigidbody.isKinematic = false;
                ToggleSubrootColliders(true);
                heldSubroot = null;
            }

            if (heldFish != null)
            {
                var creatureLm = heldFish.GetComponent<LiveMixin>();
                if (grabFishMode == GargGrabFishMode.LeviathansOnlyAndSwallow ||
                    grabFishMode == GargGrabFishMode.PickupableOnlyAndSwalllow)
                {
                    float animationLength =
                        (grabFishMode == GargGrabFishMode.PickupableOnlyAndSwalllow) ? 0.25f : 0.75f;
                    var swallowing = heldFish.AddComponent<BeingSuckedInWhole>();
                    swallowing.target = mouthAttack.throat.transform;
                    swallowing.animationLength = animationLength;
                    Destroy(heldFish, animationLength);
                }
                else
                {
                    creatureLm.TakeDamage(10000f);
                    foreach (Collider col in heldFish.GetComponentsInChildren<Collider>())
                    {
                        col.enabled = true;
                    }

                    heldFish = null;
                }
            }

            if (currentlyGrabbing != GrabType.Fish)
            {
                timeVehicleReleased = Time.time;
            }

            currentlyGrabbing = GrabType.None;
            CancelInvoke(nameof(DamageVehicle));
            mouthAttack.OnVehicleReleased();
            MainCameraControl.main.ShakeCamera(0f, 0f);
            if (behaviour.lastTarget) behaviour.lastTarget.target = null;
        }

        /// <summary>
        /// Disable cyclops colliders during garg cyclops attack animation
        /// </summary>
        /// <param name="active"></param>
        void ToggleSubrootColliders(bool active)
        {
            if (subrootStoredColliders != null)
            {
                foreach (Collider col in subrootStoredColliders)
                {
                    col.enabled = active;
                }
            }
        }
    }
}