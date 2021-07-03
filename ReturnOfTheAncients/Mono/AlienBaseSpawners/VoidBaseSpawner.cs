using System.Collections;
using UnityEngine;

namespace RotA.Mono.AlienBaseSpawners
{
    public class VoidBaseSpawner : AlienBaseSpawner
    {
        private const float platformY = 0f;

        public override IEnumerator ConstructBase()
        {
            TaskResult<GameObject> baseModel = new TaskResult<GameObject>();
            yield return SpawnPrefab(Mod.voidBaseModel.ClassID, Vector3.zero, baseModel);
            yield return GenerateAtmospheres(baseModel.Get(), "AtmosphereRoot", Mod.precursorAtmosphereVolume.ClassID);

            //Exterior platform
            yield return SpawnPrefab(Mod.voidDoor_red.ClassID, new Vector3(0f, platformY, 62f));
            yield return SpawnPrefab(Mod.voidDoor_orange.ClassID, new Vector3(0f, platformY, 62f));
            yield return SpawnPrefab(Mod.voidDoor_white.ClassID, new Vector3(0f, platformY, 62f));
            yield return SpawnPrefab(Mod.voidDoor_blue.ClassID, new Vector3(0f, platformY, 62f));
            yield return SpawnPrefab(Mod.voidDoor_purple.ClassID, new Vector3(0f, platformY, 62f));
            yield return SpawnPrefab(vfx_entrance, new Vector3(-0.5f, 3f + platformY, 62f), new Vector3(90, 0, 0), new Vector3(1.30f, 1.5f, 1.30f));
            yield return SpawnPrefab(light_volumetric_1, new Vector3(0f, 11, 62.5f), Vector3.zero, new Vector3(1f, 0.5f, 1f));
            yield return SpawnPrefab(light_volumetric_1, new Vector3(0f, 21, 62.5f), Vector3.zero, new Vector3(1f, 2f, 1f));
            yield return SpawnPrefab(light_volumetric_1, new Vector3(0f, 32, 62.5f), Vector3.zero, new Vector3(1f, 0.5f, 1f));
        }
    }
}
