using UnityEngine;
using ECCLibrary;
using ECCLibrary.Internal;

namespace ProjectAncients.Mono
{
    class GargantuanBehaviour : MonoBehaviour
    {
        private Vehicle heldVehicle;
        private VehicleType heldVehicleType;
        private float timeVehicleGrabbed;
        private float timeVehicleReleased;
        private Quaternion vehicleInitialRotation;
        private Vector3 vehicleInitialPosition;
        private AudioSource vehicleGrabSound;
        private Transform vehicleHoldPoint;
        private GargantuanMouthAttack mouthAttack;
        private RoarAbility roar;
        float damagePerSecond = 19f;
        private ECCAudio.AudioClipPool seamothSounds;
        private ECCAudio.AudioClipPool exosuitSounds;

        public Creature creature;
        public float timeCanAttackAgain;

        void Start()
        {
            creature = GetComponent<Creature>();
            vehicleGrabSound = AddVehicleGrabSound();
            vehicleHoldPoint = gameObject.SearchChild("AttachBone").transform;
            seamothSounds = ECCAudio.CreateClipPool("GargVehicleAttack");
            exosuitSounds = ECCAudio.CreateClipPool("GargVehicleAttack");
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
            return target.GetComponent<Creature>() || target.GetComponent<Player>() || target.GetComponent<Vehicle>();
        }

        public bool CanSwallowWhole(GameObject gameObject, LiveMixin liveMixin)
        {
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
        public bool IsHoldingExosuit()
        {
            return heldVehicleType == VehicleType.Exosuit;
        }
        private enum VehicleType
        {
            None = 0,
            Exosuit = 1,
            GenericSub = 2
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
            foreach(Collider col in vehicle.GetComponentsInChildren<Collider>())
            {
                col.enabled = false; //its going to be destroyed  anyway...
            }
            vehicleGrabSound.Play();
            InvokeRepeating("DamageVehicle", 1f, 1f);
            float attackLength = 6f;
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
                float dps = damagePerSecond;
                if (IsHoldingExosuit()) dps *= 3f;
                heldVehicle.liveMixin.TakeDamage(dps, type: DamageType.Normal);
                if (!heldVehicle.liveMixin.IsAlive())
                {
                    if(Player.main.currentMountedVehicle == heldVehicle)
                    {
                        Player.main.liveMixin.Kill(DamageType.Cold);
                    }
                }
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
                timeVehicleReleased = Time.time;
            }
            heldVehicleType = VehicleType.None;
            CancelInvoke("DamageVehicle");
            mouthAttack.OnVehicleReleased();
            MainCameraControl.main.ShakeCamera(0f, 0f);
            roar.PlayRoar();
        }
        public void Update()
        {
            if (heldVehicleType != VehicleType.None && heldVehicle == null)
            {
                ReleaseVehicle();
            }
            SafeAnimator.SetBool(creature.GetAnimator(), "cin_vehicle", IsHoldingVehicle());
            if (heldVehicle != null)
            {
                Transform holdPoint = GetHoldPoint();
                float num = Mathf.Clamp01(Time.time - timeVehicleGrabbed);
                if (num >= 1f)
                {
                    heldVehicle.transform.position = holdPoint.position;
                    heldVehicle.transform.rotation = holdPoint.transform.rotation;
                    return;
                }
                heldVehicle.transform.position = (holdPoint.position - this.vehicleInitialPosition) * num + this.vehicleInitialPosition;
                heldVehicle.transform.rotation = Quaternion.Lerp(this.vehicleInitialRotation, holdPoint.rotation, num);
            }
        }
        public void OnTakeDamage(DamageInfo damageInfo)
        {
            if ((damageInfo.type == DamageType.Electrical || damageInfo.type == DamageType.Poison) && heldVehicle != null)
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
