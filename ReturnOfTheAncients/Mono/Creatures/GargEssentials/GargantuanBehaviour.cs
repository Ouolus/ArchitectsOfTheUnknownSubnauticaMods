using ECCLibrary;
using ECCLibrary.Internal;
using RotA.Mono.Modules;
using RotA.Prefabs.Creatures;
using UnityEngine;

namespace RotA.Mono.Creatures.GargEssentials
{
    class GargantuanBehaviour : MonoBehaviour, IOnTakeDamage, IOnArchitectElectricityZap
    {
        Vehicle heldVehicle;
        SubRoot heldSubroot;
        GameObject heldFish;
        GrabType currentlyGrabbing;
        float timeVehicleGrabbed;
        float timeVehicleReleased;
        Quaternion vehicleInitialRotation;
        Vector3 vehicleInitialPosition;
        AudioSource vehicleGrabSound;
        AudioSource leviathanGrabSound;
        Transform vehicleHoldPoint;
        GargantuanMouthAttack mouthAttack;
        public GargantuanRoar roar;
        ECCAudio.AudioClipPool seamothSounds;
        ECCAudio.AudioClipPool exosuitSounds;
        ECCAudio.AudioClipPool cyclopsSounds;
        LastTarget lastTarget;
        public GargGrabFishMode grabFishMode;

        Collider[] subrootStoredColliders;

        public Creature creature;
        public float timeCanAttackAgain;
        public string attachBoneName;
        public float vehicleDamagePerSecond;

        void Start()
        {
            creature = GetComponent<Creature>();
            vehicleGrabSound = AddGrabSound(50f, 200f);
            leviathanGrabSound = AddGrabSound(15f, 150f);
            vehicleHoldPoint = gameObject.SearchChild(attachBoneName).transform;
            seamothSounds = ECCAudio.CreateClipPool("GargVehicleAttack");
            exosuitSounds = ECCAudio.CreateClipPool("GargVehicleAttack");
            cyclopsSounds = ECCAudio.CreateClipPool("GargCyclopsAttack");
            mouthAttack = GetComponent<GargantuanMouthAttack>();
            roar = GetComponent<GargantuanRoar>();
            lastTarget = gameObject.GetComponent<LastTarget>();
        }

        GameObject CurrentHeldObject
        {
            get
            {
                if (heldVehicle != null)
                {
                    return heldVehicle.gameObject;
                }
                if (heldSubroot != null)
                {
                    return heldSubroot.gameObject;
                }
                if (heldFish != null)
                {
                    return heldFish;
                }
                return null;
            }
        }

        Transform GetHoldPoint()
        {
            return vehicleHoldPoint;
        }
        private AudioSource AddGrabSound(float min, float max)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.volume = ECCHelpers.GetECCVolume();
            source.minDistance = min;
            source.maxDistance = max;
            source.spatialBlend = 1f;
            return source;
        }

        public bool CanEat(GameObject target)
        {
            return target.GetComponent<Creature>() || target.GetComponent<Player>() || target.GetComponent<Vehicle>() || target.GetComponent<SubRoot>() || target.GetComponent<CyclopsDecoy>();
        }

        public bool CanSwallowWhole(GameObject gameObject, LiveMixin liveMixin)
        {
            if ((liveMixin.health - DamageSystem.CalculateDamage(600f, DamageType.Normal, gameObject)) <= 0)
            {
                return false;
            }
            if (gameObject.GetComponentInParent<Player>())
            {
                return false;
            }
            if (gameObject.GetComponentInChildren<Player>())
            {
                return false;
            }
            if (gameObject.GetComponentInParent<Vehicle>())
            {
                return false;
            }
            if (gameObject.GetComponentInParent<SubRoot>())
            {
                return false;
            }
            if (liveMixin.maxHealth > 600f)
            {
                return false;
            }
            if (liveMixin.invincible)
            {
                return false;
            }
            return true;
        }

        public bool IsVehicle(GameObject gameObject)
        {
            if (gameObject is null)
            {
                return false;
            }
            if (gameObject.GetComponentInParent<Vehicle>())
            {
                return true;
            }
            if (gameObject.GetComponentInParent<SubRoot>())
            {
                return true;
            }
            return false;
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
            if (heldFish != null)
            {
                if (heldFish.GetComponent<GhostLeviathan>() is not null || heldFish.GetComponent<GhostLeviatanVoid>() is not null)
                {
                    return true;
                }
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
        private enum GrabType
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
        private void GrabSubRoot(SubRoot subRoot)
        {
            heldSubroot = subRoot;
            currentlyGrabbing = GrabType.Cyclops;
            timeVehicleGrabbed = Time.time;
            vehicleInitialRotation = subRoot.transform.rotation;
            vehicleInitialPosition = subRoot.transform.position;
            vehicleGrabSound.clip = cyclopsSounds.GetRandomClip();
            vehicleGrabSound.Play();
            FreezeRigidbodyWhenFar freezeRb = subRoot.GetComponent<FreezeRigidbodyWhenFar>();
            if (freezeRb)
            {
                freezeRb.enabled = false;
            }
            subrootStoredColliders = subRoot.GetComponentsInChildren<Collider>(false);
            ToggleSubrootColliders(false);
            subRoot.rigidbody.isKinematic = true;
            InvokeRepeating("DamageVehicle", 1f, 1f);
            float attackLength = 10f;
            Invoke("ReleaseVehicle", attackLength);
            MainCameraControl.main.ShakeCamera(7f, attackLength, MainCameraControl.ShakeMode.BuildUp, 1.2f);
            timeCanAttackAgain = Time.time + attackLength + 1f;
        }
        private void GrabVehicle(Vehicle vehicle, GrabType vehicleType)
        {
            vehicle.useRigidbody.isKinematic = true;
            vehicle.collisionModel.SetActive(false);
            heldVehicle = vehicle;
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
            vehicleInitialRotation = vehicle.transform.rotation;
            vehicleInitialPosition = vehicle.transform.position;
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
            InvokeRepeating("DamageVehicle", 1f, 1f);
            float attackLength = 4f;
            Invoke("ReleaseVehicle", attackLength);
            if (Player.main.GetVehicle() == heldVehicle)
            {
                MainCameraControl.main.ShakeCamera(4f, attackLength, MainCameraControl.ShakeMode.BuildUp, 1.2f);
            }
        }
        public void GrabFish(GameObject fish)
        {
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

            Invoke("ReleaseVehicle", 5f);
        }
        public static bool PlayerIsKillable()
        {
            if (Player.main.GetCurrentSub() != null)
            {
                return false;
            }
            if (PlayerInPrecursorBase())
            {
                return false;
            }
            return true;

        }
        public static bool PlayerInPrecursorBase()
        {
            string biome = Player.main.GetBiomeString();
            if (biome.StartsWith("precursor", System.StringComparison.OrdinalIgnoreCase) || biome.StartsWith("prison", System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Try to deal damage to the held vehicle or subroot
        /// </summary>
        private void DamageVehicle()
        {
            if (heldVehicle != null)
            {
                float dps = vehicleDamagePerSecond;
                heldVehicle.liveMixin.TakeDamage(dps, type: DamageType.Normal, dealer: gameObject);
                if (!heldVehicle.liveMixin.IsAlive())
                {
                    if (Player.main.currentMountedVehicle == heldVehicle)
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
        public void ReleaseVehicle()
        {
            if (heldVehicle != null)
            {
                if (currentlyGrabbing == GrabType.Exosuit)
                {
                    SafeAnimator.SetBool(heldVehicle.mainAnimator, "reaper_attack", false);
                    Exosuit component = heldVehicle.GetComponent<Exosuit>();
                    if (component != null)
                    {
                        component.cinematicMode = false;
                    }
                }
                heldVehicle.useRigidbody.isKinematic = false;
                heldVehicle.collisionModel.SetActive(true);
                heldVehicle = null;
            }
            if (heldSubroot != null)
            {
                FreezeRigidbodyWhenFar freezeRb = heldSubroot.GetComponent<FreezeRigidbodyWhenFar>();
                if (freezeRb)
                {
                    freezeRb.enabled = false;
                }
                heldSubroot.rigidbody.isKinematic = false;
                ToggleSubrootColliders(true);
                heldSubroot = null;
            }
            if (heldFish != null)
            {
                var creatureLm = heldFish.GetComponent<LiveMixin>();
                if (grabFishMode == GargGrabFishMode.LeviathansOnlyAndSwallow || grabFishMode == GargGrabFishMode.PickupableOnlyAndSwalllow)
                {
                    float animationLength = (grabFishMode == GargGrabFishMode.PickupableOnlyAndSwalllow) ? 0.5f : 1.5f;
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
            CancelInvoke("DamageVehicle");
            mouthAttack.OnVehicleReleased();
            MainCameraControl.main.ShakeCamera(0f, 0f);
            if (lastTarget) lastTarget.target = null;
        }

        /// <summary>
        /// Disable cyclops colliders during garg cyclops attack animation
        /// </summary>
        /// <param name="active"></param>
        private void ToggleSubrootColliders(bool active)
        {
            if (subrootStoredColliders != null)
            {
                foreach (Collider col in subrootStoredColliders)
                {
                    col.enabled = active;
                }
            }
        }
        public void Update()
        {
            if (currentlyGrabbing != GrabType.None && heldVehicle == null && heldSubroot == null && heldFish == null)
            {
                ReleaseVehicle();
            }

            SafeAnimator.SetBool(creature.GetAnimator(), "cin_vehicle", IsHoldingGenericSub() || IsHoldingExosuit());
            SafeAnimator.SetBool(creature.GetAnimator(), "cin_cyclops", IsHoldingLargeSub());
            SafeAnimator.SetBool(creature.GetAnimator(), "cin_ghostleviathanattack", IsHoldingFish());

            if (CurrentHeldObject != null)
            {
                Transform held = CurrentHeldObject.transform;
                Transform holdPoint = GetHoldPoint();
                float num = Mathf.Clamp01(Time.time - timeVehicleGrabbed);
                if (num >= 1f)
                {
                    if (IsGargJuvenile() && IsHoldingFish())
                    {
                        held.transform.position = FixJuvenileFishHoldPosition(holdPoint, holdPoint.position);
                    }
                    else
                    {
                        held.transform.position = holdPoint.position;
                    }
                    if (IsHoldingLargeSub())
                    {
                        held.transform.rotation = InverseRotation(holdPoint.transform.rotation); //cyclops faces backwards for whatever reason so we need to invert the rotation
                    }
                    else if (IsHoldingPickupableFish())
                    {
                        held.transform.rotation = FixSmallFishRotation(holdPoint.transform.rotation); //cyclops faces backwards for whatever reason so we need to invert the rotation
                    }
                    else
                    {
                        held.transform.rotation = holdPoint.transform.rotation;
                    }
                    return;
                }
                if (IsGargJuvenile() && IsHoldingFish())
                {
                    held.transform.position = (FixJuvenileFishHoldPosition(holdPoint, holdPoint.position) - this.vehicleInitialPosition) * num + this.vehicleInitialPosition;
                }
                else
                {
                    held.transform.position = (holdPoint.position - this.vehicleInitialPosition) * num + this.vehicleInitialPosition;
                }
                if (IsHoldingLargeSub())
                {
                    held.transform.rotation = Quaternion.Lerp(this.vehicleInitialRotation, InverseRotation(holdPoint.rotation), num); //cyclops faces backwards for whatever reason so we need to invert the rotation
                }
                else if (IsHoldingPickupableFish())
                {
                    held.transform.rotation = Quaternion.Lerp(this.vehicleInitialRotation, FixSmallFishRotation(holdPoint.rotation), num); //cyclops faces backwards for whatever reason so we need to invert the rotation
                }
                else
                {
                    held.transform.rotation = Quaternion.Lerp(this.vehicleInitialRotation, holdPoint.rotation, num);
                }
            }
        }
        private Quaternion InverseRotation(Quaternion input)
        {
            return Quaternion.Euler(input.eulerAngles + new Vector3(0f, 180f, 0f));
        }
        private Quaternion FixSmallFishRotation(Quaternion input)
        {
            return Quaternion.Euler(input.eulerAngles + new Vector3(0f, 0f, 90f));
        }
        private Vector3 FixJuvenileFishHoldPosition(Transform holdPoint, Vector3 input)
        {
            return input + (holdPoint.up * 3f);
        }
        public void OnTakeDamage(DamageInfo damageInfo)
        {
            if (damageInfo.type == Mod.architectElect)
            {
                OnDamagedByArchElectricity();
            }
        }
        void OnDisable()
        {
            if (heldVehicle != null)
            {
                ReleaseVehicle();
            }
        }

        public void OnDamagedByArchElectricity()
        {
            if (heldVehicle is not null)
            {
                ReleaseVehicle();
            }
            else
            {
                creature.Scared.Value = 1f;
                creature.Aggression.Value = 0f;
                timeCanAttackAgain = Time.time + 5f;
            }
            if (lastTarget != null) lastTarget.target = null;
        }
    }
}
