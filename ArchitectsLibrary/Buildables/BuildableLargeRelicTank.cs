using ArchitectsLibrary.API;
using UnityEngine;
using ArchitectsLibrary.MonoBehaviours;

namespace ArchitectsLibrary.Buildables
{
    class BuildableLargeRelicTank : GenericPrecursorDecoration
    {
        public BuildableLargeRelicTank() : base("BuildableLargeRelicTank", LanguageSystem.Get("BuildableLargeRelicTank"), LanguageSystem.GetTooltip("BuildableLargeRelicTank"))
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(0.4f, 0.4f, 1.8f)) };

        protected override string GetOriginalClassId => "1b85a183-2084-42a6-8d85-7e58dd6864bd";

        protected override bool ExteriorOnly => false;

        protected override string GetSpriteName => "RelicCaseLarge";

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.GetComponentInChildren<Collider>().transform.localPosition = new Vector3(0f, 0.1f, 0f);

            var storageRoot = new GameObject("StorageRoot");
            storageRoot.transform.SetParent(prefab.transform);

            var spawnPosition = new GameObject("SpawnPosition");
            spawnPosition.transform.SetParent(prefab.transform);
            spawnPosition.transform.localPosition = new(0f, 1.3f, -1.5f);

            var spawnPosition2 = new GameObject("SpawnPosition2");
            spawnPosition2.transform.SetParent(prefab.transform);
            spawnPosition2.transform.localPosition = new(0f, 1.3f, 0f);

            var spawnPosition3 = new GameObject("SpawnPosition3");
            spawnPosition3.transform.SetParent(prefab.transform);
            spawnPosition3.transform.localPosition = new(0f, 1.3f, 1.5f);

            var storageContainer = prefab.EnsureComponent<StorageContainer>();
            storageContainer.height = 6;
            storageContainer.width = 2;
            storageContainer.prefabRoot = prefab;
            storageContainer.storageRoot = storageRoot.EnsureComponent<ChildObjectIdentifier>();
            storageContainer.storageRoot.ClassId = "BuildableLargeRelicTankContainer";
            storageContainer.preventDeconstructionIfNotEmpty = true;

            var displayCase = prefab.EnsureComponent<DisplayCaseDecoration>();
            displayCase.spawnPositions = new Transform[] { spawnPosition.transform, spawnPosition2.transform, spawnPosition3.transform };
            displayCase.displayCaseType = DisplayCaseType.RelicTank;
        }
    }
}
