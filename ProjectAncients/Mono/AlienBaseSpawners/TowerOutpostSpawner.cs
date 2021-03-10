using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
