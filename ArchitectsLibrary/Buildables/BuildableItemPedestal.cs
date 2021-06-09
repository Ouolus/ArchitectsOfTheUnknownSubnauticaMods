using SMLHelper.V2.Crafting;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.Utility;
using UWE;

namespace ArchitectsLibrary.Buildables
{
    class BuildableItemPedestal : GenericPrecursorDecoration
    {
        public BuildableItemPedestal() : base("BuildableItemPedestal", "Empty Item Pedestal", "A large pedestal that can be used to display a single item. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(1f, 0.4f, 1f)) };

        protected override string GetOriginalClassId => "ea65ef91-e875-4157-99f9-a8f4f6dc92f8";

        protected override bool ExteriorOnly => false;

        protected override void EditPrefab(GameObject prefab)
        {
            DeleteChildComponentIfExists<PrefabPlaceholder>(prefab);
            DeleteChildComponentIfExists<PrefabPlaceholdersGroup>(prefab);
        }


        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(AUHandler.AlienCompositeGlassTechType, 1) });
        }

        protected override string GetSpriteName => "IonCubePedestal";
    }
}
