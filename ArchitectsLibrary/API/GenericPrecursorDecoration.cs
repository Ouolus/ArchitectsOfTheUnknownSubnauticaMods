using System.Collections.Generic;
using System.Collections;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.MonoBehaviours;
using UnityEngine;
using UWE;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// A buildable precursor decoration. This is meant for Architect's Library ONLY but is public just for RotA's omega cube pedestal.
    /// </summary>
    public abstract class GenericPrecursorDecoration : Buildable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="friendlyName"></param>
        /// <param name="description"></param>
        public GenericPrecursorDecoration(string classId, string friendlyName, string description) : base(classId, friendlyName, description)
        {
        }

        /// <summary>
        /// The recipe for this buildable.
        /// </summary>
        /// <returns></returns>
        protected override TechData GetBlueprintRecipe()
        {
            return new(new List<Ingredient>() { new(AUHandler.AlienCompositeGlassTechType, 1) });
        }

        /// <summary>
        /// Don't override this unless you want inconsistencies or know what you're doing.
        /// </summary>
        public override TechCategory CategoryForPDA => ExteriorOnly ? TechCategory.ExteriorOther : TechCategory.Misc;
        /// <summary>
        /// Don't override this unless you want inconsistencies or know what you're doing.
        /// </summary>
        public override TechGroup GroupForPDA => ExteriorOnly ? TechGroup.ExteriorModules : TechGroup.Miscellaneous;

        /// <summary>
        /// Don't fill up the habitat builder at game start.
        /// </summary>
        public override bool UnlockedAtStart => false;
        /// <summary>
        /// By default, requires <see cref="AUHandler.AlienTechnologyMasterTech"/>
        /// </summary>
        public override TechType RequiredForUnlock => AUHandler.AlienTechnologyMasterTech;

        /// <summary>
        /// The bounding box(es) of this constructable.
        /// </summary>
        protected abstract OrientedBounds[] GetBounds { get; }
        /// <summary>
        /// The class id this buildable is copying.
        /// </summary>
        protected abstract string GetOriginalClassId { get; }

        /// <summary>
        /// By default, <see cref="GetItemSprite"/> loads a sprite by name <see cref="GetSpriteName"/> from Architect Library's asset bundle. If you want a sprite from your own asset bundle, you must override <see cref="GetItemSprite"/>.
        /// </summary>
        protected virtual string GetSpriteName { get; }

        /// <summary>
        /// If this buildable can only be built outside.
        /// </summary>
        protected abstract bool ExteriorOnly { get; }

        /// <summary>
        /// By default, loads a sprite by name <see cref="GetSpriteName"/> from Architect Library's asset bundle.
        /// </summary>
        /// <returns></returns>
        protected override Atlas.Sprite GetItemSprite()
        {
            if (string.IsNullOrEmpty(GetSpriteName))
            {
                return SpriteManager.defaultSprite;
            }
            
            return new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>(GetSpriteName));
        }

        /// <summary>
        /// Cell level of the buildable object.
        /// </summary>
        protected virtual LargeWorldEntity.CellLevel CellLevel { get { return LargeWorldEntity.CellLevel.Medium; } }

#if SN1
        public override GameObject GetGameObject()
        {
            GameObject buildablePrefab = new GameObject(ClassID);
            buildablePrefab.SetActive(false);
            PrefabDatabase.TryGetPrefab(GetOriginalClassId, out GameObject originalPrefab);
            GameObject model = GameObject.Instantiate(originalPrefab, buildablePrefab.transform, false);
            if (this is not BuildableAlienRobot or BuildableWarper)
            {
                var rigidBody = model.GetComponent<Rigidbody>();
                if (rigidBody != null)
                    Object.DestroyImmediate(rigidBody);
            }

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
            buildablePrefab.EnsureComponent<LargeWorldEntity>().cellLevel = CellLevel;
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
            sky.renderers = buildablePrefab.GetComponentsInChildren<Renderer>(true);
            buildablePrefab.EnsureComponent<PlaceableOnConstructableFix>();
            if (this is not BuildableAlienRobot and BuildableWarper)
            {
                var rb = buildablePrefab.GetComponent<Rigidbody>();
                if (rb != null)
                    Object.DestroyImmediate(rb);
                foreach (var rigidbody in buildablePrefab.GetAllComponentsInChildren<Rigidbody>())
                {
                    Object.DestroyImmediate(rigidbody);
                }
            }
            EditPrefab(buildablePrefab);
            buildablePrefab.SetActive(true);

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
            GameObject model = GameObject.Instantiate(originalPrefab, buildablePrefab.transform, false);
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
            buildablePrefab.EnsureComponent<LargeWorldEntity>().cellLevel = CellLevel;
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
            buildablePrefab.EnsureComponent<PlaceableOnConstructableFix>();

            gameObject.Set(buildablePrefab);
    }
#endif

        protected abstract ConstructableSettings GetConstructableSettings { get; }

        /// <summary>
        /// Put any more settings necessary for this constructable here.
        /// </summary>
        /// <param name="constructable"></param>
        protected virtual void ApplyExtraConstructableSettings(Constructable constructable)
        {

        }

        /// <summary>
        /// Called in both async AND non-async.
        /// </summary>
        /// <param name="prefab"></param>
        protected virtual void EditPrefab(GameObject prefab)
        {

        }

        /// <summary>
        /// This method ONLY gets called in the async version. <see cref="EditPrefab(GameObject)"/> will get called right before this, regardless of Async or not.
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        protected virtual IEnumerator EditPrefabAsyncOnly(GameObject prefab)
        {
            yield break;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prefab"></param>
        protected static void DeleteChildComponentIfExists<T>(GameObject prefab) where T : Component
        {
            T component = prefab.GetComponentInChildren<T>();
            if (component)
            {
                Object.DestroyImmediate(component);
            }
        }

        /// <summary>
        /// Defines constructable settings for precursor decorations.
        /// </summary>
        public struct ConstructableSettings
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

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="allowedInBase"></param>
            /// <param name="allowedInSub"></param>
            /// <param name="allowedOutside"></param>
            /// <param name="allowedOnWall"></param>
            /// <param name="allowedOnGround"></param>
            /// <param name="allowedOnCeiling"></param>
            /// <param name="allowedOnConstructables"></param>
            /// <param name="rotationEnabled"></param>
            /// <param name="forceUpright"></param>
            /// <param name="placeDefaultDistance"></param>
            /// <param name="placeMinDistance"></param>
            /// <param name="placeMaxDistance"></param>
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
