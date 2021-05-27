using ECCLibrary;
using UnityEngine;
using RotA.Mono.AlienTech;

namespace RotA.Prefabs.AlienBase
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
                    if (!mat.name.ToLower().Contains("transparent") && !mat.name.ToLower().Contains("glass") && !mat.name.ToLower().Contains("tiles"))
                    {
                        mat.SetColor("_Color", new Color(0.3f, 0.3f, 0.3f));
                    }
                }
            }
            var deter = prefab.EnsureComponent<ArchitectsLibrary.MonoBehaviours.SonicDeterrentDeterCreatures>();
            deter.aggressiveFishDeterRadius = 175f;
            deter.maxDeterRadius = 175f;
            deter.smallFishDeterRadius = 0f;
        }
    }
}
