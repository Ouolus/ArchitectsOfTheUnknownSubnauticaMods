using UnityEngine;
using System.Collections;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class BonesFieldsOutpostSpawner : OutpostBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            yield return StartCoroutine(base.ConstructBase());
            GenerateCable(new Vector3(-725.3f, -746.2f, -217.6f), Vector3.up, new Vector3(-708.3f, -699.5f, -215.5f), new Vector3(0.7f, 0.7f, 0.2f), Vector3.left, 10f, 2f);
            yield return StartCoroutine(SpawnPrefab(supplies_orangeTablet, new Vector3(4f, -7.2f, 3f), Vector3.up * 36f));
        }
    }
}
