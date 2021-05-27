using UnityEngine;
using ArchitectsLibrary.Utility;

namespace ArchitectsLibrary.Buildables
{
    class BuildableLight4 : GenericPrecursorDecoration
    {
        public BuildableLight4() : base("BuildablePrecursorLight4", "Small Alien Light", "A decorational light. Activates within range. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { };

        protected override string GetOriginalClassId => "473a8c4d-162f-4575-bbef-16c1c97d1e9d";

        protected override void EditPrefab(GameObject prefab)
        {

        }

        protected override bool ExteriorOnly => false;

        protected override string GetSpriteName => "Light4";
    }
}
