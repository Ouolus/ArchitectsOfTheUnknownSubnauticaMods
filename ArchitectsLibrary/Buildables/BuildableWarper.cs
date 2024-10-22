﻿using UnityEngine;
using SMLHelper.V2.Crafting;
using System.Collections;
using System.Collections.Generic;
using ArchitectsLibrary.API;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.MonoBehaviours;

namespace ArchitectsLibrary.Buildables
{
    class BuildableWarper : GenericPrecursorDecoration
    {
        public BuildableWarper() : base("BuildableWarper", LanguageSystem.Get("BuildableWarper"), LanguageSystem.GetTooltip("BuildableWarper"))
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(false, false, true, true, true, true, true, placeDefaultDistance: 4f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[0];

        protected override string GetOriginalClassId => "c7103527-f6fa-4d1e-a75d-146433851557";

        protected override bool ExteriorOnly => true;

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(TechType.Warper, 1)});
        }

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.EnsureComponent<WarperBuildableFix>();
        }

        //protected override string GetSpriteName => "AlienRobot";
    }
}
