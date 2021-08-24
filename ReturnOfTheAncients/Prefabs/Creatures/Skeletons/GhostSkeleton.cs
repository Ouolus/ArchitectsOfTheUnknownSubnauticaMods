using ArchitectsLibrary.API;
using ArchitectsLibrary.Utility;
using UnityEngine;

namespace RotA.Prefabs.Creatures.Skeletons
{
    using SMLHelper.V2.Assets;

    public abstract class GhostSkeleton : Spawnable
    {
        protected GhostSkeleton(string classId) : base(classId, LanguageSystem.Get("GhostLeviathanSkeleton"), LanguageSystem.GetTooltip("GhostLeviathanSkeleton"))
        {
        }
        
        protected abstract string ModelName { get; }
        
        public override GameObject GetGameObject()
        {
            var model = Mod.assetBundle.LoadAsset<GameObject>(ModelName);
            var prefab = Object.Instantiate(model);
            prefab.SetActive(false);
            MaterialUtils.ApplySNShaders(prefab, 6f);
            prefab.AddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Far;
            prefab.AddComponent<SkyApplier>().renderers = prefab.GetComponentsInChildren<Renderer>();
            prefab.AddComponent<TechTag>().type = TechType;
            prefab.AddComponent<PrefabIdentifier>().ClassId = ClassID;
            prefab.AddComponent<ConstructionObstacle>();
            var rb = prefab.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.mass = 1000f;
            return prefab;
        }
    }
}