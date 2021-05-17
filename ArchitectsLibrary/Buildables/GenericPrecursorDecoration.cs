using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using ArchitectsLibrary.Handlers;
using UnityEngine;
using UWE;

namespace ArchitectsLibrary.Buildables
{
    abstract class GenericPrecursorDecoration : Buildable
    {
        public GenericPrecursorDecoration(string classId, string friendlyName, string description) : base(classId, friendlyName, description)
        {
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData(new List<Ingredient>() { new Ingredient(TechType.Titanium, 1), new Ingredient(AUHandler.AlienCompositeGlassTechType, 1) });
        }

        public override TechCategory CategoryForPDA => TechCategory.ExteriorModule;
        public override TechGroup GroupForPDA => TechGroup.ExteriorModules;

        public override bool UnlockedAtStart => false;
        public override TechType RequiredForUnlock => AUHandler.PrecursorAlloyIngotTechType;

        protected virtual OrientedBounds GetBounds { get; }
        protected virtual string GetOriginalClassId { get; }

        public override GameObject GetGameObject()
        {
            GameObject buildablePrefab = new GameObject(ClassID);
            buildablePrefab.SetActive(false);
            PrefabDatabase.TryGetPrefab(GetOriginalClassId, out GameObject originalPrefab);
            GameObject.Instantiate(originalPrefab).transform.SetParent(buildablePrefab.transform, false);
            DeleteChildComponentIfExists<LargeWorldEntity>(buildablePrefab);
            DeleteChildComponentIfExists<PrefabIdentifier>(buildablePrefab);
            DeleteChildComponentIfExists<TechTag>(buildablePrefab);
            DeleteChildComponentIfExists<SkyApplier>(buildablePrefab);
            buildablePrefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            buildablePrefab.EnsureComponent<TechTag>().type = TechType;
            buildablePrefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            buildablePrefab.EnsureComponent<SkyApplier>();
            var con = buildablePrefab.AddComponent<Constructable>();
            SetConstructableSettings(con);

            return buildablePrefab;
        }

        protected abstract void SetConstructableSettings(Constructable con);

        private void DeleteChildComponentIfExists<T>(GameObject prefab) where T : Component
        {
            T component = prefab.GetComponentInChildren<T>();
            if (component)
            {
                Object.DestroyImmediate(component);
            }
        }
    }
}
