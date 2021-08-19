using UnityEngine;

namespace RotA.Prefabs.Creatures
{ 
    using SMLHelper.V2.Assets;

    public class GhostSkeletonPose2 : Spawnable
    {
        public GhostSkeletonPose2()
            : base("GhostSkeletonPose2", "Ghost Leviathan Skeleton", "Ghost Leviathan Skeleton Pose2 that makes me go yes")
        {}

        public override GameObject GetGameObject()
        {
            var model = Mod.assetBundle.LoadAsset<GameObject>("GhostSkeletonP2");

            return Object.Instantiate(model);
        }
    }
}