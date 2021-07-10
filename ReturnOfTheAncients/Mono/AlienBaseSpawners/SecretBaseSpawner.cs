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

            yield return SpawnPrefab(Mod.precursorAtmosphereVolume.ClassID, new Vector3(0f, 3f, -30f), Vector3.zero, new Vector3(50f, 50f, 70f));

            yield return SpawnPrefab(Mod.devSecretTerminal.ClassID, new Vector3(0f, 0f, -49), Vector3.zero, Vector3.up * 180f);
        }
    }
}
