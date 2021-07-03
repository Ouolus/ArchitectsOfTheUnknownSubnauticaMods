using SMLHelper.V2.Crafting;
using UnityEngine;
using System.Collections.Generic;
using ArchitectsLibrary.API;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.MonoBehaviours;
using ArchitectsLibrary.Utility;

namespace ArchitectsLibrary.Buildables
{
    class BuildableItemPedestal : GenericPrecursorDecoration
    {
        public BuildableItemPedestal() : base("BuildableItemPedestal", "Empty Item Pedestal", "A large pedestal that can be used to display a single item. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(1f, 0.4f, 1f)) };

        protected override string GetOriginalClassId => "ea65ef91-e875-4157-99f9-a8f4f6dc92f8";

        protected override bool ExteriorOnly => false;

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.transform.GetChild(0).localEulerAngles = Vector3.up * 270f;

            DeleteChildComponentIfExists<PrefabPlaceholder>(prefab);
            DeleteChildComponentIfExists<PrefabPlaceholdersGroup>(prefab);

            var storageRoot = new GameObject("StorageRoot");
            storageRoot.transform.SetParent(prefab.transform);

            GameObject spawnPosition = new GameObject("SpawnPosition");
            spawnPosition.transform.SetParent(prefab.gameObject.SearchChild("PrecursorIonCrystal(Placeholder)").transform, false);
            spawnPosition.transform.localEulerAngles = Vector3.zero;

            var storageContainer = prefab.EnsureComponent<StorageContainer>();
            storageContainer.height = 1;
            storageContainer.width = 1;
            storageContainer.modelSizeRadius = 4;
            storageContainer.prefabRoot = prefab;
            storageContainer.storageRoot = storageRoot.EnsureComponent<ChildObjectIdentifier>();
            storageContainer.storageRoot.ClassId = "BuildableItemPedestalContainer";
            storageContainer.preventDeconstructionIfNotEmpty = true;

            var displayCase = prefab.EnsureComponent<DisplayCaseDecoration>();
            displayCase.spawnPositions = new[] { spawnPosition.transform };
            displayCase.displayCaseType = DisplayCaseType.Pedestal;
        }


        protected override TechData GetBlueprintRecipe()
        {
            return new(new List<Ingredient> { new(AUHandler.AlienCompositeGlassTechType, 1) });
        }

        protected override string GetSpriteName => "EmptyItemPedestal";
    }
}
