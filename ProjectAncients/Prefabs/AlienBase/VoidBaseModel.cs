using ECCLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ProjectAncients.Mono.AlienTech;
using Story;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class VoidBaseModel : GenericWorldPrefab
    {
        public VoidBaseModel(string classId, string friendlyName, string description, GameObject model, UBERMaterialProperties materialProperties, LargeWorldEntity.CellLevel cellLevel) : base(classId, friendlyName, description, model, materialProperties, cellLevel)
        {
        }


        public override void CustomizePrefab()
        {
            prefab.EnsureComponent<VoidBaseReveal>();
            foreach(Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
            {
                foreach(Material mat in renderer.materials)
                {
                    mat.SetColor("_Color", new Color(0.3f, 0.3f, 0.3f));
                }
            }
        }
    }
}
