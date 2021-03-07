using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class OutpostBaseSpawner : AlienBaseSpawner
    {
        public override void ConstructBase()
        {
            SpawnPrefab(outpost1, Vector3.zero);
            SpawnPrefab(Mod.outpostABTerminal.ClassID, Vector3.up * 10.5f);
        }
    }
}
