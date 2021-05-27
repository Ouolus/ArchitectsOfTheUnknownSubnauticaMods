using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildablePedestal : GenericPrecursorDecoration
    {
        public BuildablePedestal() : base("BuildablePedestal", "Alien Pedestal", "A pedestal that can be used for decoration or to display something. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(0.8f, 0.4f, 0.8f)) };

        protected override string GetOriginalClassId => "78009225-a9fa-4d21-9580-8719a3368373";

        protected override bool ExteriorOnly => false;

        protected override void EditPrefab(GameObject prefab)
        {
            
        }

        protected override string GetSpriteName => "Pedestal";
    }
}
