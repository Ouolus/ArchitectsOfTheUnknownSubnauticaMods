using UnityEngine;

namespace ProjectAncients.Mono.AlienBaseSpawners
{
    public class BonesFieldsOutpostSpawner : OutpostBaseSpawner
    {
        public override void ConstructBase()
        {
            base.ConstructBase();
            GenerateCable(new Vector3(-725.3f, -746.2f, -217.6f), Vector3.up, new Vector3(-708.3f, -699.5f, -215.5f), new Vector3(0.7f, 0.7f, 0.2f), Vector3.left, 10f);
        }
    }
}
