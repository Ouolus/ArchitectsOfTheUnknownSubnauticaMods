using SMLHelper.V2.Crafting;
using UnityEngine;
using System.Collections.Generic;
using ArchitectsLibrary.API;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.MonoBehaviours;
using SMLHelper.V2.Handlers;

namespace ArchitectsLibrary.Buildables
{
    class BuildableSonicDeterrent : GenericPrecursorDecoration
    {
        public BuildableSonicDeterrent() : base("BuildableSonicDeterrent", "Sonic Deterrent", "A large alien object that wards off fauna. Most effective against larger fauna.")
        {
            AUHandler.BuildableSonicDeterrentTechType = TechType;
            
            KnownTechHandler.SetAnalysisTechEntry(TechType, new TechType[0], 
                UnlockSprite: Main.assetBundle.LoadAsset<Sprite>("SonicDeterrent_Popup"));
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(false, false, true, true, true, true, true, placeDefaultDistance: 8f, placeMinDistance: 5f, placeMaxDistance: 15f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 6f, 0f), Quaternion.identity, new Vector3(2f, 2.8f, 2f)) };

        protected override string GetOriginalClassId => "c5512e00-9959-4f57-98ae-9a9962976eaa";

        protected override bool ExteriorOnly => true;

        public override TechType RequiredForUnlock => TechType.None;

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.transform.GetChild(0).transform.localPosition = new Vector3(0f, 5f, 0f);
            prefab.transform.GetChild(0).transform.localScale = Vector3.one * 0.75f;
            prefab.EnsureComponent<SonicDeterrentDeterCreatures>();
        }

        protected override string GetSpriteName => "SonicDeterrent";

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData() { Ingredients = new List<Ingredient>() { new Ingredient(AUHandler.PrecursorAlloyIngotTechType, 2), new Ingredient(AUHandler.RedIonCubeTechType, 1) } };
        }
    }
}
