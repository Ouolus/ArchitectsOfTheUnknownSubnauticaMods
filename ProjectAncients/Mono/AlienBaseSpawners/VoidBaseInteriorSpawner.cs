using UnityEngine;
using System.Collections;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class VoidBaseInteriorSpawner : AlienBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            //Entrance hallway
            yield return StartCoroutine(SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 5), Vector3.one, new Vector3(-3f + 0.5f, 0.5f, 18f)));
            yield return StartCoroutine(SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 5), Vector3.one, new Vector3(3f + 1.5f, 0.5f, 18f)));
            //Entrance hallway ceiling
            yield return StartCoroutine(SpawnPrefabsArray(light_big_animated, 2f, new Vector3(1, 1, 8), Vector3.one, new Vector3(-3f + 0.5f, 11.5f, 14f), Vector3.left * 180f));
            yield return StartCoroutine(SpawnPrefabsArray (light_big_animated, 2f, new Vector3(1, 1, 8), Vector3.one, new Vector3(3f + 1.5f, 11.5f, 14f), Vector3.left * 180f));

            //Aquarium
            yield return StartCoroutine(SpawnPrefab(natural_coralClumpYellow, new Vector3(-0.3f, -10f, -5f), Vector3.up * 130f));
            yield return StartCoroutine(SpawnPrefab(natural_coralClumpYellow_small, new Vector3(2.8f, -10f, -5f), Vector3.up * 55f));
            yield return StartCoroutine(SpawnPrefab(natural_coralClumpYellow, new Vector3(4.413f, -10f, -0.28f), Vector3.up * 60));
            yield return StartCoroutine(SpawnPrefab(natural_coralClumpYellow, new Vector3(-3f, -10f, 4f), Vector3.up * 92f));
            yield return StartCoroutine(SpawnPrefab(natural_coralClumpYellow_variant, new Vector3(5f, -10f, -6f), Vector3.up * -50f));
            yield return StartCoroutine(SpawnPrefab(natural_rockBlade1, new Vector3(-4f, -10f, -7f), Vector3.up * 15f, Vector3.one * 0.2f));
            yield return StartCoroutine(SpawnPrefab(natural_rockBlade1, new Vector3(3f, -10f, 6f), Vector3.up * 135f, Vector3.one * 0.21f));
            yield return StartCoroutine(SpawnPrefab(natural_rockBlade2, new Vector3(2f, -10f, -5f), Vector3.up * -60f, Vector3.one * 0.22f));

            //Supplies
            yield return StartCoroutine(SpawnPrefab(supplies_purpleTablet, new Vector3(13f, -7.3f, -7.5f), Vector3.up * 64f));

            //Egg room
            yield return StartCoroutine(SpawnPrefab(Mod.voidDoor_interior.ClassID, new Vector3(0f, 0f, -8f)));
            yield return StartCoroutine(SpawnPrefab(structure_specialPlatform, new Vector3(0f, 0f, -16f)));
            yield return StartCoroutine(SpawnPrefab(Mod.gargEgg.ClassID, new Vector3(0f, 1f, -16f)));
            yield return StartCoroutine(SpawnPrefab(light_small, new Vector3(2.5f, 0f, -13.5f)));
            yield return StartCoroutine(SpawnPrefab(light_small, new Vector3(2.5f, 0f, -18.5f)));
            yield return StartCoroutine(SpawnPrefab(light_small, new Vector3(-2.5f, 0f, -13.5f)));
            yield return StartCoroutine(SpawnPrefab(light_small, new Vector3(-2.5f, 0f, -18.5f)));
            yield return StartCoroutine(SpawnPrefab(creature_alienRobot, new Vector3(-3f, 1f, -16f)));
            yield return StartCoroutine(SpawnPrefab(creature_alienRobot, new Vector3(3f, 1f, -16f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(0f, 6f, -16f), Vector3.zero, new Vector3(1f, 0.87f, 1f)));
            yield return StartCoroutine(SpawnPrefab(pedestal_empty2, new Vector3(0f, 6f, -16f), (Vector3.left * 180f), new Vector3(1.5f, 0.5f, 1.5f)));
            yield return StartCoroutine(SpawnPrefab(light_small, new Vector3(0f, 4.5f, -16f), Vector3.left * 180f));
            GenerateCable(new Vector3(373.11f + 6f, -389.00f, -1896.07f + 5f + Mod.voidBaseZOffset), Vector3.down, new Vector3(373.11f + 6f, -399.00f - 1f, -1896.97f + 5f + Mod.voidBaseZOffset), Vector3.down, Vector3.zero, 0f, 0.5f);
            GenerateCable(new Vector3(373.11f - 6f, -389.00f, -1896.07f + 5f + Mod.voidBaseZOffset), Vector3.down, new Vector3(373.11f - 6f, -399.00f - 1f, -1896.97f + 5f + Mod.voidBaseZOffset), Vector3.down, Vector3.zero, 0f, 0.5f);
            GenerateCable(new Vector3(373.11f + 6f, -389.00f, -1896.07f - 5f + Mod.voidBaseZOffset), Vector3.down, new Vector3(373.11f + 6f, -399.00f - 1f, -1896.97f - 5f + Mod.voidBaseZOffset), Vector3.down, Vector3.zero, 0f, 0.5f);
            GenerateCable(new Vector3(373.11f - 6f, -389.00f, -1896.07f - 5f + Mod.voidBaseZOffset), Vector3.down, new Vector3(373.11f - 6f, -399.00f - 1f, -1896.97f - 5f + Mod.voidBaseZOffset), Vector3.down, Vector3.zero, 0f, 0.5f);

            //Left room
            yield return StartCoroutine(SpawnPrefab(supplies_purpleTablet, new Vector3(15.45f, 0f, -2.19f), Vector3.up * -68f));
            yield return StartCoroutine(SpawnPrefab(Mod.cachePingsTerminal.ClassID, new Vector3(21f, 0f, 0f), new Vector3(0f, 90f, 0f)));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableIonCube, new Vector3(12.44f, 0f, 6.51f), new Vector3(0f, -27f, 0f)));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableTitanium, new Vector3(12.44f, 0f, 6.51f), new Vector3(0f, -27f, 0f)));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableTitanium, new Vector3(16.03f, 0f, 7.45f), new Vector3(0f, -86.6f, 0f)));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableTitanium, new Vector3(13.28f, 0f, -8.81f), new Vector3(0f, -123f, 0f)));
            GenerateCable(transform.position + new Vector3(21f, 10f, 5f), Vector3.down, transform.position + new Vector3(21f, 0f, 5f), Vector3.down, Vector3.zero, 0f, 0.5f);
            GenerateCable(transform.position + new Vector3(21f, 10f, -5f), Vector3.down, transform.position + new Vector3(21f, 0f, -5f), Vector3.down, Vector3.zero, 0f, 0.5f);

            //Right room
            yield return StartCoroutine(SpawnPrefab(Mod.voidBaseTerminal.ClassID, new Vector3(-21f, 0f, 0f), new Vector3(0f, -90f, 0f)));
            GenerateCable(transform.position + new Vector3(-21f, 10f, 5f), Vector3.down, transform.position + new Vector3(-21f, 0f, 5f), Vector3.down, Vector3.zero, 0f, 0.5f);
            GenerateCable(transform.position + new Vector3(-21f, 10f, -5f), Vector3.down, transform.position + new Vector3(-21f, 0f, -5f), Vector3.down, Vector3.zero, 0f, 0.5f);
        }
    }
}
