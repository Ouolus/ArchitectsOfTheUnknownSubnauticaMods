using UnityEngine;
using System.Collections;

namespace RotA.Mono.AlienBaseSpawners
{
    public class CablesNearGuardian : AlienBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            yield return StartCoroutine(GenerateCable(new Vector3(354, -384, -1768), Vector3.up, new Vector3(356, -348, -1756), new Vector3(-0.2f, 0, 1), Vector3.back, 10f)); //when you're facing the guardian from the void, this is the one on the left
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(356, -336, -1747)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(362, -335, -1740)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(371, -338, -1747)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(371, -341, -1753)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(366, -344, -1753)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(364, -338, -1746)));
            yield return StartCoroutine(SpawnPrefabGlobally(atmosphereVolume_cache, new Vector3(367, -333, -1747), Vector3.zero, Vector3.one * 30f));
            yield return StartCoroutine(SpawnPrefabGlobally(hugeExteriorCube, new Vector3(403, -316, -1722), Vector3.zero, Vector3.one * 274));
            yield return StartCoroutine(SpawnPrefabGlobally(hugeExteriorCube, new Vector3(329, -336, -1721), Vector3.zero, new Vector3(21.34f, 19.66f, 85.20f)));
        }
    }
}
