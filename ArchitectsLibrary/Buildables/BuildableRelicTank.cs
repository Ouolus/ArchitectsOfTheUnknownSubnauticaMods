﻿using ArchitectsLibrary.API;
using ArchitectsLibrary.MonoBehaviours;
using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableRelicTank : GenericPrecursorDecoration
    {
        public BuildableRelicTank() : base("BuildableRelicTank", LanguageSystem.Get("BuildableRelicTank"), LanguageSystem.GetTooltip("BuildableRelicTank"))
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(0.71f, 0.4f, 0.71f))};

        protected override string GetOriginalClassId => "d0fea4da-39f2-47b4-aece-bb12fe7f9410";

        protected override bool ExteriorOnly => false;

        protected override string GetSpriteName => "RelicCase";

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.GetComponentInChildren<Collider>().transform.localPosition = new Vector3(0f, 0.1f, 0f);

            var storageRoot = new GameObject("StorageRoot");
            storageRoot.transform.SetParent(prefab.transform);

            var spawnPosition = new GameObject("SpawnPosition");
            spawnPosition.transform.SetParent(prefab.transform);
            spawnPosition.transform.localPosition = new(0f, 1.3f, 0f);

            var storageContainer = prefab.EnsureComponent<StorageContainer>();
            storageContainer.height = 2;
            storageContainer.width = 2;
            storageContainer.modelSizeRadius = 4;
            storageContainer.prefabRoot = prefab;
            storageContainer.storageRoot = storageRoot.EnsureComponent<ChildObjectIdentifier>();
            storageContainer.storageRoot.ClassId = "BuildableRelicTankContainer";
            storageContainer.preventDeconstructionIfNotEmpty = true;

            var displayCase = prefab.EnsureComponent<DisplayCaseDecoration>();
            displayCase.spawnPositions = new Transform[] { spawnPosition.transform };
            displayCase.displayCaseType = DisplayCaseType.RelicTank;
        }
    }
}
