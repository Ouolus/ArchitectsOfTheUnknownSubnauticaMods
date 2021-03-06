using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class OutpostBaseSpawner : AlienBaseSpawner
    {
        public override void ConstructBase()
        {
            SpawnPrefabsArray(box2x2x2, 2f, new Vector3(7, 1, 7), Vector3.one, new Vector3(0f, -6f, 0f));
            SpawnPrefabsArray(box2x2x2, 2f, new Vector3(7, 1, 7), Vector3.one * 2f, new Vector3(0f, -12f, 0f));
            SpawnPrefab(light_small, new Vector3(-5f, -5f, -5f));
            SpawnPrefab(light_small, new Vector3(-5f, -5f, 5f));
            SpawnPrefab(light_small, new Vector3(5f, -5f, -5f));
            SpawnPrefab(light_small, new Vector3(5f, -5f, 5f));
            SpawnPrefab(Mod.whiteTabletDoor.ClassID, new Vector3(0f, -5f, 0f));
        }
    }
}
