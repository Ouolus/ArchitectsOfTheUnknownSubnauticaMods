using UnityEngine;
using System.Collections;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class OutpostBaseSpawner : AlienBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            yield return StartCoroutine(SpawnPrefab(structure_outpost_1, Vector3.zero));
            yield return StartCoroutine(SpawnPrefabsArray(box2x1x2, 2f, new Vector3(10, 1, 10), Vector3.one, new Vector3(0f, -7.3f, 0f)));
            const float yOffset = -7.3f;
            yield return StartCoroutine(SpawnPrefab(supplies_drillableTitanium, new Vector3(-4f, yOffset, 1f)));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableTitanium, new Vector3(4f, yOffset, -6f)));
            yield return StartCoroutine(SpawnPrefab(supplies_drillableTitanium, new Vector3(1f, yOffset, 3f)));
            yield return StartCoroutine(SpawnPrefab(pedestal_ionCrystal, new Vector3(0f, yOffset, 0f)));
            yield return StartCoroutine(SpawnPrefab(pedestal_empty2, new Vector3(7f, yOffset, 7f)));
            yield return StartCoroutine(SpawnPrefab(pedestal_empty2, new Vector3(7f, yOffset, -9f)));
            yield return StartCoroutine(SpawnPrefab(pedestal_empty2, new Vector3(-9f, yOffset, 7f)));
            yield return StartCoroutine(SpawnPrefab(pedestal_empty2, new Vector3(-9f, yOffset, -9f)));
            yield return StartCoroutine(SpawnPrefab(Mod.tertiaryOutpostTerminal.ClassID, new Vector3(0f, yOffset, -5f), new Vector3(0f, 180f, 0f)));
            yield return StartCoroutine(SpawnPrefab(starfish, new Vector3(0f, yOffset, -6f)));
            yield return StartCoroutine(SpawnPrefab(starfish, new Vector3(-5f, yOffset, -4f)));
            yield return StartCoroutine(SpawnPrefab(starfish, new Vector3(3f, yOffset, 3f)));
            yield return StartCoroutine(SpawnPrefab(creature_alienRobot, new Vector3(3f, yOffset, 3f)));
            yield return StartCoroutine(SpawnPrefab(creature_alienRobot, new Vector3(-3f, yOffset, -3f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(0, 0, 0)));
            yield return StartCoroutine(SpawnPrefab(prop_claw, new Vector3(0, -2, 0)));
            const float smallLightYOffset = -4.3f;
            yield return StartCoroutine(SpawnPrefab(light_small, new Vector3(7f, smallLightYOffset, 7f)));
            yield return StartCoroutine(SpawnPrefab(light_small, new Vector3(7f, smallLightYOffset, -9f)));
            yield return StartCoroutine(SpawnPrefab(light_small, new Vector3(-9f, smallLightYOffset, 7f)));
            yield return StartCoroutine(SpawnPrefab(light_small, new Vector3(-9f, smallLightYOffset, -9f)));
            yield return StartCoroutine(SpawnColumns(-16f));
            yield return null;
        }

        protected IEnumerator SpawnColumns(float yOffset)
        {
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(7f, yOffset, 7f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(7f, yOffset, -9f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(-9f, yOffset, 7f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(-9f, yOffset, -9f)));
        }
    }
}
