using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class VoidBaseSpawner : AlienBaseSpawner
    {
        public override void ConstructBase()
        {
            GameObject baseModel = SpawnPrefab(Mod.voidBaseModel.ClassID, Vector3.zero);
            GenerateAtmospheres(baseModel, "AtmosphereRoot", atmosphereVolume_cache);
            SpawnPrefab(airlock_1, new Vector3(0f, 0f, 19.75f), Vector3.one);
            SpawnPrefab(vfx_entrance, new Vector3(0f, 0f, 19.75f), new Vector3(90f, 0f, 0f), Vector3.one);
        }
    }
}
