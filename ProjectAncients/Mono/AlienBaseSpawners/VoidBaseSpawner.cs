using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class VoidBaseSpawner : AlienBaseSpawner
    {
        public override void ConstructBase()
        {
            GameObject baseModel = SpawnPrefab(Mod.voidBaseModel.ClassID, Vector3.zero);
            GenerateAtmospheres(baseModel, "AtmosphereRoot", atmosphereVolume_cache);
            SpawnPrefab(airlock_1, new Vector3(0f, 3f, 19.75f), Vector3.zero, new Vector3(1.7f, 1.5f, 1f));
            SpawnPrefab(vfx_entrance, new Vector3(0f, 3f, 19.75f), new Vector3(1.7f, 1.5f, 1f), Vector3.one);
            SpawnPrefab(natural_coralClumpYellow, new Vector3(-0.3f, -10f, 4f), Vector3.up * 130f);
            SpawnPrefab(natural_coralClumpYellow_small, new Vector3(2.8f, -10f, -5f), Vector3.up * 55f);
            SpawnPrefab(natural_coralClumpYellow, new Vector3(4.413f, -10f, -0.28f), Vector3.up * 60);
            SpawnPrefab(natural_coralClumpYellow, new Vector3(-3f, -10f, 4f), Vector3.up * 92f);
            SpawnPrefab(natural_coralClumpYellow_variant, new Vector3(5f, -10f, -6f), Vector3.up * -50f);
            SpawnPrefab(natural_rockBlade1, new Vector3(2f, -10f, 0f), Vector3.up * 15f, Vector3.one * 0.2f);
            SpawnPrefab(natural_rockBlade1, new Vector3(3f, -10f, 0.4f), Vector3.up * 45f, Vector3.one * 0.21f);
            SpawnPrefab(natural_rockBlade2, new Vector3(4f, -10f, 0.8f), Vector3.up * -15f, Vector3.one * 0.22f);
        }
    }
}
