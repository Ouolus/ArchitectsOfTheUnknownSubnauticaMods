using ArchitectsLibrary.API;
using UnityEngine;
using ArchitectsLibrary.MonoBehaviours;

namespace ArchitectsLibrary.Buildables
{
    class BuildableSpecimenCases : GenericPrecursorDecoration
    {
        public BuildableSpecimenCases() : base("BuildableSpecimenCases", "Specimen Cases", "A large alien object, which has many small storage slots. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 1.7f, 0f) * 0.7f, Quaternion.identity, new Vector3(1.15f, 1.69f, 1.15f) * 0.7f) };

        protected override string GetOriginalClassId => "da8f10dd-e181-4f28-bf98-9b6de4a9976a";

        protected override bool ExteriorOnly => false;

        protected override string GetSpriteName => "SpecimenCase";

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.transform.GetChild(0).localScale = Vector3.one * 0.7f;

            AddStorageRoot(prefab, new Vector3(-0.4f, 1.5f, -0.4f), "SpecimenCaseStorage1");
            AddStorageRoot(prefab, new Vector3(-0.4f, 1.5f, 0.4f), "SpecimenCaseStorage2");
            AddStorageRoot(prefab, new Vector3(0.4f, 1.5f, -0.4f), "SpecimenCaseStorage3");
            AddStorageRoot(prefab, new Vector3(0.4f, 1.5f, 0.4f), "SpecimenCaseStorage4");
            AddStorageRoot(prefab, new Vector3(-0.4f, 2.3f, -0.4f), "SpecimenCaseStorage5");
            AddStorageRoot(prefab, new Vector3(-0.4f, 2.3f, 0.4f), "SpecimenCaseStorage6");
            AddStorageRoot(prefab, new Vector3(0.4f, 2.3f, -0.4f), "SpecimenCaseStorage7");
            AddStorageRoot(prefab, new Vector3(0.4f, 2.3f, 0.4f), "SpecimenCaseStorage8");
        }

        private void AddStorageRoot(GameObject prefab, Vector3 localPosition, string classId)
        {
            var storageRoot = new GameObject($"StorageRoot{classId}");
            storageRoot.transform.SetParent(prefab.transform);
            storageRoot.transform.localPosition = localPosition + new Vector3(0f, -0.25f, 0f);
            BoxCollider collider = storageRoot.EnsureComponent<BoxCollider>();
            collider.size = new Vector3(1.1f, 0.4f, 1.1f);
            var storageContainer = storageRoot.EnsureComponent<StorageContainer>();
            storageContainer.height = 1;
            storageContainer.width = 1;
            storageContainer.modelSizeRadius = 4;
            storageContainer.prefabRoot = prefab;
            storageContainer.storageRoot = storageRoot.EnsureComponent<ChildObjectIdentifier>();
            storageContainer.storageRoot.ClassId = classId;
            storageContainer.preventDeconstructionIfNotEmpty = true;
            var spawnPosition = new GameObject($"SpawnPosition{classId}");
            spawnPosition.transform.SetParent(prefab.transform);
            spawnPosition.transform.localPosition = localPosition + new Vector3(0f, -0.3f, 0f);
            var displayCase = storageRoot.EnsureComponent<DisplayCaseDecoration>();
            displayCase.spawnPositions = new Transform[] { spawnPosition.transform };
            displayCase.displayCaseType = DisplayCaseType.SpecimenCase;
        }
    }
}
