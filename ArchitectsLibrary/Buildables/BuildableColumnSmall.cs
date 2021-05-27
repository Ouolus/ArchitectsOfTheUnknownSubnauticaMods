using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableColumnSmall : GenericPrecursorDecoration
    {
        public BuildableColumnSmall() : base("BuildableColumnSmall", "Small Alien Column Structure", "A tall, column-like object with a complex design. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 4f * 0.37f, 0f), Quaternion.identity, new Vector3(1f * 0.37f, 3.5f * 0.37f, 1f * 0.37f)) };

        protected override string GetOriginalClassId => "640f57a6-6436-4132-a9bb-d914f3e19ef5";

        protected override bool ExteriorOnly => false;

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.transform.GetChild(0).localScale = Vector3.one * 0.37f;
        }

        protected override string GetSpriteName => "Column";
    }
}
