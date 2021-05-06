using UnityEngine;
using System.Collections;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public abstract class SecondaryBaseSpawner : AlienBaseSpawner
    {
        public const float centerLocalX = 2.2f;
        public const float floorLocalY = -2.885f;
        public const float ceilingLocalY = 10.1f;
        public const float foundationUnderLocalY = -12.88f;


        public override IEnumerator ConstructBase()
        {
            TaskResult<GameObject> baseModel = new TaskResult<GameObject>();
            yield return SpawnPrefab(Mod.secondaryBaseModel.ClassID, Vector3.zero, baseModel);
            yield return StartCoroutine(GenerateAtmospheres(baseModel.Get(), "AtmosphereRoot", Mod.precursorAtmosphereVolume.ClassID));
            yield return StartCoroutine(SpawnPrefab(airlock_1, new Vector3(centerLocalX, floorLocalY + 2.38f, 24f), Vector3.zero, new Vector3(0.76f, 1f, 1f)));
            yield return StartCoroutine(SpawnPrefab(vfx_entrance, new Vector3(centerLocalX, floorLocalY + 2.38f, 24f), new Vector3(90f, 0f, 0f), new Vector3(0.76f, 1f, 1f)));
            Vector3 floorLightRotation = new Vector3(0f, 0f, 0f);
            yield return StartCoroutine(SpawnPrefab(light_big_animated, new Vector3(centerLocalX, -3.17f, 16f), floorLightRotation));
            yield return StartCoroutine(SpawnPrefab(light_big_animated, new Vector3(centerLocalX - 3f, -3.17f, 3f), floorLightRotation));
            yield return StartCoroutine(SpawnPrefab(light_big_animated, new Vector3(centerLocalX + 3f, -3.17f, 3f), floorLightRotation));
            yield return StartCoroutine(SpawnPrefab(MainTerminalClassId, new Vector3(centerLocalX, floorLocalY, MainTerminalZOffset), new Vector3(0f, 180f, 0f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX - 3f, floorLocalY, MainTerminalZOffset), Vector3.zero, new Vector3(1f, 1.7f, 1f)));
            yield return StartCoroutine(SpawnPrefab(structure_column, new Vector3(centerLocalX + 3f, floorLocalY, MainTerminalZOffset), Vector3.zero, new Vector3(1f, 1.7f, 1f)));
            yield return StartCoroutine(SpawnPrefab(light_big_animated, new Vector3(centerLocalX + 10f, -3.17f, 0f), floorLightRotation));
            yield return StartCoroutine(SpawnPrefab(light_big_animated, new Vector3(centerLocalX + 18f, -3.17f, 0f), floorLightRotation));
            yield return StartCoroutine(SpawnPrefab(light_big_animated, new Vector3(centerLocalX - 10f, -3.17f, 0f), floorLightRotation));
            yield return StartCoroutine(SpawnPrefab(light_big_animated, new Vector3(centerLocalX - 18f, -3.17f, 0f), floorLightRotation));
            yield return StartCoroutine(SpawnPrefab(creature_alienRobot, new Vector3(centerLocalX + 5f, floorLocalY, 5f)));
            yield return StartCoroutine(SpawnPrefab(creature_alienRobot, new Vector3(centerLocalX - 5f, floorLocalY, 5f)));
            yield return StartCoroutine(SpawnPrefab(natural_lr_hangingplant1_2, new Vector3(2f, ceilingLocalY, 3f)));
            yield return StartCoroutine(SpawnPrefab(natural_lr_hangingplant1_1, new Vector3(-5f, ceilingLocalY, 6f)));
            yield return StartCoroutine(SpawnPrefab(natural_lr_hangingplant1_3, new Vector3(4f, ceilingLocalY, -5f)));
            yield return StartCoroutine(SpawnPrefab(natural_lr_hangingplant1_1, new Vector3(-6f, ceilingLocalY, -4f)));
            yield return StartCoroutine(SpawnPrefab(natural_lr_hangingplant1_2, new Vector3(1f, ceilingLocalY, 8f)));
            yield return StartCoroutine(SpawnPrefab(natural_lr_hangingplant1_1, new Vector3(3f, ceilingLocalY, 3.2f)));
            yield return StartCoroutine(SpawnPrefab(natural_lr_hangingplant1_3, new Vector3(-4f, ceilingLocalY, -1.5f)));
            yield return StartCoroutine(SpawnPrefab(natural_lr_hangingplant1_2, new Vector3(-5f, ceilingLocalY, -5f)));

            yield return StartCoroutine(SpawnPrefab(prop_tabletPedestal, new Vector3(centerLocalX, -3.17f, 17f), Vector3.zero, Vector3.one * 0.5f));
            yield return StartCoroutine(SpawnPrefab(TabletClassId, new Vector3(centerLocalX, floorLocalY + 1.07f, 17f), new Vector3(22.5f, 0f, 0f)));
        }

        protected virtual string TabletClassId
        {
            get
            {
                return supplies_whiteTablet;
            }
        }

        protected abstract string MainTerminalClassId
        {
            get;
        }

        protected virtual float MainTerminalZOffset
        {
            get
            {
                return 0f;
            }
        }
    }
}
