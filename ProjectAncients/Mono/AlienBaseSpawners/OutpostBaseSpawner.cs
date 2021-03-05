using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class OutpostBaseSpawner : AlienBaseSpawner
    {
        public override void ConstructBase()
        {
            SpawnPrefab(box2x2, Vector3.zero);
            SpawnPrefab(smallLight, new Vector3(0f, 2f, 0f));
        }
    }
}
