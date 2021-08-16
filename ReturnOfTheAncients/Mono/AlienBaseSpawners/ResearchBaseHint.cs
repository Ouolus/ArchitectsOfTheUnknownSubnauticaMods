using System.Collections;
using UnityEngine;

namespace RotA.Mono.AlienBaseSpawners
{
    public class ResearchBaseHint : AlienBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            yield return SpawnPrefabGlobally(structure_skeletonScanner1, new Vector3(-621.77f, -191.72f, -617.81f), new Vector3(0f, 85.71f, 0f), Vector3.one);
            yield return SpawnPrefabGlobally(light_big_ceiling_animated, new Vector3(-616f, -174.58f, -605.33f), new Vector3(0f, 17.14f, 0f), Vector3.one * 2f);
            yield return SpawnPrefabGlobally(light_big_ceiling_animated, new Vector3(-614.25f, -169.50f, -588.17f), new Vector3(0f, 137.14f, 0f), Vector3.one * 2f);
        }
    }
}
