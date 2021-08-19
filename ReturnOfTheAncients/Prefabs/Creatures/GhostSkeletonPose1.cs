using UnityEngine;

namespace RotA.Prefabs.Creatures
{ 
    using SMLHelper.V2.Assets;

    public class GhostSkeletonPose1 : Spawnable
    {
        public GhostSkeletonPose1()
            : base("GhostSkeletonPose1", "Ghost Leviathan Skeleton", "Ghost Leviathan Skeleton Pose1 that makes me go yes")
        {}

        public override GameObject GetGameObject()
        {
            var model = Mod.assetBundle.LoadAsset<GameObject>("GhostSkeletonP1");

            return Object.Instantiate(model);
        }
    }
}