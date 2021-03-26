using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class VoidBaseSpawner : AlienBaseSpawner
    {
        public override void ConstructBase()
        {
            GameObject baseModel = SpawnPrefab(Mod.voidBaseModel.ClassID, Vector3.zero);
            GenerateAtmospheres(baseModel, "AtmosphereRoot", atmosphereVolume_cache);
            SpawnPrefab(natural_coralClumpYellow, new Vector3(-0.3f, -10f, -5f), Vector3.up * 130f);
            SpawnPrefab(natural_coralClumpYellow_small, new Vector3(2.8f, -10f, -5f), Vector3.up * 55f);
            SpawnPrefab(natural_coralClumpYellow, new Vector3(4.413f, -10f, -0.28f), Vector3.up * 60);
            SpawnPrefab(natural_coralClumpYellow, new Vector3(-3f, -10f, 4f), Vector3.up * 92f);
            SpawnPrefab(natural_coralClumpYellow_variant, new Vector3(5f, -10f, -6f), Vector3.up * -50f);
            SpawnPrefab(natural_rockBlade1, new Vector3(-4f, -10f, -7f), Vector3.up * 15f, Vector3.one * 0.2f);
            SpawnPrefab(natural_rockBlade1, new Vector3(3f, -10f, 6f), Vector3.up * 135f, Vector3.one * 0.21f);
            SpawnPrefab(natural_rockBlade2, new Vector3(2f, -10f, -5f), Vector3.up * -60f, Vector3.one * 0.22f);
            SpawnPrefab(light_big_animated, new Vector3(0f, -0.5f, 36f), new Vector3(0f, 0f, 0f));
            SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 7), Vector3.one, new Vector3(-8.7f + 0.5f, 0.5f, 35));
            SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 7), Vector3.one, new Vector3(8.7f + 1.5f, 0.5f, 35));

            SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 5), Vector3.one, new Vector3(-3f + 0.5f, 0.5f, 18f));
            SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 5), Vector3.one, new Vector3(3f + 1.5f, 0.5f, 18f));

            SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 8), Vector3.one, new Vector3(-3f + 0.5f, 11.5f, 14f), Vector3.left * 180f);
            SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 8), Vector3.one, new Vector3(3f + 1.5f, 11.5f, 14f), Vector3.left * 180f);

            GenerateCable(new Vector3(388, -393, -1805), new Vector3(0.7f, 0f, 0.7f), new Vector3(396, -395, -1762), new Vector3(-0.3f, -0.1f, 0.9f), Vector3.right, 6f);
            GenerateCable(new Vector3(383, -412, -1810), new Vector3(0.7f, 0f, 0.7f), new Vector3(383, -416, -1763), new Vector3(0, 0, 1), Vector3.down, 8f);
            GenerateCable(new Vector3(381, -424, -1812), new Vector3(0.7f, 0f, 0.7f), new Vector3(370.5f, -426, -1769.7f), new Vector3(0.4f, -0.1f, 0.9f), Vector3.down, 8f);
            GenerateCable(new Vector3(379, -436, -1814), new Vector3(0.7f, 0f, 0.7f), new Vector3(398, -451, -1769.9f), new Vector3(-0.1f, 0, 1), Vector3.down, 8f);

            SpawnPrefab(Mod.voidDoor_red.ClassID, new Vector3(0f, 0f, 24f));
            SpawnPrefab(Mod.voidDoor_orange.ClassID, new Vector3(0f, 0f, 24f));
            SpawnPrefab(Mod.voidDoor_white.ClassID, new Vector3(0f, 0f, 24f));
            SpawnPrefab(Mod.voidDoor_blue.ClassID, new Vector3(0f, 0f, 24f));
            SpawnPrefab(Mod.voidDoor_purple.ClassID, new Vector3(0f, 0f, 24f));

            SpawnPrefab(Mod.voidDoor_interior.ClassID, new Vector3(0f, 0f, -8f));
            SpawnPrefab(vfx_entrance, new Vector3(-0.5f, 3f, 24f), new Vector3(90, 0, 0), new Vector3(1.30f, 1.5f, 1f));
        }
    }
}
