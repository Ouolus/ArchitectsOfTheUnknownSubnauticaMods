using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class CablesNearGuardian : AlienBaseSpawner
    {
        public override void ConstructBase()
        {
            GenerateCable(new Vector3(354, -384, -1768), Vector3.up, new Vector3(356, -348, -1756), new Vector3(-0.2f, 0, 1), Vector3.back, 10f); //when you're facing the guardian from the void, this is the one on the left
            GenerateCable(new Vector3(382, -386, -1759), Vector3.up, new Vector3(376, -351, -1748), new Vector3(-0.5f, 0.1f, 0.9f), Vector3.back, 10f); //one on the right
            SpawnPrefabGlobally(ambience_greenLight, new Vector3(356, -336, -1747));
            SpawnPrefabGlobally(ambience_greenLight, new Vector3(362, -335, -1740));
            SpawnPrefabGlobally(ambience_greenLight, new Vector3(371, -338, -1747));
            SpawnPrefabGlobally(ambience_greenLight, new Vector3(371, -341, -1753));
            SpawnPrefabGlobally(ambience_greenLight, new Vector3(366, -344, -1753));
            SpawnPrefabGlobally(ambience_greenLight, new Vector3(364, -338, -1746));
            SpawnPrefabGlobally(atmosphereVolume_cache, new Vector3(367, -333, -1747), Vector3.zero, Vector3.one * 30f);
        }
    }
}
