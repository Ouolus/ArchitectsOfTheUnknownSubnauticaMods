using UnityEngine;

namespace RotA.Prefabs.Creatures.Skeletons
{ 
    using SMLHelper.V2.Assets;

    public class GhostSkeletonPose1 : GhostSkeleton
    {
        public GhostSkeletonPose1()
            : base("GhostSkeletonPose1")
        {}
        
        protected override string ModelName => "GhostSkeletonP1";
    }
}