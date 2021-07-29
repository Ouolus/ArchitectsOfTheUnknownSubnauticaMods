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

            yield return SpawnPrefab(Mod.devSecretTerminal.ClassID, new Vector3(0, 0, -49), Vector3.up * 180f);

            yield return SpawnPrefab(Mod.devTerminalAlan.ClassID, new Vector3(13, 0, -11), Vector3.up * 90);
            yield return SpawnPrefab(Mod.devTerminalHipnox.ClassID, new Vector3(13, 0, -25), Vector3.up * 90);
            yield return SpawnPrefab(Mod.devTerminalLee23.ClassID, new Vector3(13, 0, -35), Vector3.up * 90);
            yield return SpawnPrefab(Mod.devTerminalMetious.ClassID, new Vector3(-13, 0, -11), Vector3.up * -90);
            yield return SpawnPrefab(Mod.devTerminalN8crafter.ClassID, new Vector3(-13, 0, -25), Vector3.up * -90);
            yield return SpawnPrefab(Mod.devTerminalSlendyPlayz.ClassID, new Vector3(-13, 0, -35), Vector3.up * -90);

            yield return SpawnPrefab(Mod.devTerminalTori.ClassID, new Vector3(0f, 0, -25), Vector3.up * 180f);
        }
    }
}
