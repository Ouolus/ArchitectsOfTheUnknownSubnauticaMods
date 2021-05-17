using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableClaw : GenericPrecursorDecoration
    {
        public BuildableClaw() : base("BuildableClaw", "Alien Claw Device", "A decorational alien claw device. Placeable on ceilings inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, false, false, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, -0.5f, 0f), Quaternion.identity, new Vector3(0.4f, 0.4f, 0.4f)) };

        protected override string GetOriginalClassId => "6a01a336-fb46-469a-9f7d-1659e07d11d7";

        protected override bool ExteriorOnly => false;

        protected override void EditPrefab(GameObject prefab)
        {

        }
    }
}
