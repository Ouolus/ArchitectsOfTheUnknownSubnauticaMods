using UnityEngine;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using ArchitectsLibrary.Utility;
using System.Collections.Generic;
using System.Collections;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// A class that helps you with adding custom Architect Library posters. You do NOT need to worry about adding it to the precursor fabricator. Unlocks with Alloy Ingot unless specified otherwise.
    /// </summary>
    public abstract class HolographicPoster : Equipable
    {
        GameObject cachedPrefab;

        public override bool UnlockedAtStart => false;
        public override TechType RequiredForUnlock => Handlers.AUHandler.PrecursorAlloyIngotTechType;

        /// <summary>
        /// Constructor for this poster.
        /// </summary>
        /// <param name="classId">Class ID.</param>
        /// <param name="friendlyName">Name in inventory.</param>
        /// <param name="description">Tooltip in inventory.</param>
        protected HolographicPoster(string classId, string friendlyName, string description) : base(classId, friendlyName, description)
        {
            OnFinishedPatching += () =>
            {
                PrecursorFabricatorService.SubscribeToFabricator(new PrecursorFabricatorEntry(TechType, PrecursorFabricatorTab.Decorations));
            };
        }

        public override sealed EquipmentType EquipmentType => EquipmentType.Hand;

        /// <summary>
        /// The recipe for this poster. Is by default 2 titanium and 1 emerald.
        /// </summary>
        /// <returns></returns>
        protected override TechData GetBlueprintRecipe()
        {
            return new()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new(TechType.Titanium, 2), new(Handlers.AUHandler.EmeraldTechType, 1)
                }
            };
        }

        /// <summary>
        /// How long it takes to craft the poster
        /// </summary>
        public override float CraftingTime => 7f;

        /// <summary>
        /// The texture that appears on the poster.
        /// </summary>
        /// <returns></returns>
        public abstract Texture2D GetPosterTexture { get; }

        /// <summary>
        /// What shape the poster should be.
        /// </summary>
        /// <returns></returns>
        public abstract PosterDimensions GetPosterDimensions { get; }

        static string GetPrefabNameForDimensions(PosterDimensions dimensions)
        {
            switch (dimensions)
            {
                default:
                    return "PrecursorPoster_Prefab";
                case PosterDimensions.Square:
                    return "PrecursorPosterSquare_Prefab";
                case PosterDimensions.Landscape:
                    return "PrecursorPosterLandscape_Prefab";
            }
        }
        public sealed override GameObject GetGameObject()
        {
            if (cachedPrefab == null)
            {
                cachedPrefab = GameObject.Instantiate(Main.assetBundle.LoadAsset<GameObject>(GetPrefabNameForDimensions(GetPosterDimensions)));
                cachedPrefab.SetActive(false);

                cachedPrefab.EnsureComponent<TechTag>().type = TechType;
                cachedPrefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
                cachedPrefab.EnsureComponent<Pickupable>();
                cachedPrefab.EnsureComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.metal;
                cachedPrefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
                cachedPrefab.EnsureComponent<SkyApplier>().renderers = cachedPrefab.GetComponentsInChildren<Renderer>();
                ConstructableBounds bounds = cachedPrefab.AddComponent<ConstructableBounds>();
                bounds.bounds = new OrientedBounds(new Vector3(0f, -0.03f, 0.1f), Quaternion.identity, new Vector3(0.6f, 0.8f, 0f));
                var fpModel = cachedPrefab.AddComponent<FPModel>();
                fpModel.propModel = cachedPrefab.SearchChild("WorldModel");
                fpModel.viewModel = cachedPrefab.SearchChild("FPModel");
                var placeTool = cachedPrefab.EnsureComponent<PlaceTool>();
                placeTool.mainCollider = cachedPrefab.GetComponent<Collider>();
                placeTool.allowedOnGround = false;
                placeTool.allowedOnWalls = true;
                placeTool.hasAnimations = false;
                placeTool.placementSound = ScriptableObject.CreateInstance<FMODAsset>();
                placeTool.placementSound.path = "event:/env/prec_light_on_2";
                var vfxFabricating = cachedPrefab.SearchChild("CraftModel").EnsureComponent<VFXFabricating>();
                vfxFabricating.localMinY = -0.09f;
                vfxFabricating.localMaxY = 0.06f;
                vfxFabricating.scaleFactor = 2f;

                var soundEmitter = cachedPrefab.EnsureComponent<FMOD_CustomLoopingEmitter>();
                soundEmitter.asset = ScriptableObject.CreateInstance<FMODAsset>();
                soundEmitter.asset.path = "event:/env/prec_artifact_loop";
                soundEmitter.playOnAwake = true;


                MaterialUtils.ApplySNShaders(cachedPrefab);

                MaterialUtils.ApplyPrecursorMaterials(cachedPrefab.SearchChild("CraftModel"), 8f);
                MaterialUtils.ApplyPrecursorMaterials(cachedPrefab.SearchChild("Projector_Top"), 8f);
                MaterialUtils.ApplyPrecursorMaterials(cachedPrefab.SearchChild("Projector_Bottom"), 8f);

                Renderer posterRenderer = cachedPrefab.SearchChild("PosterCanvas").GetComponent<Renderer>();
                var posterTexture = GetPosterTexture;
                if (posterTexture != null) //If no texture is given, show the template texture, rather than just the plain white texture which results from setting a texture to null
                { 
                    posterRenderer.material.SetTexture("_MainTex", posterTexture);
                    posterRenderer.material.SetTexture("_SpecTex", posterTexture);
                    posterRenderer.material.EnableKeyword("MARMO_EMISSION");
                    posterRenderer.material.SetFloat("_EnableGlow", 1f);
                    posterRenderer.material.SetTexture("_Illum", posterTexture);
                }
                posterRenderer.material.SetColor("_Color", new Color(1f, 1.5f, 1f, 0.5f));
                posterRenderer.gameObject.AddComponent<MonoBehaviours.PosterFlicker>().renderer = posterRenderer;

                Renderer[] scanLineRenderers = cachedPrefab.SearchChild("ScanLines").GetComponentsInChildren<Renderer>(true);
                foreach(var rend in scanLineRenderers)
                {
                    rend.material.SetColor("_Color", new Color(0f, 0f, 0f, 0.25f));
                }
            }
            return cachedPrefab;
        }

        public sealed override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            gameObject.Set(GetGameObject());
            yield return null;
        }

        /// <summary>
        /// Determines the shape of the poster you are using. The XML documentation of each value tells you the exact dimensions needed.
        /// </summary>
        public enum PosterDimensions
        {
            /// <summary>
            /// 1:1 aspect ratio. Perfectly square.
            /// </summary>
            Square,

            /// <summary>
            /// 4:5 aspect ratio. Tall.
            /// </summary>
            Portait,

            /// <summary>
            /// 3:2 aspect ratio. Wide.
            /// </summary>
            Landscape
        }
    }
}
