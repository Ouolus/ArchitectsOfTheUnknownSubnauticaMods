using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override float EyeFov => 0.6f;

        public override EcoTargetType EcoTargetType => EcoTargetType.Leviathan;

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 12f, "Lifeforms/Fauna/Titans", null, null);

        public override bool ScannerRoomScannable => true;

        public override BehaviourLODLevelsStruct BehaviourLODSettings => new BehaviourLODLevelsStruct(75f, 1000f, 2000f);

        public override string GetEncyTitle => "[REDACTED]";

        public override string GetEncyDesc => "[REDACTED]\n\n(Coming soon)";

        public override bool EnableAggression => true;

        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.4f, 15f, 16f, 20f, 60f, 30f);

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
            CreateTrail(prefab.SearchChild("Spine1"), spines.ToArray(), components, 0.1f);

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

            MakeAggressiveTo(35f, 2, EcoTargetType.Shark, 0.2f, 2f);
            MakeAggressiveTo(100f, 3, EcoTargetType.Leviathan, 0.3f, 5f);

            GargantuanBehaviour gulperBehaviour = prefab.AddComponent<GargantuanBehaviour>();
            gulperBehaviour.creature = components.creature;

            GameObject mouth = prefab.SearchChild("Mouth");
            GargantuanMeleeAttack meleeAttack = prefab.AddComponent<GargantuanMeleeAttack>();
            meleeAttack.mouth = mouth;
            meleeAttack.canBeFed = false;
            meleeAttack.biteInterval = 2f;
            meleeAttack.biteDamage = 75f;
            meleeAttack.eatHungerDecrement = 0.05f;
            meleeAttack.eatHappyIncrement = 0.1f;
            meleeAttack.biteAggressionDecrement = 0.02f;
            meleeAttack.biteAggressionThreshold = 0.1f;
            meleeAttack.lastTarget = components.lastTarget;
            meleeAttack.creature = components.creature;
            meleeAttack.liveMixin = components.liveMixin;
            meleeAttack.animator = components.creature.GetAnimator();

            prefab.AddComponent<GargantuanRoar>();

            mouth.AddComponent<OnTouch>();
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 50000f;
        }
    }
}
