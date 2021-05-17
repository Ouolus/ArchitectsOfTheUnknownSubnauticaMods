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

        protected abstract OrientedBounds[] GetBounds { get; }
        protected abstract string GetOriginalClassId { get; }

        public override GameObject GetGameObject()
        {
            GameObject buildablePrefab = new GameObject(ClassID);
            buildablePrefab.SetActive(false);
            PrefabDatabase.TryGetPrefab(GetOriginalClassId, out GameObject originalPrefab);
            GameObject model = GameObject.Instantiate(originalPrefab);
            model.transform.SetParent(buildablePrefab.transform, false);
            DeleteChildComponentIfExists<LargeWorldEntity>(buildablePrefab);
            DeleteChildComponentIfExists<PrefabIdentifier>(buildablePrefab);
            DeleteChildComponentIfExists<TechTag>(buildablePrefab);
            DeleteChildComponentIfExists<SkyApplier>(buildablePrefab);
            buildablePrefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            buildablePrefab.EnsureComponent<TechTag>().type = TechType;
            buildablePrefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            buildablePrefab.EnsureComponent<SkyApplier>();
            Constructable con = buildablePrefab.AddComponent<Constructable>();
            con.model = model;
            ConstructableSettings conSettings = GetConstructableSettings;
            con.allowedInBase = conSettings.AllowedInBase;
            con.allowedOutside = conSettings.AllowedOutside;
            con.allowedInSub = conSettings.AllowedInSub;
            con.allowedOnWall = conSettings.AllowedOnWall;
            con.allowedOnGround = conSettings.AllowedOnGround;
            con.allowedOnCeiling = conSettings.AllowedOnCeiling;
            con.rotationEnabled = conSettings.RotationEnabled;
            ApplyExtraConstructableSettings(con);
            foreach(var bounds in GetBounds)
            {
                buildablePrefab.AddComponent<ConstructableBounds>().bounds = bounds;
            }

            return buildablePrefab;
        }

        protected abstract ConstructableSettings GetConstructableSettings { get; }

        protected virtual void ApplyExtraConstructableSettings(Constructable constructable)
        {

        }

        private void DeleteChildComponentIfExists<T>(GameObject prefab) where T : Component
        {
            T component = prefab.GetComponentInChildren<T>();
            if (component)
            {
                Object.DestroyImmediate(component);
            }
        }

        internal struct ConstructableSettings
        {
            internal bool AllowedInBase;
            internal bool AllowedOutside;
            internal bool AllowedInSub;
            internal bool AllowedOnWall;
            internal bool AllowedOnGround;
            internal bool AllowedOnCeiling;
            internal bool RotationEnabled;

            public ConstructableSettings(bool allowedInBase, bool allowedInSub, bool allowedOutside, bool allowedOnWall, bool allowedOnGround, bool allowedOnCeiling, bool rotationEnabled)
            {
                AllowedInBase = allowedInBase;
                AllowedInSub = allowedInSub;
                AllowedOutside = allowedOutside;
                AllowedOnWall = allowedOnWall;
                AllowedOnGround = allowedOnGround;
                AllowedOnCeiling = allowedOnCeiling;
                RotationEnabled = rotationEnabled;
            }
        }
    }
}
