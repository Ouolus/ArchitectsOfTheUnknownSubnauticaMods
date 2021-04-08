using ECCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class VoidBaseModel : GenericWorldPrefab
    {
        public VoidBaseModel(string classId, string friendlyName, string description, GameObject model, UBERMaterialProperties materialProperties, LargeWorldEntity.CellLevel cellLevel) : base(classId, friendlyName, description, model, materialProperties, cellLevel)
        {
        }

        public override void CustomizePrefab()
        {
            
        }
    }
}
