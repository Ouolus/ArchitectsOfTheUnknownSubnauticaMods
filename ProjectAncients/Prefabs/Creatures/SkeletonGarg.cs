using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ECCLibrary;

namespace ProjectAncients.Prefabs
{
    //Secret!
    public class SkeletonGarg : GargantuanBase
    {
        public SkeletonGarg(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override ScannableItemData ScannableSettings => new ScannableItemData(true, 5f, Mod.modEncyPath_analysis, null, null);

        public override string GetEncyTitle => "Reanimated Skeleton";

        public override string GetEncyDesc => "Analysis failed. This object is biologically impossible. It is likely controlled by some outside force.";

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            base.AddCustomBehaviour(components);
            components.locomotion.driftFactor = 1f;
            components.locomotion.forwardRotationSpeed = 0.4f;
            components.locomotion.upRotationSpeed = 3f;
            components.locomotion.maxAcceleration = 15f;
        }

        public override void ApplyAggression()
        {

        }

        public override bool AttackPlayer => false;
        public override bool UseSwimSounds => true;

        public override string CloseRoarPrefix => "GargBaby";
        public override string DistantRoarPrefix => "GargBaby";

        public override float TentacleSnapSpeed => 3.5f;

        public override float Mass => 500f;

        public override float BiteDamage => 0f;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(30f, 15f, 30f), 5f, 3f, 0.1f);
        public override StayAtLeashData StayAtLeashSettings => new StayAtLeashData(0.39f, 100f);
        public override AvoidObstaclesData AvoidObstaclesSettings => new AvoidObstaclesData(0.38f, true, 10f);
        public override VFXSurfaceTypes SurfaceType => VFXSurfaceTypes.rock;
        public override AttackLastTargetSettings AttackSettings => new AttackLastTargetSettings(0f, 0f, 0f, 0f, 0f, 0f);
        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Far;
        public override EcoTargetType EcoTargetType => EcoTargetType.CuteFish;

        public override string AttachBoneName => "AttachBone";

        public override bool CanRoar => false;
    }
}
