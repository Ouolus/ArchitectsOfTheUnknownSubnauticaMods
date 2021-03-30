using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class OutpostBaseSpawner : AlienBaseSpawner
    {
        public override void ConstructBase()
        {
            SpawnPrefab(structure_outpost_1, Vector3.zero);
            SpawnPrefabsArray(box2x1x2, 2f, new Vector3(10, 1, 10), Vector3.one, new Vector3(0f, -7.3f, 0f));
            const float yOffset = -7.3f;
            SpawnPrefab(supplies_drillableTitanium, new Vector3(-4f, yOffset, 1f));
            SpawnPrefab(supplies_drillableTitanium, new Vector3(4f, yOffset, -6f));
            SpawnPrefab(supplies_drillableTitanium, new Vector3(1f, yOffset, 3f));
            SpawnPrefab(pedestal_ionCrystal, new Vector3(0f, yOffset, 0f));
            SpawnPrefab(pedestal_empty2, new Vector3(7f, yOffset, 7f));
            SpawnPrefab(pedestal_empty2, new Vector3(7f, yOffset, -9f));
            SpawnPrefab(pedestal_empty2, new Vector3(-9f, yOffset, 7f));
            SpawnPrefab(pedestal_empty2, new Vector3(-9f, yOffset, -9f));
            SpawnPrefab(Mod.tertiaryOutpostTerminal.ClassID, new Vector3(0f, yOffset, -5f), new Vector3(0f, 180f, 0f));
            SpawnPrefab(starfish, new Vector3(0f, yOffset, -6f));
            SpawnPrefab(starfish, new Vector3(-5f, yOffset, -4f));
            SpawnPrefab(starfish, new Vector3(3f, yOffset, 3f));
            SpawnPrefab(creature_alienRobot, new Vector3(3f, yOffset, 3f));
            SpawnPrefab(creature_alienRobot, new Vector3(-3f, yOffset, -3f));
            SpawnPrefab(structure_column, new Vector3(0, 0, 0));
            SpawnPrefab(prop_claw, new Vector3(0, -2, 0));
            const float smallLightYOffset = -4.3f;
            SpawnPrefab(light_small, new Vector3(7f, smallLightYOffset, 7f));
            SpawnPrefab(light_small, new Vector3(7f, smallLightYOffset, -9f));
            SpawnPrefab(light_small, new Vector3(-9f, smallLightYOffset, 7f));
            SpawnPrefab(light_small, new Vector3(-9f, smallLightYOffset, -9f));
            SpawnColumns(-16f);
        }

        protected void SpawnColumns(float yOffset)
        {
            SpawnPrefab(structure_column, new Vector3(7f, yOffset, 7f));
            SpawnPrefab(structure_column, new Vector3(7f, yOffset, -9f));
            SpawnPrefab(structure_column, new Vector3(-9f, yOffset, 7f));
            SpawnPrefab(structure_column, new Vector3(-9f, yOffset, -9f));
        }
    }
}
