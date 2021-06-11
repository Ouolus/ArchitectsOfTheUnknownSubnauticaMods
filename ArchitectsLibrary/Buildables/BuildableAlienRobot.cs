using UnityEngine;
using SMLHelper.V2.Crafting;
using System.Collections;
using System.Collections.Generic;
using ArchitectsLibrary.API;
using ArchitectsLibrary.Handlers;

namespace ArchitectsLibrary.Buildables
{
    class BuildableAlienRobot : GenericPrecursorDecoration
    {
        public BuildableAlienRobot() : base("BuildableAlienRobot", "Alien Robot", "An alien robot that wanders around. Placeable inside and outside.")
        {
        }

        protected override ConstructableSettings GetConstructableSettings => new ConstructableSettings(true, true, true, true, true, true, true, placeDefaultDistance: 2f, placeMinDistance: 2f, placeMaxDistance: 10f);

        protected override OrientedBounds[] GetBounds => new OrientedBounds[0];

        protected override string GetOriginalClassId => "4fae8fa4-0280-43bd-bcf1-f3cba97eed77";

        protected override bool ExteriorOnly => false;

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(TechType.PrecursorDroid, 1) });
        }

        protected override void EditPrefab(GameObject prefab)
        {
            prefab.transform.GetChild(0).localPosition = new Vector3(0f, 0.7f, 0f);
        }

        protected override string GetSpriteName => "AlienRobot";
    }
}
