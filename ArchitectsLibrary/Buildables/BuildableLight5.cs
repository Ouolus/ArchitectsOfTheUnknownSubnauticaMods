using ArchitectsLibrary.API;
using UnityEngine;
using ArchitectsLibrary.Utility;

namespace ArchitectsLibrary.Buildables
{
    class BuildableLight5 : GenericPrecursorDecoration
    {
        public BuildableLight5() : base("BuildablePrecursorLight5", "Small Alien Spotlight", "A decorational light. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { };

        protected override string GetOriginalClassId => "742b410c-14d4-42c6-ac84-0e2bcaff09c1";

        protected override void EditPrefab(GameObject prefab)
        {

        }

        protected override bool ExteriorOnly => false;

        protected override string GetSpriteName => "Light5";
    }
}
