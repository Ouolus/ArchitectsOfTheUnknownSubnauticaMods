using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class SecondaryBaseSpawner : AlienBaseSpawner
    {
        public const float centerLocalX = 2.2f;
        public const float floorLocalY = -2.885f;
        public const float ceilingLocalY = 10.1f;
        public const float foundationUnderLocalY = -12.88f;


        public override void ConstructBase()
        {
            GameObject baseModel = SpawnPrefab(Mod.secondaryBaseModel.ClassID, Vector3.zero);
            GenerateAtmospheres(baseModel, "AtmosphereRoot", atmosphereVolume_cache);
            SpawnPrefab(entry_1, new Vector3(centerLocalX, floorLocalY + 2.38f, 24f), Vector3.zero, new Vector3(0.76f, 1f, 1f));
            SpawnPrefab(vfx_entrance, new Vector3(centerLocalX, floorLocalY + 2.38f, 24f), new Vector3(90f, 0f, 0f), new Vector3(0.76f, 1f, 1f));
            Vector3 ceilingLightRotation = new Vector3(0f, 0f, 0f);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX, -3.17f, 16f), ceilingLightRotation);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX, -3.17f, 8f), ceilingLightRotation);
            SpawnPrefab(pedestal_ionCrystal, new Vector3(centerLocalX, floorLocalY, 0f), ceilingLightRotation);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX, -3.17f, -8), ceilingLightRotation);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX, -3.17f, -16f), ceilingLightRotation);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX + 10f, -3.17f, 0f), ceilingLightRotation);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX + 18f, -3.17f, 0f), ceilingLightRotation);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX - 10f, -3.17f, 0f), ceilingLightRotation);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX - 18f, -3.17f, 0f), ceilingLightRotation);
            SpawnPrefab(creature_alienRobot, new Vector3(centerLocalX + 5f, floorLocalY, 5f));
            SpawnPrefab(creature_alienRobot, new Vector3(centerLocalX - 5f, floorLocalY, 5f));
        }
    }
}
