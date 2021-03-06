using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class OutpostBaseSpawner : AlienBaseSpawner
    {
        public override void ConstructBase()
        {
            SpawnPrefabsArray(box2x2x2, 2f, new Vector3(7, 1, 7), Vector3.one * 3f, new Vector3(0f, 0f, 0f));
            SpawnPrefab(light_small, new Vector3(-21f, 3f, -21f));
            SpawnPrefab(light_small, new Vector3(-21f, 3f, 21f));
            SpawnPrefab(light_small, new Vector3(21f, 3f, -21f));
            SpawnPrefab(light_small, new Vector3(21f, 3f, 21f));
        }
    }
}
