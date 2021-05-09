using SMLHelper.V2.Assets;
using SMLHelper.V2.Utility;
using UnityEngine;
using System.Collections.Generic;
using UWE;

namespace ArchitectsLibrary.Items
{
    class DrillableEmerald : ReskinItem
    {
        protected override string ReferenceClassId => "4f441e53-7a9a-44dc-83a4-b1791dc88ffd";

        public DrillableEmerald() : base("DrillableEmerald", "Emerald", "Be₃Al₂SiO₆. Beryl variant with applications in advanced alien fabrication.")
        {
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            
        }
    }
}
