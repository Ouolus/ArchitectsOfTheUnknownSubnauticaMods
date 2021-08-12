using ECCLibrary;
using RotA.Mono.Creatures.CreatureActions;
using RotA.Mono.Creatures.GargEssentials;
using System.Collections.Generic;
using UnityEngine;

namespace RotA.Prefabs.Creatures
{
    public class Guardian : CreatureAsset
    {
        public Guardian(string classId, string friendlyName, string description, GameObject model, Texture2D spriteTexture) : base(classId, friendlyName, description, model, spriteTexture)
        {
        }

        public override BehaviourType BehaviourType => BehaviourType.Leviathan;

        public override LargeWorldEntity.CellLevel CellLevel => LargeWorldEntity.CellLevel.Far;

        public override SwimRandomData SwimRandomSettings => new SwimRandomData(true, new Vector3(40f, 30f, 40f), 4f, 3f, 0.1f);

        public override EcoTargetType EcoTargetType => EcoTargetType.Leviathan;

        public override void AddCustomBehaviour(CreatureComponents components)
        {
            TrailManager trail = CreateTrail(prefab.SearchChild("Neck1"), components, 1f, maxSegmentOffset: 1.5f);
            trail.pitchMultiplier = new AnimationCurve(new[] { new Keyframe(0f, 0f), new Keyframe(1f, 0.9f) });
            trail.rollMultiplier = new AnimationCurve(new[] { new Keyframe(0f, 0f), new Keyframe(1f, 0.9f) });
            trail.yawMultiplier = new AnimationCurve(new[] { new Keyframe(0f, 0f), new Keyframe(1f, 0.9f) });
        }

        public override void SetLiveMixinData(ref LiveMixinData liveMixinData)
        {
            liveMixinData.maxHealth = 20000f;
        }
    }
}
