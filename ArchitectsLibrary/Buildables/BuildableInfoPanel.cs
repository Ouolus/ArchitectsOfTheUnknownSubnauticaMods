using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableInfoPanel : GenericPrecursorDecoration
    {
        public BuildableInfoPanel() : base("BuildableInfoPanel", "Alien Map", "A map of an unknown alien structure. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, false, false, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[0];

        protected override string GetOriginalClassId => "172d9440-2670-45a3-93c7-104fee6da6bc";

        protected override bool ExteriorOnly => false;

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.transform.GetChild(0).transform.localScale = Vector3.one * 0.4f;
        }

        protected override string GetSpriteName => "Infoframe";
    }
}
