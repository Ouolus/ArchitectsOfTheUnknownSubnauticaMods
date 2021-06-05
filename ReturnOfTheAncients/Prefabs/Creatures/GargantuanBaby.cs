using RotA.Mono;
using ArchitectsLibrary.MonoBehaviours;
using UnityEngine;
using ECCLibrary;

namespace RotA.Prefabs
{
    public class GargantuanBaby : GargantuanBase
    {
        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(10f, 3f, 10f), 4f, 0.5f, 0.1f);
        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.39f, 9f);
        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.38f, true, 5f);
        public override VFXSurfaceTypes SurfaceType => VFXSurfaceTypes.organic;
        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.4f, 36f, 6f, 7f, 2f, 15f);
        public override WaterParkCreatureParameters WaterParkParameters => new WaterParkCreatureParameters(0.1f, 0.5f, 0.5f, 1f, true);
        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Global;
        public override EcoTargetType EcoTargetType => EcoTargetType.CuteFish;

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 4f, Mod.modEncyPath_creatures, Mod.assetBundle.LoadAsset<Sprite>("Juvenile_Popup"), Mod.assetBundle.LoadAsset<Texture2D>("Baby_Ency"));

        public override string GetEncyTitle => "Gargantuan Leviathan Baby";
        public override string GetEncyDesc => "A very young specimen, hatched from the last known egg of its species.\n\n1. Appearance:\nThis creature appears significantly similar to elder members of its species. However, a thick growing shell suggests this creature is millenniums away from complete loss of scales, which can be observed in only the most ancient specimens.\n\n2. Behavior:\nUnusually, this apex predator appears to be quite attached to its adopter. It goes to the extent of warding off predators much larger than itself if it ensures protection.\n\nAssessment: Valuable survival tool. Treat with care. Always be wary of betrayal.";


        public GargantuanBaby(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override void ApplyAggression()
        {
            prefab.AddComponent<GargantuanBabyAggression>();
        }

        public override float JawTentacleSnapSpeed => 4f;

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            base.AddCustomBehaviour(components);
            CreatureFollowPlayer followPlayer = prefab.AddComponent<CreatureFollowPlayer>();
            followPlayer.distanceToPlayer = 7f;
            followPlayer.creature = components.creature;
            followPlayer.maxYPos = -8f;
            var babyComponent = prefab.AddComponent<GargantuanBabyTeleport>();
            components.locomotion.driftFactor = 1f;
            components.locomotion.forwardRotationSpeed = 0.4f;
            components.locomotion.upRotationSpeed = 3f;
            components.locomotion.maxAcceleration = 15f;
            prefab.GetComponent<AttackLastTarget>().swimInterval = 0.05f;
            GameObject target = prefab.SearchChild("CinematicTarget");
            target.AddComponent<GargBabyTarget>();
            babyComponent.cinematicTarget = target;
            prefab.GetComponent<AvoidObstacles>().scanRadius = 5f;

            prefab.EnsureComponent<GargantuanBabyGrowthManager>();
        }

        public override bool UseSwimSounds => false;

        public override string CloseRoarPrefix => "GargBaby";
        public override string DistantRoarPrefix => "GargBaby";

        public override (float, float) RoarDelayMinMax => (18f, 40f);

        public override Vector2int SizeInInventory => new Vector2int(5, 3);

        public override (float, float) RoarSoundMinMax => (1f, 10f);

        public override float TentacleSnapSpeed => 1.5f;

        public override bool TentaclesHaveTrails => false;

        public override bool AttackPlayer => false;

        public override float Mass => 600f;

        public override float BiteDamage => 500f;

        public override float SpineBoneSnapSpeed => 1f;

        public override HeldFishData ViewModelSettings => new HeldFishData(TechType.Peeper, "GargModel", "GargModel_FP");

        public override bool Pickupable => true;

        public override float BioReactorCharge => 99999999999f;

        public override RespawnData RespawnSettings => new RespawnData(true, 15f);

        public override BehaviourLODLevelsStruct BehaviourLODSettings => new BehaviourLODLevelsStruct(50f, 150f, 500f);

        public override ItemSoundsType ItemSounds => ItemSoundsType.Fish;

        public override float TurnSpeed => 1f;
    }
}
