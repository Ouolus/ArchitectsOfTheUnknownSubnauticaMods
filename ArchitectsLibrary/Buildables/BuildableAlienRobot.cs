using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableAlienRobot : GenericPrecursorDecoration
    {
        public BuildableAlienRobot() : base("BuildableAlienRobot", "Alien Robot", "An alien robot. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[0];

        protected override string GetOriginalClassId => "4fae8fa4-0280-43bd-bcf1-f3cba97eed77";

        protected override bool ExteriorOnly => false;
    }
}
