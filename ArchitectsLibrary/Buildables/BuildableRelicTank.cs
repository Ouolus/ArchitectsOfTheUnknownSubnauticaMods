using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableRelicTank : GenericPrecursorDecoration
    {
        public BuildableRelicTank() : base("BuildableRelicTank", "Empty Relic Case", "An empty relic case. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(0.4f, 0.4f, 0.4f))};

        protected override string GetOriginalClassId => "d0fea4da-39f2-47b4-aece-bb12fe7f9410";

        protected override bool ExteriorOnly => false;

        protected override string GetSpriteName => "RelicCase";
    }
}
