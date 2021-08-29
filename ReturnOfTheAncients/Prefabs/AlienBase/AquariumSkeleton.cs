using ArchitectsLibrary.API;
using ECCLibrary;
using UnityEngine;

namespace RotA.Prefabs.AlienBase
{
    public class AquariumSkeleton : GenericWorldPrefab
    {
        public AquariumSkeleton(string classId, GameObject model, UBERMaterialProperties materialProperties, LargeWorldEntity.CellLevel cellLevel, bool applyPrecursorMaterial = true) : base(classId, LanguageSystem.Get("VoidbaseAquariumSkeleton"), LanguageSystem.Default, model, materialProperties, cellLevel, applyPrecursorMaterial)
        {
        }

        public override void CustomizePrefab()
        {
            foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>())
            {
                foreach (Material material in renderer.materials)
                {
                    if (renderer.gameObject.name == "Cylinder")
                    {
                        material.SetColor("_Color1", new Color(0.2079127f, 0.1599204f, 0.1142356f));
                    }
                    else
                    {
                        material.SetColor("_Color1", new Color(0.8f, 0.6509804f, 0.4352941f));
                    }
                }
            }
        }
    }
}
