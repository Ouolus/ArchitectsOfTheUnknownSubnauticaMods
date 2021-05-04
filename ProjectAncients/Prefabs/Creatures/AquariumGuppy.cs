using UnityEngine;
using ECCLibrary;

namespace ProjectAncients.Prefabs
{
    public class AquariumGuppy : CreatureAsset
    {
        public AquariumGuppy(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override BehaviourType BehaviourType => BehaviourType.MediumFish;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Near;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(7f, 7f, 7f), 3f, 1f, 0.1f);

        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.2f, 10f);

        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.3f, false, 2f);

        public override EcoTargetType EcoTargetType => EcoTargetType.None;

        public override float TurnSpeed => 0.5f;

        public override float MaxVelocityForSpeedParameter => 3f;

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 999f;
        }
    }
}
