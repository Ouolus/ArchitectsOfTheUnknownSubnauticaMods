using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildablePlatform : GenericPrecursorDecoration
    {
        public BuildablePlatform() : base("BuildablePlatform", "Alien Platform", "A large, flat platform, for decoration. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(2f, 0.4f, 2f)) };

        protected override string GetOriginalClassId => "738892ae-64b0-4240-953c-cea1d19ca111";

        protected override bool ExteriorOnly => false;

        protected override void EditPrefab(GameObject prefab)
        {
            foreach (Collider col in prefab.GetComponentsInChildren<Collider>())
            {
                Object.DestroyImmediate(col);
            }
            prefab.transform.GetChild(0).GetChild(0).transform.localPosition = Vector3.zero;
            BoxCollider boxCollider = prefab.transform.GetChild(0).gameObject.EnsureComponent<BoxCollider>();
            boxCollider.center = new Vector3(0f, 0.5f, 0f);
            boxCollider.size = new Vector3(4f, 1f, 4f);
        }
    }
}
