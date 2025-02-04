﻿using ArchitectsLibrary.API;
using UnityEngine;
using ArchitectsLibrary.Utility;

namespace ArchitectsLibrary.Buildables
{
    class BuildableLight1 : GenericPrecursorDecoration
    {
        public BuildableLight1() : base("BuildablePrecursorLight1", LanguageSystem.Get("BuildablePrecursorLight1"), LanguageSystem.GetTooltip("BuildablePrecursorLight1"))
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, false, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 0.5f, 0f), Quaternion.identity, new Vector3(0.4f, 0.4f, 0.4f)) };

        protected override string GetOriginalClassId => "ce3c3053-5022-404e-a165-e31abe495f1b";

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.transform.GetChild(0).transform.eulerAngles = new Vector3(180f, 0f, 0f);
            prefab.transform.GetChild(0).transform.localScale = Vector3.one * 0.9f;
            prefab.GetComponentInChildren<SphereCollider>().radius = 6f / 0.9f;
        }

        protected override bool ExteriorOnly => false;

        protected override string GetSpriteName => "Light1";
    }
}
