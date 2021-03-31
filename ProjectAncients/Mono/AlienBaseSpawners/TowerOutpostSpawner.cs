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
            yield return StartCoroutine(GenerateCable(new Vector3(-527, -49, 28), new Vector3(0.4f, 0f, 0.9f), new Vector3(-546.6f, -62, 50.6f), Vector3.down, Vector3.up, 15f));
            yield return StartCoroutine(GenerateCable(new Vector3(-526, -49, 16), new Vector3(-0.4f, 0f, -0.9f), new Vector3(-516, -79, -30), Vector3.down, Vector3.down, 15f));
        }
    }
}
