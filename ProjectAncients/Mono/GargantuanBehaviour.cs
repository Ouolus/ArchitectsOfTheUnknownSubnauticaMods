using UnityEngine;
using ECCLibrary;
using ECCLibrary.Internal;

namespace ProjectAncients.Mono
{
    class GargantuanBehaviour : MonoBehaviour, IOnTakeDamage
    {
        private Vehicle heldVehicle;
        private SubRoot heldSubroot;
        private VehicleType heldVehicleType;
        private float timeVehicleGrabbed;
        private float timeVehicleReleased;
        private Quaternion vehicleInitialRotation;
        private Vector3 vehicleInitialPosition;
        private AudioSource vehicleGrabSound;
        private Transform vehicleHoldPoint;
        private GargantuanMouthAttack mouthAttack;
        private RoarAbility roar;
        private ECCAudio.AudioClipPool seamothSounds;
        private ECCAudio.AudioClipPool exosuitSounds;
        private ECCAudio.AudioClipPool cyclopsSounds;

        private Collider[] subrootStoredColliders;

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
            roar = GetComponent<RoarAbility>();
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
            return target.GetComponent<Creature>() || target.GetComponent<Player>() || target.GetComponent<Vehicle>() || target.GetComponent<SubRoot>();
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
        private void DamageVehicle()
        {
            if (heldVehicle != null)
            {
                float dps = vehicleDamagePerSecond;
                heldVehicle.liveMixin.TakeDamage(dps, type: DamageType.Normal);
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
        }

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
                    held.transform.rotation = holdPoint.transform.rotation;
                    return;
                }
                held.transform.position = (holdPoint.position - this.vehicleInitialPosition) * num + this.vehicleInitialPosition;
                held.transform.rotation = Quaternion.Lerp(this.vehicleInitialRotation, holdPoint.rotation, num);
            }
        }
        public void OnTakeDamage(DamageInfo damageInfo)
        {
            if (damageInfo.type == Mod.ar && heldVehicle != null)
            {
                ReleaseVehicle();
            }
        }
        void OnDisable()
        {
            if (heldVehicle != null)
            {
                ReleaseVehicle();
            }
        }
    }
}
