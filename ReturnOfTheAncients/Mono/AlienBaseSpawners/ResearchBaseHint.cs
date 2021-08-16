using System.Collections;
using UnityEngine;

namespace RotA.Mono.AlienBaseSpawners
{
    public class ResearchBaseHint : AlienBaseSpawner
    {
        public override IEnumerator ConstructBase()
        {
            yield return SpawnPrefabGlobally(structure_skeletonScanner1, new Vector3(-621.77f, -191.72f, -617.81f), new Vector3(0f, 85.71f, 0f), Vector3.one);
            yield return SpawnPrefabGlobally(structure_skeletonScanner3, new Vector3(-693.91f, -211.09f, -641.29f), new Vector3(-5.91f, -3.09f, 14.71f), Vector3.one * 3f);
            yield return SpawnPrefab(structure_skeletonScanner2, new Vector3(-735.43f, -202.50f, -625.30f), new Vector3(0f, 205.7f, 17.14f), Vector3.one * 3f);
            yield return SpawnPrefabGlobally(light_big_ceiling_animated, new Vector3(-616f, -174.58f, -605.33f), new Vector3(0f, 17.14f, 0f), Vector3.one * 2f);
            yield return SpawnPrefabGlobally(light_big_ceiling_animated, new Vector3(-614.25f, -169.50f, -588.17f), new Vector3(0f, 137.14f, 0f), Vector3.one * 2f);
        }
    }
}
