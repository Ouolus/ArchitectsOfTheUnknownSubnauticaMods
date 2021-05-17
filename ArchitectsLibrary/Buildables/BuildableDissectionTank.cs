using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableDissectionTank : GenericPrecursorDecoration
    {
        public BuildableDissectionTank() : base("BuildableDissectionTank", "Dissection Tank", "A decorational tank. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(0.4f, 0.4f, 1.8f)) };

        protected override string GetOriginalClassId => "44974fcd-c47a-41aa-a279-43eaf234bfa6";

        protected override bool ExteriorOnly => false;

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.transform.GetChild(0).position = new Vector3(0f, 0.1f, 0f);
        }
    }
}
