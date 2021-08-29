using System.Collections;
using UnityEngine;

namespace RotA.Mono.AlienBaseSpawners
{
    public class CacheBaseExteriorSpawner : AlienBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            TaskResult<GameObject> baseModel = new TaskResult<GameObject>();
            yield return SpawnPrefab(Mod.secondaryBaseModel.ClassID, Vector3.zero, baseModel);
            yield return StartCoroutine(GenerateAtmospheres(baseModel.Get(), "AtmosphereRoot", Mod.precursorAtmosphereVolume.ClassID));
        }
    }
}
