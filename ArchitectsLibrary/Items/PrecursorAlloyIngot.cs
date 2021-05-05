using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ArchitectsLibrary.Utility;

namespace ArchitectsLibrary.Items
{
    public class PrecursorAlloyIngot : Craftable
    {
        private GameObject prefab;

        public PrecursorAlloyIngot() : base("PrecursorIngot", "Precursor Alloy Ingot", "An alien resource with mysterious properties and unprecedented integrity.")
        {
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(TechType.TitaniumIngot, 1), new Ingredient(TechType.PrecursorIonCrystal, 2) });
        }

        public override bool UnlockedAtStart => false;

        public override GameObject GetGameObject()
        {
            if(prefab == null)
            {
                prefab = GameObject.Instantiate(Main.assetBundle.LoadAsset<GameObject>("PrecursorIngot_Prefab"));
                prefab.SetActive(false);

                prefab.AddComponent<TechTag>().type = TechType;
                prefab.AddComponent<PrefabIdentifier>().ClassId = ClassID;
                prefab.AddComponent<Pickupable>();
                prefab.AddComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.metal;
                MaterialUtils.ApplySNShaders(prefab);
                MaterialUtils.ApplyPrecursorMaterials(prefab, 5);
            }
            return prefab;
        }
    }
}
