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
            GenerateCable(new Vector3(-527, -49, 28), new Vector3(0.4f, 0f, 0.9f), new Vector3(-546.6f, -62, 50.6f), Vector3.down, Vector3.up, 15f);
            GenerateCable(new Vector3(-526, -49, 16), new Vector3(-0.4f, 0f, -0.9f), new Vector3(-516, -79, -30), Vector3.down, Vector3.down, 15f);
            GenerateCable(transform.position + new Vector3(0f, 6f, 0f), Vector3.up, transform.position + new Vector3(0f, 400f, 150f), Vector3.up, Vector3.down, 12f);
        }
    }
}
