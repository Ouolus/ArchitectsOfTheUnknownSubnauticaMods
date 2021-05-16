using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using ArchitectsLibrary.Handlers;
using UnityEngine;

namespace ArchitectsLibrary.Buildables
{
    abstract class GenericPrecursorDecoration : Buildable
    {
        public GenericPrecursorDecoration(string classId, string friendlyName, string description) : base(classId, friendlyName, description)
        {
        }

        /// <summary>
        /// The recipe for this buildable.
        /// </summary>
        /// <returns></returns>
        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(TechType.Titanium, 1), new Ingredient(AUHandler.EmeraldTechType, 2) });
        }

        public override sealed TechCategory CategoryForPDA => TechCategory.ExteriorModule;
        public override sealed TechGroup GroupForPDA => TechGroup.ExteriorModules;

        protected virtual string ClassIdReference { get; }

        public override GameObject GetGameObject()
        {
            return null;
        }
    }
}
