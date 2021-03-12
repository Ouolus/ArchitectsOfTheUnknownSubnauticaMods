using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class CablesNearGuardian : AlienBaseSpawner
    {
        public override void ConstructBase()
        {
            GenerateCable(new Vector3(354, -384, -1768), Vector3.up, new Vector3(356, -348, -1756), new Vector3(-0.2f, 0, -), Vector3.back, 10f); //when you're facing the guardian from the void, this is the one on the left
            GenerateCable(new Vector3(382, -386, -1759), Vector3.up, new Vector3(376, -351, -1748), new Vector3(-0.5f, 0.1f, 0.9f), Vector3.back, 10f); //one on the right
        }
    }
}
