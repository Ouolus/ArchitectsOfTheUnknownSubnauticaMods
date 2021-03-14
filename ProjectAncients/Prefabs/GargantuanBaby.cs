using ProjectAncients.Mono;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectAncients.Prefabs
{
    public class GargantuanBaby : GargantuanBase
    {
        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(10f, 3f, 10f), 8f, 1f, 0.1f);
        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.2f, 10f);
        public override VFXSurfaceTypes SurfaceType => VFXSurfaceTypes.organic;
        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0.4f, 10f, 6f, 7f, 2f, 25f);
        public override WaterParkCreatureParameters WaterParkParameters => new WaterParkCreatureParameters(0.1f, 0.5f, 1f, 2f, false);

        public GargantuanBaby(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override void ApplyAggression()
        {
            prefab.AddComponent<GargantuanBabyAggression>();
        }

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            base.AddCustomBehaviour(components);
            CreatureFollowPlayer followPlayer = prefab.AddComponent<CreatureFollowPlayer>();
            followPlayer.distanceToPlayer = 8f;
            followPlayer.creature = components.creature;
            followPlayer.maxYPos = -8f;
            prefab.AddComponent<GargantuanBabyTeleport>();
        }

        public override bool UseSwimSounds => false;

        public override string CloseRoarPrefix => "GargBaby";
        public override string DistantRoarPrefix => "GargBaby";

        public override Vector2int SizeInInventory => new Vector2int(5, 5);

        public override (float, float) RoarSoundMinMax => (5f, 15f);

        public override float TentacleSnapSpeed => 3f;

        public override bool AttackPlayer => false;

        public override float Mass => 600f;
    }
}
