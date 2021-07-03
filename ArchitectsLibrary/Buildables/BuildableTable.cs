using ArchitectsLibrary.API;
using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableTable : GenericPrecursorDecoration
    {
        public BuildableTable() : base("BuildableAlienTable", "Alien Lab Table", "A long table that can be used for decoration. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 0.3f, 0f), Quaternion.identity, new Vector3(0.5f, 0.2f, 1.6f)) };

        protected override string GetOriginalClassId => "68c58fba-bc8d-40fc-a137-544af418f953";

        protected override bool ExteriorOnly => false;

        protected override string GetSpriteName => "Pedestal";

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.transform.GetChild(0).localScale = Vector3.one * 0.6f;
        }
    }
}
