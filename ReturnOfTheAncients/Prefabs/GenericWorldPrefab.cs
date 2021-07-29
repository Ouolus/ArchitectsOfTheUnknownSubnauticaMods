using ArchitectsLibrary.Utility;
using ECCLibrary;
using SMLHelper.V2.Assets;
using System.Collections;
using UnityEngine;

namespace RotA.Prefabs
{
    /// <summary>
    /// Made for Precursor base structures
    /// </summary>
    public class GenericWorldPrefab : Spawnable
    {
        private GameObject model;
        protected GameObject prefab;
        private UBERMaterialProperties materialProperties;
        private LargeWorldEntity.CellLevel cellLevel;
        private bool applyPrecursorMaterial;

        public GenericWorldPrefab(string classId, string friendlyName, string description, GameObject model, UBERMaterialProperties materialProperties, LargeWorldEntity.CellLevel cellLevel, bool applyPrecursorMaterial = true) : base(classId, friendlyName, description)
        {
            this.model = model;
            this.materialProperties = materialProperties;
            this.cellLevel = cellLevel;
            this.applyPrecursorMaterial = applyPrecursorMaterial;
        }

        public virtual void CustomizePrefab()
        {

        }

        public override GameObject GetGameObject()
        {
            if (prefab == null)
            {
                prefab = GameObject.Instantiate(model);
                prefab.SetActive(false);
                prefab.EnsureComponent<LargeWorldEntity>().cellLevel = cellLevel;
                prefab.EnsureComponent<PrefabIdentifier>().classId = ClassID;
                prefab.EnsureComponent<TechTag>().type = TechType;
                prefab.EnsureComponent<SkyApplier>().renderers = prefab.GetComponentsInChildren<Renderer>();
                var rb = prefab.EnsureComponent<Rigidbody>();
                rb.mass = 10000f;
                rb.isKinematic = true;
                prefab.EnsureComponent<ImmuneToPropulsioncannon>();
                ECCHelpers.ApplySNShaders(prefab, materialProperties);
                if (applyPrecursorMaterial)
                {
                    MaterialUtils.ApplyPrecursorMaterials(prefab, materialProperties.SpecularInt);
                }
                CustomizePrefab();
                foreach (Collider col in prefab.GetComponentsInChildren<Collider>())
                {
                    col.gameObject.EnsureComponent<ConstructionObstacle>();
                }
            }
            return prefab;
        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            gameObject.Set(GetGameObject());
            yield break;
        }
    }
}
