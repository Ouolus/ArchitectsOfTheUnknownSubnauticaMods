using SMLHelper.V2.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ArchitectsLibrary.Items
{
    class Electricube : ReskinCraftable
    {
        public Electricube() : base("Electricube", "Electricube", "A high capacity energy source, with a similar structure to the Ion Cube. Has applications in biomechanical materials and warping technology.")
        {
        }

        protected override string ReferenceClassId => "38ebd2e5-9dcc-4d7a-ada4-86a22e01191a";

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData() { Ingredients = new List<Ingredient>() { new Ingredient(TechType.Titanium, 2) } };
        }

        protected override void ApplyChangesToPrefab(GameObject prefab)
        {
            prefab.GetComponentInChildren<Renderer>().material.SetColor("_Color", new Color(1f, 0f, 1f));
        }

        protected override string SpriteName()
        {
            return "Electricube_Icon";
        }
    }
}
