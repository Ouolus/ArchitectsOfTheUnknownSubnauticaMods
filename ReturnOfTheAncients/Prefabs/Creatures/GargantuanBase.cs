using ECCLibrary;
using RotA.Mono.Creatures.CreatureActions;
using RotA.Mono.Creatures.GargEssentials;
using System.Collections.Generic;
using UnityEngine;

namespace RotA.Prefabs.Creatures
{
    public class GargantuanBase : CreatureAsset
    {
        public GargantuanBase(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override BehaviourType BehaviourType => BehaviourType.Leviathan;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.VeryFar;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(120f, 30f, 120f), 10f, 3f, 0.1f);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.2f, 120f);

        public override float TurnSpeed => 0.3f;

        public override float EyeFov => -1f;

        public override EcoTargetType EcoTargetType => EcoTargetType.Leviathan;

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 12f, "Lifeforms/Fauna/Titans", null, null);

        public override bool ScannerRoomScannable => true;

        public override BehaviourLODLevelsStruct BehaviourLODSettings => new BehaviourLODLevelsStruct(75f, 1000f, 2000f);

        public override string GetEncyTitle => "[REDACTED]";

        public override string GetEncyDesc => "[REDACTED]";

        public override bool EnableAggression => true;

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.4f, 24f, 25f, 30f, 17f, 30f);

        public override float Mass => 10000f;

        public override bool AcidImmune => true;

        public override bool CanBeInfected => false;

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(1f, true, 14f);

        public override VFXSurfaceTypes SurfaceType => VFXSurfaceTypes.metal;

        public override UBERMaterialProperties MaterialSettings => new UBERMaterialProperties(5f, 1f, 2f);

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            //advanced collisions stuff
            List<Collider> collidersToIgnore = new List<Collider>();
            collidersToIgnore.Add(prefab.GetComponent<Collider>());
            bool stopPlacingColliders = false;

            List<Transform> spines = new List<Transform>();

            GameObject currentSpine = prefab.SearchChild("Spine");
            while (currentSpine != null)
            {
                currentSpine = currentSpine.SearchChild("Spine", ECCStringComparison.StartsWith);
                if (currentSpine)
                {
                    if (currentSpine.name.Contains("11")) //dont add colliders after you've gone to the 11th spine
                    {
                        stopPlacingColliders = true;
                    }
                    if (AdvancedCollisions == true && stopPlacingColliders == false && currentSpine.name != "Spine") //dont add collider to first spine
                    {
                        var newCapsule = currentSpine.AddComponent<CapsuleCollider>();
                        newCapsule.height = 0.85f;
                        newCapsule.direction = 1;

                        bool firstSpine = currentSpine.name.Contains("001");
                        if (firstSpine)
                        {
                            newCapsule.radius = 0.14f; //first segment of the garg is a lot thinner than the rest, until it gradually tapers off about halfway
                        }
                        else
                        {
                            newCapsule.radius = 0.2f;
                        }
                        collidersToIgnore.Add(newCapsule);
                    }
                    if (currentSpine.name.Contains("59"))
                    {
                        break;
                    }
                    else
                    {
                        spines.Add(currentSpine.transform);
                    }
                }
            }
            if (AdvancedCollisions == true)
            {
                prefab.EnsureComponent<RotA.Mono.Creatures.IgnoreSelfCollisionsAtStart>().collidersToIgnoreEachOther = collidersToIgnore;
            }
            spines.Add(prefab.SearchChild("Tail", ECCStringComparison.Equals).transform);
            spines.Add(prefab.SearchChild("Tail1", ECCStringComparison.Equals).transform);
            spines.Add(prefab.SearchChild("Tail2", ECCStringComparison.Equals).transform);
            spines.Add(prefab.SearchChild("Tail3", ECCStringComparison.Equals).transform);
            spines.Add(prefab.SearchChild("Tail4", ECCStringComparison.Equals).transform);
            spines.Add(prefab.SearchChild("Tail5", ECCStringComparison.Equals).transform);
            spines.Add(prefab.SearchChild("Tail6", ECCStringComparison.Equals).transform);
            FixRotationMultipliers(CreateTrail(prefab.SearchChild("Spine"), spines.ToArray(), components, SpineBoneSnapSpeed, 40f), 0.26f, 0.26f, 0.05f);

            components.creature.Hunger = new CreatureTrait(0f, -0.07f);

            components.locomotion.driftFactor = 1f;
            components.locomotion.forwardRotationSpeed = 0.3f;
            components.locomotion.upRotationSpeed = 1f;

            if (TentaclesHaveTrails)
            {
                FixRotationMultipliers(CreateTrail(prefab.SearchChild("BLT"), components, TentacleSnapSpeed), 0.25f, 0.26f);
                FixRotationMultipliers(CreateTrail(prefab.SearchChild("BRT"), components, TentacleSnapSpeed), 0.25f, 0.26f);
                FixRotationMultipliers(CreateTrail(prefab.SearchChild("TLT"), components, TentacleSnapSpeed), 0.25f, 0.26f);
                FixRotationMultipliers(CreateTrail(prefab.SearchChild("TRT"), components, TentacleSnapSpeed), 0.25f, 0.26f);
                FixRotationMultipliers(CreateTrail(prefab.SearchChild("MLT"), components, TentacleSnapSpeed), 0.25f, 0.26f);
                FixRotationMultipliers(CreateTrail(prefab.SearchChild("MRT"), components, TentacleSnapSpeed), 0.25f, 0.26f);
            }

            CreateTrail(prefab.SearchChild("LLA"), components, JawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("LRA"), components, JawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("SLA"), components, JawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("SRA"), components, JawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("LJT"), components, JawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("RJT"), components, JawTentacleSnapSpeed);

            ApplyAggression();

            var atkLast = prefab.GetComponent<AttackLastTarget>();
            if (atkLast)
            {
                atkLast.resetAggressionOnTime = false;
                atkLast.swimInterval = 0.2f;
            }

            components.locomotion.maxAcceleration = 27f;

            prefab.EnsureComponent<GargantuanBehaviour>();
            
            GargantuanGrab gargantuanGrab = prefab.EnsureComponent<GargantuanGrab>();
            gargantuanGrab.attachBoneName = AttachBoneName;
            gargantuanGrab.vehicleDamagePerSecond = VehicleDamagePerSecond;
            gargantuanGrab.grabFishMode = GrabFishMode;

            GameObject mouth = prefab.SearchChild("Mouth");
            GargantuanMouthAttack mouthAttack = prefab.AddComponent<GargantuanMouthAttack>();
            mouthAttack.mouth = mouth;
            mouthAttack.canBeFed = false;
            mouthAttack.biteInterval = 2f;
            mouthAttack.lastTarget = components.lastTarget;
            mouthAttack.creature = components.creature;
            mouthAttack.liveMixin = components.liveMixin;
            mouthAttack.animator = components.creature.GetAnimator();
            mouthAttack.canAttackPlayer = AttackPlayer;
            mouthAttack.biteDamage = BiteDamage;
            mouthAttack.oneShotPlayer = OneShotsPlayer;
            mouthAttack.attachBoneName = AttachBoneName;
            mouthAttack.canPerformCyclopsCinematic = CanPerformCyclopsCinematic;
            mouthAttack.grabFishMode = GrabFishMode;

            if (AttackPlayer)
            {
                AttackCyclops actionAtkCyclops = prefab.AddComponent<AttackCyclops>();
                actionAtkCyclops.swimVelocity = 25f;
                actionAtkCyclops.aggressiveToNoise = new CreatureTrait(0f, 0.02f);
                actionAtkCyclops.evaluatePriority = 0.5f;
                actionAtkCyclops.priorityMultiplier = ECCHelpers.Curve_Flat();
                actionAtkCyclops.maxDistToLeash = 110f;
                actionAtkCyclops.attackAggressionThreshold = 0.65f;
                actionAtkCyclops.aggressPerSecond = 5f;
            }

            if (CanBeScaredByElectricity)
            {
                prefab.AddComponent<RunAwayWhenScared>();
            }

            if (CanRoar)
            {
                GargantuanRoar roar = prefab.AddComponent<GargantuanRoar>();
                roar.closeSoundsPrefix = CloseRoarPrefix;
                roar.distantSoundsPrefix = DistantRoarPrefix;
                roar.minDistance = RoarSoundMinMax.Item1;
                roar.maxDistance = RoarSoundMinMax.Item2;
                roar.delayMin = RoarDelayMinMax.Item1;
                roar.delayMax = RoarDelayMinMax.Item2;
                roar.screenShake = DoesScreenShake;
                roar.closeRoarThreshold = CloseRoarThreshold;
                roar.roarDoesDamage = RoarDoesDamage;
            }
            if (UseSwimSounds)
            {
                prefab.AddComponent<GargantuanSwimAmbience>();
            }

            if (HasEyeTracking)
            {
                prefab.SearchChild("BLE").AddComponent<GargEyeTracker>();
                prefab.SearchChild("BRE").AddComponent<GargEyeTracker>();
                prefab.SearchChild("FLE").AddComponent<GargEyeTracker>();
                prefab.SearchChild("FRE").AddComponent<GargEyeTracker>();
                prefab.SearchChild("MLE").AddComponent<GargEyeTracker>();
                prefab.SearchChild("MRE").AddComponent<GargEyeTracker>();
            }
            prefab.SearchChild("BLE").AddComponent<GargEyeFixer>();
            prefab.SearchChild("BRE").AddComponent<GargEyeFixer>();
            prefab.SearchChild("FLE").AddComponent<GargEyeFixer>();
            prefab.SearchChild("FRE").AddComponent<GargEyeFixer>();
            prefab.SearchChild("MLE").AddComponent<GargEyeFixer>();
            prefab.SearchChild("MRE").AddComponent<GargEyeFixer>();

            prefab.AddComponent<VFXSchoolFishRepulsor>();

            components.creature.eyeFOV = EyeFov;
        }

        public virtual void ApplyAggression()
        {
            MakeAggressiveTo(80f, 2, EcoTargetType.Shark, 0.2f, 2f);
            MakeAggressiveTo(60f, 2, EcoTargetType.Whale, 0.23f, 2.3f);
            MakeAggressiveTo(250f, 7, EcoTargetType.Leviathan, 0.3f, 5f);
            MakeAggressiveTo(200f, 7, Mod.superDecoyTargetType, 0f, 5f);
        }

        public virtual bool HasEyeTracking
        {
            get
            {
                return false;
            }
        }

        public virtual bool TentaclesHaveTrails
        {
            get
            {
                return true;
            }
        }

        public virtual bool CanRoar
        {
            get
            {
                return true;
            }
        }

        public virtual bool AdvancedCollisions
        {
            get
            {
                return false;
            }
        }

        public virtual bool DoesScreenShake
        {
            get
            {
                return false;
            }
        }
        public virtual float SpineBoneSnapSpeed
        {
            get
            {
                return 0.075f;
            }
        }
        public virtual (float, float) RoarDelayMinMax
        {
            get
            {
                return (11f, 18f);
            }
        }
        public virtual bool UseSwimSounds
        {
            get
            {
                return true;
            }
        }
        public virtual float JawTentacleSnapSpeed
        {
            get
            {
                return 6f;
            }
        }
        public virtual bool CanBeScaredByElectricity
        {
            get
            {
                return false;
            }
        }

        public virtual bool CanPerformCyclopsCinematic
        {
            get
            {
                return false;
            }
        }

        public virtual bool RoarDoesDamage
        {
            get
            {
                return false;
            }
        }

        public virtual string CloseRoarPrefix
        {
            get
            {
                return "garg_roar";
            }
        }

        public virtual bool OneShotsPlayer
        {
            get
            {
                return false;
            }
        }

        public virtual string DistantRoarPrefix
        {
            get
            {
                return "garg_for_anth_distant";
            }
        }

        public virtual (float, float) RoarSoundMinMax
        {
            get
            {
                return (50f, 600f);
            }
        }

        public virtual float BiteDamage
        {
            get
            {
                return 1500f;
            }
        }

        /// <summary>
        /// Seamoth has 300 health. Vehicle attack lasts 4 seconds.
        /// </summary>
        public virtual float VehicleDamagePerSecond
        {
            get
            {
                return 49f;
            }
        }

        public virtual float TentacleSnapSpeed
        {
            get
            {
                return 6f;
            }
        }

        public virtual bool AttackPlayer
        {
            get
            {
                return true;
            }
        }

        public virtual string AttachBoneName
        {
            get
            {
                return "Head.001";
            }
        }

        public virtual float CloseRoarThreshold
        {
            get
            {
                return 150f;
            }
        }

        public virtual GargGrabFishMode GrabFishMode
        {
            get
            {
                return GargGrabFishMode.CantGrabFish;
            }
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 50000f;
        }

        void FixRotationMultipliers(TrailManager tm, float frame1, float frame2)
        {
            AnimationCurve curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, frame1), new Keyframe(1f, frame2) });
            tm.pitchMultiplier = curve;
            tm.rollMultiplier = curve;
            tm.yawMultiplier = curve;
        }

        void FixRotationMultipliers(TrailManager tm, float frame1, float frame2, float frame3)
        {
            AnimationCurve curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, frame1), new Keyframe(0.7f, frame2), new Keyframe(1f, frame3) });
            tm.pitchMultiplier = curve;
            tm.rollMultiplier = curve;
            tm.yawMultiplier = curve;
        }
    }

    public enum GargGrabFishMode
    {
        CantGrabFish,
        LeviathansOnlyNoSwallow,
        LeviathansOnlyAndSwallow,
        PickupableOnlyAndSwalllow
    }
}
