using UnityEngine;
using ECCLibrary;
using ECCLibrary.Internal;
using ProjectAncients.Mono.Modules;

namespace ProjectAncients.Mono
{
    class GargantuanBehaviour : MonoBehaviour, IOnTakeDamage, IOnArchitectElectricityZap
    {
        Vehicle heldVehicle;
        SubRoot heldSubroot;
        VehicleType heldVehicleType;
        float timeVehicleGrabbed;
        float timeVehicleReleased;
        Quaternion vehicleInitialRotation;
        Vector3 vehicleInitialPosition;
        AudioSource vehicleGrabSound;
        Transform vehicleHoldPoint;
        GargantuanMouthAttack mouthAttack;
        public GargantuanRoar roar;
        ECCAudio.AudioClipPool seamothSounds;
        ECCAudio.AudioClipPool exosuitSounds;
        ECCAudio.AudioClipPool cyclopsSounds;

        Collider[] subrootStoredColliders;

        public Creature creature;
        public float timeCanAttackAgain;
        public string attachBoneName;
        public float vehicleDamagePerSecond;

        void Start()
        {
            creature = GetComponent<Creature>();
            vehicleGrabSound = AddVehicleGrabSound();
            vehicleHoldPoint = gameObject.SearchChild(attachBoneName).transform;
            seamothSounds = ECCAudio.CreateClipPool("GargVehicleAttack");
            exosuitSounds = ECCAudio.CreateClipPool("GargVehicleAttack");
            cyclopsSounds = ECCAudio.CreateClipPool("GargVehicleAttack");
            mouthAttack = GetComponent<GargantuanMouthAttack>();
            roar = GetComponent<GargantuanRoar>();
        }

        Transform GetHoldPoint()
        {
            return vehicleHoldPoint;
        }
        private AudioSource AddVehicleGrabSound()
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.volume = ECCHelpers.GetECCVolume() * 0.75f;
            source.minDistance = 5f;
            source.maxDistance = 20f;
            source.spatialBlend = 1f;
            return source;
        }

        public bool Edible(GameObject target)
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
            if(gameObject is null)
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
            return heldVehicleType != VehicleType.None;
        }
        /// <summary>
        /// Holding Seamoth or Seatruck.
        /// </summary>
        /// <returns></returns>
        public bool IsHoldingGenericSub()
        {
            return heldVehicleType == VehicleType.GenericSub;
        }
        /// <summary>
        /// Holding a Cyclops.
        /// </summary>
        /// <returns></returns>
        public bool IsHoldingLargeSub()
        {
            return heldVehicleType == VehicleType.Cyclops;
        }
        public bool IsHoldingExosuit()
        {
            return heldVehicleType == VehicleType.Exosuit;
        }
        private enum VehicleType
        {
            None = 0,
            Exosuit = 1,
            GenericSub = 2,
            Cyclops
        }
        public void GrabLargeSub(SubRoot subRoot)
        {
            GrabSubRoot(subRoot);
        }
        public void GrabGenericSub(Vehicle vehicle)
        {
            GrabVehicle(vehicle, VehicleType.GenericSub);
        }
        public void GrabExosuit(Vehicle exosuit)
        {
            GrabVehicle(exosuit, VehicleType.Exosuit);
        }
        public bool GetCanGrabVehicle()
        {
            return timeVehicleReleased + 10f < Time.time && !IsHoldingVehicle();
        }
        private void GrabSubRoot(SubRoot subRoot)
        {
            heldSubroot = subRoot;
            heldVehicleType = VehicleType.Cyclops;
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
            float attackLength = 4f;
            Invoke("ReleaseVehicle", attackLength);
            MainCameraControl.main.ShakeCamera(7f, attackLength, MainCameraControl.ShakeMode.BuildUp, 1.2f);
            timeCanAttackAgain = Time.time + attackLength;
        }
        private void GrabVehicle(Vehicle vehicle, VehicleType vehicleType)
        {
            vehicle.GetComponent<Rigidbody>().isKinematic = true;
            vehicle.collisionModel.SetActive(false);
            heldVehicle = vehicle;
            heldVehicleType = vehicleType;
            if (heldVehicleType == VehicleType.Exosuit)
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
            if (heldVehicleType == VehicleType.GenericSub)
            {
                vehicleGrabSound.clip = seamothSounds.GetRandomClip();
            }
            else if (heldVehicleType == VehicleType.Exosuit)
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
        public static bool PlayerIsKillable()
        {
            if (Player.main.GetCurrentSub() != null) 
            {
                return false;
            }
            string biome = Player.main.GetBiomeString();
            if(biome.StartsWith("precursor", System.StringComparison.OrdinalIgnoreCase) || biome.StartsWith("prison", System.StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return true;
            
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
                if (heldVehicleType == VehicleType.Exosuit)
                {
                    SafeAnimator.SetBool(heldVehicle.mainAnimator, "reaper_attack", false);
                    Exosuit component = heldVehicle.GetComponent<Exosuit>();
                    if (component != null)
                    {
                        component.cinematicMode = false;
                    }
                }
                heldVehicle.GetComponent<Rigidbody>().isKinematic = false;
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
            timeVehicleReleased = Time.time;
            heldVehicleType = VehicleType.None;
            CancelInvoke("DamageVehicle");
            mouthAttack.OnVehicleReleased();
            MainCameraControl.main.ShakeCamera(0f, 0f);
            var lastTarget = gameObject.GetComponent<LastTarget>();
            if(lastTarget) lastTarget.target = null;
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
            if (heldVehicleType != VehicleType.None && (heldVehicle == null && heldSubroot == null))
            {
                ReleaseVehicle();
            }
            SafeAnimator.SetBool(creature.GetAnimator(), "cin_vehicle", IsHoldingGenericSub() || IsHoldingExosuit());
            SafeAnimator.SetBool(creature.GetAnimator(), "cin_cyclops", IsHoldingLargeSub());
            GameObject held = null;
            if (heldVehicle != null)
            {
                held = heldVehicle.gameObject;
            }
            if (heldSubroot != null)
            {
                held = heldSubroot.gameObject;
            }
            if (held != null)
            {
                Transform holdPoint = GetHoldPoint();
                float num = Mathf.Clamp01(Time.time - timeVehicleGrabbed);
                if (num >= 1f)
                {
                    held.transform.position = holdPoint.position;
                    if (IsHoldingLargeSub())
                    {
                        held.transform.rotation = InverseRotation(holdPoint.transform.rotation); //cyclops faces backwards for whatever reason so we need to invert the rotation
                    }
                    else
                    {
                        held.transform.rotation = holdPoint.transform.rotation;
                    }
                    return;
                }
                held.transform.position = (holdPoint.position - this.vehicleInitialPosition) * num + this.vehicleInitialPosition;
                if (IsHoldingLargeSub())
                {
                    held.transform.rotation = Quaternion.Lerp(this.vehicleInitialRotation, InverseRotation(holdPoint.rotation), num); //cyclops faces backwards for whatever reason so we need to invert the rotation
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
            var lastTarget = gameObject.GetComponent<LastTarget>();
            if (lastTarget) lastTarget.target = null;
            roar.PlayOnce(out _, GargantuanRoar.RoarMode.CloseOnly);
        }
    }
}
