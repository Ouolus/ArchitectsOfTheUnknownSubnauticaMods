using UnityEngine;
using SMLHelper.V2.Crafting;
using System.Collections;
using System.Collections.Generic;
using ArchitectsLibrary.API;
using ArchitectsLibrary.Handlers;

namespace ArchitectsLibrary.Buildables
{
    class BuildableMicroscope : GenericPrecursorDecoration
    {
        public BuildableMicroscope() : base("BuildableMicroscope", "Alien Microscope", "What appears to be an advanced microscope. Non-functional. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, false, true, false, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(0.4f, 0.4f, 0.4f)) };

        protected override string GetOriginalClassId => "a30d0115-c37e-40ec-a5d9-318819e94f81";

        protected override bool ExteriorOnly => false;

        protected override string GetSpriteName => "Microscope";

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.transform.GetChild(0).localScale = Vector3.one * 0.7f;
        }
    }
}
