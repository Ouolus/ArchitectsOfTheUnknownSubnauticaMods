using SMLHelper.V2.Assets;
using UnityEngine;

namespace ArchitectsLibrary.Items.VanillaPrefabPatching
{
    class PrefabPatching : ModPrefab
    {
        public PrefabPatching(string classId, string prefabFileName, TechType techType)
            : base(classId, prefabFileName, techType)
        {}

        // dont override the object's settings.
        protected sealed override void ProcessPrefab(GameObject go)
        {
            
        }
    }
}