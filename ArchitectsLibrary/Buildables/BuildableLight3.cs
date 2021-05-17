using UnityEngine;
using ArchitectsLibrary.Utility;

namespace ArchitectsLibrary.Buildables
{
    class BuildableLight3 : GenericPrecursorDecoration
    {
        public BuildableLight3() : base("BuildablePrecursorLight3", "Large Alien Light", "A decorational light. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(0.4f, 0.4f, 1f)) };

        protected override string GetOriginalClassId => "5631b64f-d0f0-47f5-b7ac-f23215432070";

        protected override void EditPrefab(GameObject prefab)
        {
            Light light = prefab.EnsureComponent<Light>();
            light.color = new Color(0.4191176f, 1f, 0.6875251f);
            light.intensity = 1.6f;
            light.range = 10f;
        }

        protected override bool ExteriorOnly => false;
    }
}
