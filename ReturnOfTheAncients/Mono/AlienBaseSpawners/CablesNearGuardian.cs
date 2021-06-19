using UnityEngine;
using System.Collections;

namespace RotA.Mono.AlienBaseSpawners
{
    public class CablesNearGuardian : AlienBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            yield return StartCoroutine(GenerateCable(new Vector3(354, -384, -1768), Vector3.up, new Vector3(356, -348, -1756), new Vector3(-0.2f, 0, 1), Vector3.back, 10f)); //when you're facing the guardian from the void, this is the one on the left
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(378, -352, -1750)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(370, -338, -1746)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(384, -332, -1736.5f)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(366, -327, -1731)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(363, -318, -1723)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(357, -312, -1715)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(356, -322, -1729)));
            yield return StartCoroutine(SpawnPrefabGlobally(atmosphereVolume_cache, new Vector3(367, -333, -1747), Vector3.zero, Vector3.one * 30f));
            yield return StartCoroutine(SpawnPrefabGlobally(hugeExteriorCube, new Vector3(403, -316, -1722), Vector3.zero, Vector3.one * 274));
            yield return StartCoroutine(SpawnPrefabGlobally(hugeExteriorCube, new Vector3(329, -336, -1721), Vector3.zero, new Vector3(21.34f, 19.66f, 85.20f)));
        }
    }
}
