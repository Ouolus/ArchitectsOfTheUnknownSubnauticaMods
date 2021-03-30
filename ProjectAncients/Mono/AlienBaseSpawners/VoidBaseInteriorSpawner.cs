using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class VoidBaseInteriorSpawner : AlienBaseSpawner
    {
        public override void ConstructBase()
        {
            //Entrance hallway
            SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 5), Vector3.one, new Vector3(-3f + 0.5f, 0.5f, 18f));
            SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 5), Vector3.one, new Vector3(3f + 1.5f, 0.5f, 18f));
            //Entrance hallway ceiling
            SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 8), Vector3.one, new Vector3(-3f + 0.5f, 11.5f, 14f), Vector3.left * 180f);
            SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 8), Vector3.one, new Vector3(3f + 1.5f, 11.5f, 14f), Vector3.left * 180f);

            //Aquarium
            SpawnPrefab(natural_coralClumpYellow, new Vector3(-0.3f, -10f, -5f), Vector3.up * 130f);
            SpawnPrefab(natural_coralClumpYellow_small, new Vector3(2.8f, -10f, -5f), Vector3.up * 55f);
            SpawnPrefab(natural_coralClumpYellow, new Vector3(4.413f, -10f, -0.28f), Vector3.up * 60);
            SpawnPrefab(natural_coralClumpYellow, new Vector3(-3f, -10f, 4f), Vector3.up * 92f);
            SpawnPrefab(natural_coralClumpYellow_variant, new Vector3(5f, -10f, -6f), Vector3.up * -50f);
            SpawnPrefab(natural_rockBlade1, new Vector3(-4f, -10f, -7f), Vector3.up * 15f, Vector3.one * 0.2f);
            SpawnPrefab(natural_rockBlade1, new Vector3(3f, -10f, 6f), Vector3.up * 135f, Vector3.one * 0.21f);
            SpawnPrefab(natural_rockBlade2, new Vector3(2f, -10f, -5f), Vector3.up * -60f, Vector3.one * 0.22f);

            //Supplies
            SpawnPrefab(supplies_purpleTablet, new Vector3(13f, -7.3f, -7.5f), Vector3.up * 64f);

            //Egg room
            SpawnPrefab(Mod.voidDoor_interior.ClassID, new Vector3(0f, 0f, -8f));
            SpawnPrefab(structure_specialPlatform, new Vector3(0f, 0f, -16f));
            SpawnPrefab(Mod.gargEgg.ClassID, new Vector3(0f, 1f, -16f));
            SpawnPrefab(light_small, new Vector3(2.5f, 0f, -13.5f));
            SpawnPrefab(light_small, new Vector3(2.5f, 0f, -18.5f));
            SpawnPrefab(light_small, new Vector3(-2.5f, 0f, -13.5f));
            SpawnPrefab(light_small, new Vector3(-2.5f, 0f, -18.5f));
            SpawnPrefab(creature_alienRobot, new Vector3(-4f, 0f, -18.5f));
            SpawnPrefab(creature_alienRobot, new Vector3(4f, 0f, -18.5f));
            SpawnPrefab(structure_column, new Vector3(0f, 6f, -16f), Vector3.zero, new Vector3(1f, 0.87f, 1f));
            SpawnPrefab(pedestal_empty2, new Vector3(0f, 6f, -16f), (Vector3.left * 180f), new Vector3(1.5f, 0.5f, 1.5f));
            SpawnPrefab(light_small, new Vector3(0f, 4.5f, -16f), Vector3.left * 180f);
            GenerateCable(new Vector3(373.11f + 6f, -389.00f, -1896.07f + 5f), Vector3.down, new Vector3(373.11f + 6f, -399.00f - 1f, -1896.97f + 5f), Vector3.down, Vector3.zero, 0f, 0.5f);
            GenerateCable(new Vector3(373.11f - 6f, -389.00f, -1896.07f + 5f), Vector3.down, new Vector3(373.11f - 6f, -399.00f - 1f, -1896.97f + 5f), Vector3.down, Vector3.zero, 0f, 0.5f);
            GenerateCable(new Vector3(373.11f + 6f, -389.00f, -1896.07f - 5f), Vector3.down, new Vector3(373.11f + 6f, -399.00f - 1f, -1896.97f - 5f), Vector3.down, Vector3.zero, 0f, 0.5f);
            GenerateCable(new Vector3(373.11f - 6f, -389.00f, -1896.07f - 5f), Vector3.down, new Vector3(373.11f - 6f, -399.00f - 1f, -1896.97f - 5f), Vector3.down, Vector3.zero, 0f, 0.5f);

            //Left room
            SpawnPrefab(supplies_purpleTablet, new Vector3(15.45f, 0f, -2.19f), Vector3.up * -68f);
        }
    }
}
