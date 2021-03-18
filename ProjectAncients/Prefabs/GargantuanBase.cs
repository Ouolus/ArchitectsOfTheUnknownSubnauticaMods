using System;
using System.Collections.Generic;
using ECCLibrary;
using ProjectAncients.Mono;
using UnityEngine;

namespace ProjectAncients.Prefabs
{
    public class GargantuanBase : CreatureAsset
    {
        public GargantuanBase(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override BehaviourType BehaviourType => BehaviourType.Leviathan;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.VeryFar;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(120f, 30f, 120f), 10f, 3f, 0.1f);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.2f, 150f);

        public override float TurnSpeed => 0.3f;

        public override float EyeFov => -1f;

        public override EcoTargetType EcoTargetType => EcoTargetType.Leviathan;

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 12f, "Lifeforms/Fauna/Titans", null, null);

        public override bool ScannerRoomScannable => true;

        public override BehaviourLODLevelsStruct BehaviourLODSettings => new BehaviourLODLevelsStruct(75f, 1000f, 2000f);

        public override string GetEncyTitle => "[REDACTED]";

        public override string GetEncyDesc => "[REDACTED]\n\n(Coming soon)";

        public override bool EnableAggression => true;

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.4f, 24f, 25f, 30f, 17f, 30f);

        public override float Mass => 10000f;

        public override bool AcidImmune => true;

        public override bool CanBeInfected => false;

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(1f, true, 14f);

        public override VFXSurfaceTypes SurfaceType => VFXSurfaceTypes.metal;

        public override UBERMaterialProperties MaterialSettings => new UBERMaterialProperties(10f, 1f, 2f);

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            List<Transform> spines = new List<Transform>();
            GameObject currentSpine = prefab.SearchChild("Spine");
            while(currentSpine != null)
            {
                currentSpine = currentSpine.SearchChild("Spine", ECCStringComparison.StartsWith);
                if (currentSpine)
                {
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
            FixRotationMultipliers(CreateTrail(prefab.SearchChild("Spine"), spines.ToArray(), components, 0.075f, 40f), 0.26f, 0.26f);

            components.creature.Hunger = new CreatureTrait(0f, -0.07f);

            components.locomotion.driftFactor = 1f;
            components.locomotion.forwardRotationSpeed = 0.3f;
            components.locomotion.upRotationSpeed = 1f;

            FixRotationMultipliers(CreateTrail(prefab.SearchChild("BLT"), components, TentacleSnapSpeed), 0.25f, 0.26f);
            FixRotationMultipliers(CreateTrail(prefab.SearchChild("BRT"), components, TentacleSnapSpeed), 0.25f, 0.26f);
            FixRotationMultipliers(CreateTrail(prefab.SearchChild("TLT"), components, TentacleSnapSpeed), 0.25f, 0.26f);
            FixRotationMultipliers(CreateTrail(prefab.SearchChild("TRT"), components, TentacleSnapSpeed), 0.25f, 0.26f);
            FixRotationMultipliers(CreateTrail(prefab.SearchChild("MLT"), components, TentacleSnapSpeed), 0.25f, 0.26f);
            FixRotationMultipliers(CreateTrail(prefab.SearchChild("MRT"), components, TentacleSnapSpeed), 0.25f, 0.26f);

            const float jawTentacleSnapSpeed = 6f;
            CreateTrail(prefab.SearchChild("LLA"), components, jawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("LRA"), components, jawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("SLA"), components, jawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("SRA"), components, jawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("LJT"), components, jawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("RJT"), components, jawTentacleSnapSpeed);

            ApplyAggression();

            var atkLast = prefab.GetComponent<AttackLastTarget>();
            atkLast.resetAggressionOnTime = false;
            atkLast.swimInterval = 0.2f;

            components.locomotion.maxAcceleration = 27f;

            GargantuanBehaviour gargBehaviour = prefab.AddComponent<GargantuanBehaviour>();
            gargBehaviour.creature = components.creature;
            gargBehaviour.attachBoneName = AttachBoneName;
            gargBehaviour.vehicleDamagePerSecond = VehicleDamagePerSecond;

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

            /*GameObject tentacleTrigger = prefab.SearchChild("TentacleTrigger");
            GargantuanTentacleAttack tentacleAttack = prefab.AddComponent<GargantuanTentacleAttack>();
            tentacleAttack.mouth = tentacleTrigger;
            tentacleAttack.canBeFed = false;
            tentacleAttack.biteInterval = 2f;
            tentacleAttack.lastTarget = components.lastTarget;
            tentacleAttack.creature = components.creature;
            tentacleAttack.liveMixin = components.liveMixin;
            tentacleAttack.animator = components.creature.GetAnimator();*/

            GargantuanRoar roar = prefab.AddComponent<GargantuanRoar>();
            roar.closeSoundsPrefix = CloseRoarPrefix;
            roar.distantSoundsPrefix = DistantRoarPrefix;
            roar.minDistance = RoarSoundMinMax.Item1;
            roar.maxDistance = RoarSoundMinMax.Item2;
            if (UseSwimSounds)
            {
                prefab.AddComponent<GargantuanSwimAmbience>();
            }

            prefab.SearchChild("BLE").AddComponent<GargEyeTracker>();
            prefab.SearchChild("BRE").AddComponent<GargEyeTracker>();
            prefab.SearchChild("FLE").AddComponent<GargEyeTracker>();
            prefab.SearchChild("FRE").AddComponent<GargEyeTracker>();
            prefab.SearchChild("MLE").AddComponent<GargEyeTracker>();
            prefab.SearchChild("MRE").AddComponent<GargEyeTracker>();
        }

        public virtual void ApplyAggression()
        {
            MakeAggressiveTo(60f, 2, EcoTargetType.Shark, 0.2f, 2f);
            MakeAggressiveTo(60f, 2, EcoTargetType.Whale, 0.23f, 2.3f);
            MakeAggressiveTo(250f, 7, EcoTargetType.Leviathan, 0.3f, 5f);
        }

        public virtual bool UseSwimSounds
        {
            get
            {
                return true;
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
                return 5f;
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

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 50000f;
        }

        void FixRotationMultipliers(TrailManager tm, float min, float max)
        {
            AnimationCurve curve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, min), new Keyframe(1f, max) });
            tm.pitchMultiplier = curve;
            tm.rollMultiplier = curve;
            tm.yawMultiplier = curve;
        }
    }
}
