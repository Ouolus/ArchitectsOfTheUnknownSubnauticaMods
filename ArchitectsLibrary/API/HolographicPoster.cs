using UnityEngine;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using ArchitectsLibrary.Utility;
using System.Collections.Generic;

namespace ArchitectsLibrary.API
{
    /// <summary>
    /// A class that helps you with adding custom Architect Library posters. You do NOT need to worry about adding it to the precursor fabricator. Unlocks with Alloy Ingot unless specified otherwise.
    /// </summary>
    public abstract class HolographicPoster : Equipable
    {
        private GameObject cachedPrefab;

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

        public override EquipmentType EquipmentType => EquipmentType.Hand;

        /// <summary>
        /// The recipe for this poster. Is by default 2 titanium and 1 emerald.
        /// </summary>
        /// <returns></returns>
        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(TechType.Titanium, 2), new Ingredient(Handlers.AUHandler.EmeraldTechType, 1)
                }
            };
        }

        public override float CraftingTime => 7f;

        /// <summary>
        /// The texture that appears on the poster. Should have an aspect ratio of 4:5.
        /// </summary>
        /// <returns></returns>
        public abstract Texture2D GetPosterTexture();

        public sealed override GameObject GetGameObject()
        {
            if (cachedPrefab == null)
            {
                cachedPrefab = GameObject.Instantiate(Main.assetBundle.LoadAsset<GameObject>("PrecursorPoster_Prefab"));
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
                vfxFabricating.localMinY = -0.08f;
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
                posterRenderer.material.SetTexture("_MainTex", GetPosterTexture());
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
    }
}
