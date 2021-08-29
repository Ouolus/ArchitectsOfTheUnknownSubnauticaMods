using UnityEngine;

namespace RotA.Prefabs.Creatures.Skeletons
{ 
    using SMLHelper.V2.Assets;

    public class GhostSkeletonPose2 : GhostSkeleton
    {
        public GhostSkeletonPose2()
            : base("GhostSkeletonPose2")
        {}

        protected override string ModelName => "GhostSkeletonP2";
    }
}