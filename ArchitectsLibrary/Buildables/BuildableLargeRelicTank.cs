using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableLargeRelicTank : GenericPrecursorDecoration
    {
        public BuildableLargeRelicTank() : base("BuildableLargeRelicTank", "Large Relic Case", "A large, empty relic case. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[0];

        protected override string GetOriginalClassId => "1b85a183-2084-42a6-8d85-7e58dd6864bd";

        protected override bool ExteriorOnly => false;
    }
}
