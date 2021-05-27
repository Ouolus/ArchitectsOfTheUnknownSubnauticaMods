using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableLargeRelicTank : GenericPrecursorDecoration
    {
        public BuildableLargeRelicTank() : base("BuildableLargeRelicTank", "Large Relic Case", "A large, empty relic case. Placeable inside and outside.")
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
        }
    }
}
