using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public abstract class SecondaryBaseSpawner : AlienBaseSpawner
    {
        public const float centerLocalX = 2.2f;
        public const float floorLocalY = -2.885f;
        public const float ceilingLocalY = 10.1f;
        public const float foundationUnderLocalY = -12.88f;


        public override void ConstructBase()
        {
            GameObject baseModel = SpawnPrefab(Mod.secondaryBaseModel.ClassID, Vector3.zero);
            GenerateAtmospheres(baseModel, "AtmosphereRoot", atmosphereVolume_antechamber);
            SpawnPrefab(airlock_1, new Vector3(centerLocalX, floorLocalY + 2.38f, 24f), Vector3.zero, new Vector3(0.76f, 1f, 1f));
            SpawnPrefab(vfx_entrance, new Vector3(centerLocalX, floorLocalY + 2.38f, 24f), new Vector3(90f, 0f, 0f), new Vector3(0.76f, 1f, 1f));
            Vector3 ceilingLightRotation = new Vector3(0f, 0f, 0f);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX, -3.17f, 16f), ceilingLightRotation);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX, -3.17f, 8f), ceilingLightRotation);
            SpawnPrefab(pedestal_ionCrystal, new Vector3(centerLocalX, floorLocalY, 0f), ceilingLightRotation);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX, -3.17f, -8), ceilingLightRotation);
            SpawnPrefab(TerminalClassId, new Vector3(centerLocalX, floorLocalY, -16f), new Vector3(0f, 180f, 0f));
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX + 10f, -3.17f, 0f), ceilingLightRotation);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX + 18f, -3.17f, 0f), ceilingLightRotation);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX - 10f, -3.17f, 0f), ceilingLightRotation);
            SpawnPrefab(light_big_animated, new Vector3(centerLocalX - 18f, -3.17f, 0f), ceilingLightRotation);
            SpawnPrefab(creature_alienRobot, new Vector3(centerLocalX + 5f, floorLocalY, 5f));
            SpawnPrefab(creature_alienRobot, new Vector3(centerLocalX - 5f, floorLocalY, 5f));
            SpawnPrefab(natural_lr_hangingplant1_2, new Vector3(2f, ceilingLocalY, 3f));
            SpawnPrefab(natural_lr_hangingplant1_1, new Vector3(-5f, ceilingLocalY, 6f));
            SpawnPrefab(natural_lr_hangingplant1_3, new Vector3(4f, ceilingLocalY, -5f));
            SpawnPrefab(natural_lr_hangingplant1_1, new Vector3(-6f, ceilingLocalY, -4f));
            SpawnPrefab(natural_lr_hangingplant1_2, new Vector3(1f, ceilingLocalY, 8f));
            SpawnPrefab(natural_lr_hangingplant1_1, new Vector3(3f, ceilingLocalY, 3.2f));
            SpawnPrefab(natural_lr_hangingplant1_3, new Vector3(-4f, ceilingLocalY, -1.5f));
            SpawnPrefab(natural_lr_hangingplant1_2, new Vector3(-5f, ceilingLocalY, -5f));

            SpawnPrefab(prop_tabletPedestal, new Vector3(centerLocalX, -3.17f, 17f), Vector3.zero, Vector3.one * 0.5f);
            SpawnPrefab(TabletClassId, new Vector3(centerLocalX, floorLocalY + 1.07f, 17f), new Vector3(22.5f, 0f, 0f));
        }

        protected virtual string TabletClassId
        {
            get
            {
                return supplies_whiteTablet;
            }
        }

        protected abstract string TerminalClassId
        {
            get;
        }
    }
}
