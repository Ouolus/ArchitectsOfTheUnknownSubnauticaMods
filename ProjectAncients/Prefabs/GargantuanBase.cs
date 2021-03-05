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

        public override float TurnSpeed => 0.1f;

        public override float EyeFov => -1f;

        public override EcoTargetType EcoTargetType => EcoTargetType.Leviathan;

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 12f, "Lifeforms/Fauna/Titans", null, null);

        public override bool ScannerRoomScannable => true;

        public override BehaviourLODLevelsStruct BehaviourLODSettings => new BehaviourLODLevelsStruct(75f, 1000f, 2000f);

        public override string GetEncyTitle => "[REDACTED]";

        public override string GetEncyDesc => "[REDACTED]\n\n(Coming soon)";

        public override bool EnableAggression => true;

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.4f, 30f, 25f, 30f, 17f, 30f);

        public override float Mass => 10000f;

        public override bool AcidImmune => true;

        public override bool CanBeInfected => false;

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(1f, true, 14f);

        public override VFXSurfaceTypes SurfaceType => VFXSurfaceTypes.metal;

        public override UBERMaterialProperties MaterialSettings => new UBERMaterialProperties(4f, 5f, 2f);

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            List<Transform> spines = new List<Transform>();
            GameObject currentSpine = prefab.SearchChild("Spine1");
            while(currentSpine != null)
            {
                currentSpine = currentSpine.SearchChild("Spine", ECCStringComparison.StartsWith);
                if (currentSpine)
                {
                    if (currentSpine.name.Contains("end"))
                    {
                        break;
                    }
                    else
                    {
                        spines.Add(currentSpine.transform);
                    }
                }
            }
            CreateTrail(prefab.SearchChild("Spine1"), spines.ToArray(), components, 0.075f);

            components.creature.Hunger = new CreatureTrait(0f, -0.07f);

            const float tentacleSnapSpeed = 5f;
            CreateTrail(prefab.SearchChild("BLT"), components, tentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("BRT"), components, tentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("LTT"), components, tentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("RTT"), components, tentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("MLT"), components, tentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("MRT"), components, tentacleSnapSpeed);

            const float jawTentacleSnapSpeed = 6f;
            CreateTrail(prefab.SearchChild("BLA"), components, jawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("BRA"), components, jawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("FLA"), components, jawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("FRA"), components, jawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("LJT"), components, jawTentacleSnapSpeed);
            CreateTrail(prefab.SearchChild("RJT"), components, jawTentacleSnapSpeed);

            MakeAggressiveTo(60f, 2, EcoTargetType.Shark, 0.2f, 2f);
            MakeAggressiveTo(60f, 2, EcoTargetType.Whale, 0.23f, 2.3f);
            MakeAggressiveTo(250f, 7, EcoTargetType.Leviathan, 0.3f, 5f);

            GargantuanBehaviour gargBehaviour = prefab.AddComponent<GargantuanBehaviour>();
            gargBehaviour.creature = components.creature;

            GameObject mouth = prefab.SearchChild("Mouth");
            GargantuanMouthAttack mouthAttack = prefab.AddComponent<GargantuanMouthAttack>();
            mouthAttack.mouth = mouth;
            mouthAttack.canBeFed = false;
            mouthAttack.biteInterval = 2f;
            mouthAttack.lastTarget = components.lastTarget;
            mouthAttack.creature = components.creature;
            mouthAttack.liveMixin = components.liveMixin;
            mouthAttack.animator = components.creature.GetAnimator();

            GameObject tentacleTrigger = prefab.SearchChild("TentacleTrigger");
            GargantuanTentacleAttack tentacleAttack = prefab.AddComponent<GargantuanTentacleAttack>();
            tentacleAttack.mouth = tentacleTrigger;
            tentacleAttack.canBeFed = false;
            tentacleAttack.biteInterval = 2f;
            tentacleAttack.lastTarget = components.lastTarget;
            tentacleAttack.creature = components.creature;
            tentacleAttack.liveMixin = components.liveMixin;
            tentacleAttack.animator = components.creature.GetAnimator();

            prefab.AddComponent<GargantuanRoar>();
            prefab.AddComponent<GargantuanSwimAmbience>();

            prefab.SearchChild("BLEye").AddComponent<GargEyeTracker>();
            prefab.SearchChild("BREye").AddComponent<GargEyeTracker>();
            prefab.SearchChild("FLEye").AddComponent<GargEyeTracker>();
            prefab.SearchChild("FREye").AddComponent<GargEyeTracker>();
            prefab.SearchChild("MLEye").AddComponent<GargEyeTracker>();
            prefab.SearchChild("MREye").AddComponent<GargEyeTracker>();
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 50000f;
        }
    }
}
