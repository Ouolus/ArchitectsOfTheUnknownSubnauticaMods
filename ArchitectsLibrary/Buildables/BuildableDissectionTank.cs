using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableDissectionTank : GenericPrecursorDecoration
    {
        public BuildableDissectionTank() : base("BuildableDissectionTank", "Dissection Tank", "A decorational tank. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[0];

        protected override string GetOriginalClassId => "44974fcd-c47a-41aa-a279-43eaf234bfa6";

        protected override bool ExteriorOnly => false;
    }
}
