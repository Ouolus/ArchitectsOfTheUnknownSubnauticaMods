﻿using ArchitectsLibrary.API;
using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    class BuildableColumn : GenericPrecursorDecoration
    {
        public BuildableColumn() : base("BuildableColumn", LanguageSystem.Get("BuildableColumn"), LanguageSystem.GetTooltip("BuildableColumn"))
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(false, false, true, true, true, true, true, placeDefaultDistance: 4f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[] { new OrientedBounds(new Vector3(0f, 4f, 0f), Quaternion.identity, new Vector3(1f, 3.9f, 1f)) };

        protected override string GetOriginalClassId => "640f57a6-6436-4132-a9bb-d914f3e19ef5";

        protected override bool ExteriorOnly => true;

        protected override string GetSpriteName => "Column";
    }
}
