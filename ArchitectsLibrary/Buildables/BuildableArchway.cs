using ArchitectsLibrary.API;
using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableArchway : GenericPrecursorDecoration
    {
        public BuildableArchway() : base("BuildableArchway", "Alien Door Structure", "A large object, likely used as a door frame. Decorational.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(false, false, true, true, true, true, true, placeDefaultDistance: 4f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[0];

        protected override string GetOriginalClassId => "db5a85f5-a5fe-43f8-b71e-7b1f0a8636fe";

        protected override bool ExteriorOnly => true;

        protected override string GetSpriteName => "Archway";
    }
}
