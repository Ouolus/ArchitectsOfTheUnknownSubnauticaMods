using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class TowerOutpostSpawner : OutpostBaseSpawner
    {
        public override void ConstructBase()
        {
            base.ConstructBase();
            SpawnColumns(-24f);
            SpawnColumns(-32f);
            SpawnColumns(-40f);
            GenerateCable(new Vector3(-527, -49, 28), Vector3.forward, new Vector3(-546.6f, -61, 50.6f), Vector3.down, Vector3.down, 15f);
            GenerateCable(new Vector3(-526, -49, 16), Vector3.back, new Vector3(-516, -79, -30), Vector3.down, Vector3.down, 15f);
        }
    }
}
