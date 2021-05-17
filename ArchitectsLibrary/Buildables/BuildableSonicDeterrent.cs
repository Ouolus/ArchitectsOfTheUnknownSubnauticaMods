using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableSonicDeterrent : GenericPrecursorDecoration
    {
        public BuildableSonicDeterrent() : base("BuildableSonicDeterrent", "Sonic Deterrent", "A large alien object that is designed to ward off aggressive fauna. Non-functional.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(false, false, true, true, true, true, true, placeDefaultDistance: 8f, placeMinDistance: 5f, placeMaxDistance: 15f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 6f, 0f), Quaternion.identity, new Vector3(2f, 2.8f, 2f)) };

        protected override string GetOriginalClassId => "c5512e00-9959-4f57-98ae-9a9962976eaa";

        protected override bool ExteriorOnly => true;

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.transform.GetChild(0).transform.localPosition = new Vector3(0f, 5f, 0f);
        }
    }
}
