using ECCLibrary;
using SMLHelper.V2.Assets;
using UnityEngine;

namespace ProjectAncients.Prefabs
{
    /// <summary>
    /// Made for Precursor base structures
    /// </summary>
    public class GenericWorldPrefab : Spawnable
    {
        private GameObject model;
        private GameObject prefab;
        private UBERMaterialProperties materialProperties;
        private LargeWorldEntity.CellLevel cellLevel;

        public GenericWorldPrefab(string classId, string friendlyName, string description, GameObject model, UBERMaterialProperties materialProperties, LargeWorldEntity.CellLevel cellLevel) : base(classId, friendlyName, description)
        {
            this.model = model;
            this.materialProperties = materialProperties;
            this.cellLevel = cellLevel;
        }

        public override GameObject GetGameObject()
        {
            if(prefab == null)
            {
                prefab = GameObject.Instantiate(model);
                prefab.SetActive(false);
                prefab.EnsureComponent<LargeWorldEntity>().cellLevel = cellLevel;
                prefab.EnsureComponent<PrefabIdentifier>().classId = ClassID;
                prefab.EnsureComponent<TechTag>().type = TechType;
                prefab.EnsureComponent<SkyApplier>().renderers = prefab.GetComponentsInChildren<Renderer>();
                ECCHelpers.ApplySNShaders(prefab, materialProperties);
                foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>())
                {
                    Material material = renderer.material;
                    material.SetColor("_SpecColor", new Color(0.25f, 0.54f, 0.41f));
                    material.SetFloat("_SpecInt", 8f);
                    material.SetFloat("_Fresnel", 0.4f);
                    if(ECCHelpers.CompareStrings(material.name, "Transparent", ECCStringComparison.Contains))
                    {
                        material.SetFloat("_SrcBlend", 5f);
                        material.SetFloat("_DstBlend", 10f);
                        material.SetFloat("_SrcBlend2", 5f);
                        material.SetFloat("_DstBlend2", 10f);
                        material.SetFloat("_AddSrcBlend", 5f);
                        material.SetFloat("_AddDstBlend", 1f);
                        material.SetFloat("_AddSrcBlend2", 5f);
                        material.SetFloat("_AddDstBlend2", 1f);
                        material.SetInt("_ZWrite", 0);
                        material.EnableKeyword("MARMO_SIMPLE_GLASS");
                    }
                }
                foreach(Collider col in prefab.GetComponentsInChildren<Collider>())
                {
                    col.gameObject.AddComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.metal;
                }
            }
            return prefab;
        }
    }
}
