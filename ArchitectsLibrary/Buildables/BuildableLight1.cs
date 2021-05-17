using UnityEngine;
using ArchitectsLibrary.Utility;

namespace ArchitectsLibrary.Buildables
{
    class BuildableLight1 : GenericPrecursorDecoration
    {
        public BuildableLight1() : base("BuildablePrecursorLight1", "Large Alien Activated Light", "A decorational light, that activates within range.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(false, false, true, true, true, true, true, placeDefaultDistance: 4f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(0.4f, 0.4f, 0.4f)) };

        protected override string GetOriginalClassId => "ce3c3053-5022-404e-a165-e31abe495f1b";

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.transform.GetChild(0).transform.eulerAngles = new Vector3(180f, 0f, 0f);
        }

        protected override bool ExteriorOnly => true;
    }
}
