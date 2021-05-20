using UnityEngine;
using System.Collections;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class TowerOutpostSpawner : OutpostBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            yield return StartCoroutine(base.ConstructBase());
            yield return StartCoroutine(SpawnColumns(-24f));
            yield return StartCoroutine(SpawnColumns(-32f));
            yield return StartCoroutine(SpawnColumns(-40f));
        }

        public override string TerminalClassId => Mod.tertiaryOutpostTerminalGrassy.ClassID;
    }
}
