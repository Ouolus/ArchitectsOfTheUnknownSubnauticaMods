using ArchitectsLibrary.Handlers;
using System.Collections;
using UnityEngine;

namespace RotA.Mono.AlienBaseSpawners
{
    public class SecretBaseSpawner : AlienBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            yield return SpawnPrefab(Mod.secretBaseModel.ClassID, new Vector3(0f, 0f, 0f));
        }
    }
}
