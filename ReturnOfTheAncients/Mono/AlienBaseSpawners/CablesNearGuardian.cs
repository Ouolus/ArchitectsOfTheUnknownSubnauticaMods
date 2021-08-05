using System.Collections;
using UnityEngine;

namespace RotA.Mono.AlienBaseSpawners
{
    public class CablesNearGuardian : AlienBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            yield return StartCoroutine(GenerateCable(new Vector3(354, -430, -1780), Vector3.up, new Vector3(356, -348, -1756), new Vector3(-0.2f, 0, 1), Vector3.back, 10f, 3f, false)); //when you're facing the guardian from the void, this is the one on the left
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(378, -352, -1750)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(370, -338, -1746)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(384, -332, -1736.5f)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(366, -327, -1731)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(363, -318, -1723)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(357, -312, -1715)));
            yield return StartCoroutine(SpawnPrefabGlobally(ambience_greenLight, new Vector3(356, -322, -1729)));
            yield return StartCoroutine(SpawnPrefabGlobally(hugeExteriorCube, new Vector3(403, -316, -1722), Vector3.up * 274, Vector3.one));
            yield return StartCoroutine(SpawnPrefabGlobally(hugeExteriorCube, new Vector3(329, -336, -1721), new Vector3(21.34f, 19.66f, 85.20f), Vector3.one));
            yield return StartCoroutine(SpawnPrefabGlobally(CraftData.GetClassIdForTechType(ArchitectsLibrary.Handlers.AUHandler.PrecursorAlloyIngotTechType), new Vector3(340.23f, -320.71f, -1710.09f), new Vector3(357.36f, 358.90f, 45.27f), new Vector3(2f, 2f, 2f)));
        }
    }
}
