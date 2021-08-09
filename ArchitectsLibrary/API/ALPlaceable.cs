using UnityEngine;
using SMLHelper.V2.Assets;
using ArchitectsLibrary.Utility;
using System.Collections;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// A class that helps you with adding custom Architect Library placeable items. You do NOT need to worry about adding it to the precursor fabricator.
    /// </summary>
    public abstract class ALPlaceable : Equipable
    {
        GameObject cachedPrefab;

        /// <summary>
        /// By default, this unlocks with all alien master tech.
        /// </summary>
        public override bool UnlockedAtStart => false;
        /// <summary>
        /// Unlocked along with all alien master tech by default.
        /// </summary>
        public override TechType RequiredForUnlock => Handlers.AUHandler.AlienTechnologyMasterTech;

        /// <summary>
        /// Constructor for this decoration.
        /// </summary>
        /// <param name="classId">Class ID.</param>
        /// <param name="friendlyName">Name in inventory.</param>
        /// <param name="description">Tooltip in inventory.</param>
        protected ALPlaceable(string classId, string friendlyName, string description) : base(classId, friendlyName, description)
        {
            OnFinishedPatching += () =>
            {
                PrecursorFabricatorService.SubscribeToFabricator(new PrecursorFabricatorEntry(TechType, PrecursorFabricatorTab.Decorations));
            };
        }

        /// <summary>
        /// So it can be held and placed. No need to override so it's sealed.
        /// </summary>
        public override sealed EquipmentType EquipmentType => EquipmentType.Hand;

        /// <summary>
        /// The icon of the decoration.
        /// </summary>
        /// <returns></returns>
        protected override abstract Atlas.Sprite GetItemSprite();

        /// <summary>
        /// How long it takes to craft the decoration.
        /// </summary>
        public override float CraftingTime => 7f;

        /// <summary>
        /// Flag that defines rules on how the decoration can be placed.
        /// </summary>
        /// <returns></returns>
        public abstract PlacementFlags GetPlacementMode { get; }

        /// <summary>
        /// The bounds for placing the decoration. If null, can be placed anywhere.
        /// </summary>
        public virtual OrientedBounds? GetBounds { get; } = null;

        public sealed override GameObject GetGameObject()
        {
            if (cachedPrefab == null)
            {
                cachedPrefab = new GameObject(ClassID);
                cachedPrefab.SetActive(false);

                GameObject model = GetModel();
                model.transform.SetParent(cachedPrefab.transform, false);

                GameObject viewModel = new GameObject("ViewModel");
                model.transform.SetParent(cachedPrefab.transform, false);

                cachedPrefab.EnsureComponent<TechTag>().type = TechType;
                cachedPrefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
                cachedPrefab.EnsureComponent<Pickupable>();
                cachedPrefab.EnsureComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.metal;
                cachedPrefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
                cachedPrefab.EnsureComponent<SkyApplier>().renderers = cachedPrefab.GetComponentsInChildren<Renderer>();

                var rb = cachedPrefab.EnsureComponent<Rigidbody>();
                rb.mass = 5f;
                rb.useGravity = false;
                rb.isKinematic = true;
                cachedPrefab.EnsureComponent<WorldForces>();
                if (GetBounds.HasValue)
                {
                    ConstructableBounds bounds = cachedPrefab.AddComponent<ConstructableBounds>();
                    bounds.bounds = GetBounds.Value;
                }
                var fpModel = cachedPrefab.AddComponent<FPModel>();
                fpModel.propModel = model;
                fpModel.viewModel = viewModel;
                var placeTool = cachedPrefab.EnsureComponent<PlaceTool>();
                placeTool.mainCollider = cachedPrefab.GetComponent<Collider>();
                placeTool.allowedOnGround = GetPlacementMode.HasFlag(PlacementFlags.Ground);
                placeTool.allowedOnWalls = GetPlacementMode.HasFlag(PlacementFlags.Walls);
                placeTool.allowedOnCeiling = GetPlacementMode.HasFlag(PlacementFlags.Ceiling);
                placeTool.allowedInBase = GetPlacementMode.HasFlag(PlacementFlags.Inside);
                placeTool.allowedOutside = GetPlacementMode.HasFlag(PlacementFlags.Outside);
                placeTool.hasAnimations = false;
                placeTool.placementSound = SNAudioEvents.GetFmodAsset(SNAudioEvents.Paths.LightStickPlace);
                placeTool.alignWithSurface = GetPlacementMode.HasFlag(PlacementFlags.AlignWithSurface);
                placeTool.allowedOnRigidBody = GetPlacementMode.HasFlag(PlacementFlags.AllowedOnRigidbody);
            }
            return cachedPrefab;
        }

        /// <summary>
        /// The model for the decoration. Will be cached after the prefab is spawned for the first time. Note: Shaders are NOT automatically converted and crafting models are not automatically created. See the methods in <see cref="MaterialUtils"/> for shader conversion and the <see cref="VFXFabricating"/> class for craft models.
        /// </summary>
        /// <returns></returns>
        public abstract GameObject GetModel();

        public sealed override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            gameObject.Set(GetGameObject());
            yield return null;
        }

        /// <summary>
        /// Defines how a placeable can be placed.
        /// </summary>
        [System.Flags]
        public enum PlacementFlags
        {
            /// <summary>
            /// Can be placed on floors.
            /// </summary>
            Ground = 1,
            /// <summary>
            /// Can be placed on walls.
            /// </summary>
            Walls = 2,
            /// <summary>
            /// Can be placed on ceilings.
            /// </summary>
            Ceiling = 4,
            /// <summary>
            /// Can be placed indoors.
            /// </summary>
            Inside = 8,
            /// <summary>
            /// Can be placed outside.
            /// </summary>
            Outside = 16,
            /// <summary>
            /// Whether the poster aligns with surfaces. For posters on walls mainly.
            /// </summary>
            AlignWithSurface = 32,
            /// <summary>
            /// Allowed in Cyclops, Sea Voyager, etc...
            /// </summary>
            AllowedOnRigidbody = 64,
        }
    }
}
