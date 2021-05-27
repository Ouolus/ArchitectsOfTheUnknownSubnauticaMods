using System.Collections.Generic;
using System.Collections;
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
            return new(new List<Ingredient>() { new(AUHandler.AlienCompositeGlassTechType, 1) });
        }

        public override TechCategory CategoryForPDA => ExteriorOnly ? TechCategory.ExteriorOther : TechCategory.Misc;
        public override TechGroup GroupForPDA => ExteriorOnly ? TechGroup.ExteriorModules : TechGroup.Miscellaneous;

        public override bool UnlockedAtStart => false;
        public override TechType RequiredForUnlock => AUHandler.AlienTechnologyMasterTech;

        protected abstract OrientedBounds[] GetBounds { get; }
        protected abstract string GetOriginalClassId { get; }

        protected virtual string GetSpriteName { get; }

        protected abstract bool ExteriorOnly { get; }

        protected override Atlas.Sprite GetItemSprite()
        {
            if (string.IsNullOrEmpty(GetSpriteName))
            {
                return SpriteManager.defaultSprite;
            }
            
            return new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>(GetSpriteName));
        }

#if SN1
        public override GameObject GetGameObject()
        {
            GameObject buildablePrefab = new GameObject(ClassID);
            buildablePrefab.SetActive(false);
            PrefabDatabase.TryGetPrefab(GetOriginalClassId, out GameObject originalPrefab);
            GameObject model = GameObject.Instantiate(originalPrefab);
            model.transform.SetParent(buildablePrefab.transform, false);
            model.transform.localPosition = Vector3.zero;
            model.transform.localEulerAngles = Vector3.zero;
            model.SetActive(true);
            DeleteChildComponentIfExists<LargeWorldEntity>(buildablePrefab);
            DeleteChildComponentIfExists<PrefabIdentifier>(buildablePrefab);
            DeleteChildComponentIfExists<TechTag>(buildablePrefab);
            DeleteChildComponentIfExists<SkyApplier>(buildablePrefab);
            DeleteChildComponentIfExists<ConstructionObstacle>(buildablePrefab);
            buildablePrefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            buildablePrefab.EnsureComponent<TechTag>().type = TechType;
            buildablePrefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Medium;
            SkyApplier sky = buildablePrefab.EnsureComponent<SkyApplier>();
            Constructable con = buildablePrefab.AddComponent<Constructable>();
            con.model = model;
            ConstructableSettings conSettings = GetConstructableSettings;
            con.allowedInBase = conSettings.AllowedInBase;
            con.allowedOutside = conSettings.AllowedOutside;
            con.allowedInSub = conSettings.AllowedInSub;
            con.allowedOnWall = conSettings.AllowedOnWall;
            con.allowedOnGround = conSettings.AllowedOnGround;
            con.allowedOnCeiling = conSettings.AllowedOnCeiling;
            con.allowedOnConstructables = conSettings.AllowedOnConstructables;
            con.rotationEnabled = conSettings.RotationEnabled;
            con.forceUpright = conSettings.ForceUpright;
            con.placeMinDistance = conSettings.PlaceMinDistance;
            con.placeDefaultDistance = conSettings.PlaceDefaultDistance;
            con.placeMaxDistance = conSettings.PlaceMaxDistance;
            ApplyExtraConstructableSettings(con);
            foreach(var bounds in GetBounds)
            {
                buildablePrefab.AddComponent<ConstructableBounds>().bounds = bounds;
            }
            EditPrefab(buildablePrefab);
            buildablePrefab.SetActive(true);
            sky.renderers = buildablePrefab.GetComponentsInChildren<Renderer>(true);

            return buildablePrefab;
        }
#else
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            GameObject buildablePrefab = new GameObject(ClassID);
            buildablePrefab.SetActive(false);
            var request = PrefabDatabase.GetPrefabAsync(GetOriginalClassId);
            yield return request;
            request.TryGetPrefab(out GameObject originalPrefab);
            GameObject model = GameObject.Instantiate(originalPrefab);
            model.transform.SetParent(buildablePrefab.transform, false);
            model.transform.localPosition = Vector3.zero;
            model.transform.localEulerAngles = Vector3.zero;
            model.SetActive(true);
            DeleteChildComponentIfExists<LargeWorldEntity>(buildablePrefab);
            DeleteChildComponentIfExists<PrefabIdentifier>(buildablePrefab);
            DeleteChildComponentIfExists<TechTag>(buildablePrefab);
            DeleteChildComponentIfExists<SkyApplier>(buildablePrefab);
            DeleteChildComponentIfExists<ConstructionObstacle>(buildablePrefab);
            buildablePrefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            buildablePrefab.EnsureComponent<TechTag>().type = TechType;
            buildablePrefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Global;
            SkyApplier sky = buildablePrefab.EnsureComponent<SkyApplier>();
            Constructable con = buildablePrefab.AddComponent<Constructable>();
            con.model = model;
            ConstructableSettings conSettings = GetConstructableSettings;
            con.allowedInBase = conSettings.AllowedInBase;
            con.allowedOutside = conSettings.AllowedOutside;
            con.allowedInSub = conSettings.AllowedInSub;
            con.allowedOnWall = conSettings.AllowedOnWall;
            con.allowedOnGround = conSettings.AllowedOnGround;
            con.allowedOnCeiling = conSettings.AllowedOnCeiling;
            con.allowedOnConstructables = conSettings.AllowedOnConstructables;
            con.rotationEnabled = conSettings.RotationEnabled;
            con.forceUpright = conSettings.ForceUpright;
            con.placeMinDistance = conSettings.PlaceMinDistance;
            con.placeDefaultDistance = conSettings.PlaceDefaultDistance;
            con.placeMaxDistance = conSettings.PlaceMaxDistance;
            ApplyExtraConstructableSettings(con);
            foreach (var bounds in GetBounds)
            {
                buildablePrefab.AddComponent<ConstructableBounds>().bounds = bounds;
            }
            EditPrefab(buildablePrefab);
            yield return EditPrefabAsyncOnly(buildablePrefab);
            buildablePrefab.SetActive(true);
            sky.renderers = buildablePrefab.GetComponentsInChildren<Renderer>(true);

            gameObject.Set(buildablePrefab);
    }
#endif

        protected abstract ConstructableSettings GetConstructableSettings { get; }

        protected virtual void ApplyExtraConstructableSettings(Constructable constructable)
        {

        }

        protected virtual void EditPrefab(GameObject prefab)
        {

        }

        protected virtual IEnumerator EditPrefabAsyncOnly(GameObject prefab)
        {
            yield break;
        }

        protected static void DeleteChildComponentIfExists<T>(GameObject prefab) where T : Component
        {
            T component = prefab.GetComponentInChildren<T>();
            if (component)
            {
                Object.DestroyImmediate(component);
            }
        }

        internal struct ConstructableSettings
        {
            internal readonly bool AllowedInBase;
            internal readonly bool AllowedOutside;
            internal readonly bool AllowedInSub;
            internal readonly bool AllowedOnWall;
            internal readonly bool AllowedOnGround;
            internal readonly bool AllowedOnCeiling;
            internal readonly bool AllowedOnConstructables;
            internal readonly bool RotationEnabled;
            internal readonly bool ForceUpright;
            internal readonly float PlaceDefaultDistance;
            internal readonly float PlaceMinDistance;
            internal readonly float PlaceMaxDistance;

            public ConstructableSettings(bool allowedInBase, bool allowedInSub, bool allowedOutside, bool allowedOnWall, bool allowedOnGround, bool allowedOnCeiling, bool allowedOnConstructables, bool rotationEnabled = true, bool forceUpright = false, float placeDefaultDistance = 2f, float placeMinDistance = 1.2f, float placeMaxDistance = 5f)
            {
                AllowedInBase = allowedInBase;
                AllowedInSub = allowedInSub;
                AllowedOutside = allowedOutside;
                AllowedOnWall = allowedOnWall;
                AllowedOnGround = allowedOnGround;
                AllowedOnCeiling = allowedOnCeiling;
                AllowedOnConstructables = allowedOnConstructables;
                RotationEnabled = rotationEnabled;
                ForceUpright = forceUpright;
                PlaceDefaultDistance = placeDefaultDistance;
                PlaceMinDistance = placeMinDistance;
                PlaceMaxDistance = placeMaxDistance;
            }
        }
    }
}
