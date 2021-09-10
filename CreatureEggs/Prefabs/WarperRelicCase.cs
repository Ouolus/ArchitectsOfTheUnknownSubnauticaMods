using ArchitectsLibrary.Utility;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;
using static CreatureEggs.Helpers.AssetsBundleHelper;

namespace CreatureEggs.Prefabs
{
    class WarperRelicCase : Spawnable
    {
        private const string kRelicCase = "d0fea4da-39f2-47b4-aece-bb12fe7f9410";
        
        public WarperRelicCase() 
            : base("WarperRelicCase", "Warper Relic Case", "Warper Relic Case that makes me go yes.")
        {}

        public override GameObject GetGameObject()
        {
            var model = LoadGameObject("WarperRelicCase");
            PrefabDatabase.TryGetPrefab(kRelicCase, out var obj);
            var prefab = Object.Instantiate(model);
            MaterialUtils.ApplySNShaders(prefab);
            Object.Instantiate(obj, prefab.transform);

            return prefab;
        }
    }
}