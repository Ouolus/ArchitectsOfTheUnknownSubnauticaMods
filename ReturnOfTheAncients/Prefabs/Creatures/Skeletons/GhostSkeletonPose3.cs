using UnityEngine;

namespace RotA.Prefabs.Creatures.Skeletons
{ 
    using SMLHelper.V2.Assets;

    public class GhostSkeletonPose3 : GhostSkeleton
    {
        public GhostSkeletonPose3()
            : base("GhostSkeletonPose3")
        {}

        protected override string ModelName => "GhostSkeletonP3";
    }
}