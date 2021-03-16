using ECCLibrary;
using SMLHelper.V2.Assets;
using UnityEngine;

namespace ProjectAncients.Prefabs
{
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
                ECCHelpers.ApplySNShaders(prefab, materialProperties);
                foreach(Collider col in prefab.GetComponentsInChildren<Collider>())
                {
                    col.gameObject.AddComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.metal;
                }
            }
            return prefab;
        }
    }
}
