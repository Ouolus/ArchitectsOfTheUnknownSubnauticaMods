using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Handlers;
using UnityEngine;
using System.Collections.Generic;
using ArchitectsLibrary.API;
using ArchitectsLibrary.Utility;
using ArchitectsLibrary.Handlers;

namespace ArchitectsLibrary.Items.AdvancedMaterials
{
    class PrecursorAlloyIngot : Craftable
    {
        GameObject prefab;
        Atlas.Sprite sprite;

        public PrecursorAlloyIngot() : base("PrecursorIngot", LanguageSystem.Get("PrecursorIngot"), LanguageSystem.GetTooltip("PrecursorIngot"))
        {
            OnFinishedPatching += () =>
            {
                PrecursorFabricatorService.SubscribeToFabricator(TechType, PrecursorFabricatorTab.Materials);
                AUHandler.PrecursorAlloyIngotTechType = TechType;
                CraftData.pickupSoundList.Add(TechType, Main.ionCubePickupSound);
                KnownTechHandler.SetAnalysisTechEntry(TechType, new List<TechType>() { TechType });
            };
        }

        protected override TechData GetBlueprintRecipe()
        {
            TechData techData = new TechData(new List<Ingredient>() { new Ingredient(AUHandler.EmeraldTechType, 2), new Ingredient(AUHandler.CobaltIngotTechType, 1), new Ingredient(TechType.Kyanite, 1), new Ingredient(TechType.Diamond, 1) });
            techData.craftAmount = 3;
            return techData;
        }

        public override TechCategory CategoryForPDA => TechCategory.AdvancedMaterials;
        public override TechGroup GroupForPDA => TechGroup.Resources;

        public override bool UnlockedAtStart => false;

        public override float CraftingTime => 8f;

        protected override Atlas.Sprite GetItemSprite()
        {
            return sprite ??= new Atlas.Sprite(Main.assetBundle.LoadAsset<Sprite>("PrecursorIngot_Icon"));
        }
#if SN1
        public override GameObject GetGameObject()
        {
            if (prefab == null)
            {
                prefab = GameObject.Instantiate(Main.assetBundle.LoadAsset<GameObject>("PrecursorIngot_Prefab"));
                prefab.SetActive(false);

                prefab.EnsureComponent<TechTag>().type = TechType;
                prefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
                prefab.EnsureComponent<Pickupable>();
                prefab.EnsureComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.metal;
                var rb = prefab.EnsureComponent<Rigidbody>();
                rb.mass = 15f;
                rb.useGravity = false;
                rb.isKinematic = true;
                prefab.EnsureComponent<WorldForces>();
                prefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;

                var inspect = prefab.EnsureComponent<InspectOnFirstPickup>();
                inspect.pickupAble = prefab.GetComponent<Pickupable>();
                inspect.collision = prefab.GetComponent<Collider>();
                inspect.rigidBody = prefab.GetComponent<Rigidbody>();
                inspect.animParam = "holding_precursorkey";
                inspect.inspectDuration = 4.1f;

                var vfxFabricating = prefab.transform.GetChild(1).gameObject.AddComponent<VFXFabricating>();
                vfxFabricating.localMinY = -0.2f;
                vfxFabricating.localMaxY = 0.15f;
                vfxFabricating.eulerOffset = new Vector3(0f, 90f, 0f);
                vfxFabricating.posOffset = new Vector3(0f, 0.06f, 0.1f);
                vfxFabricating.scaleFactor = 0.75f;

                MaterialUtils.ApplySNShaders(prefab);
                MaterialUtils.ApplyPrecursorMaterials(prefab, 12);
            }
            return prefab;
        }
#else
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            if (prefab == null)
            {
                prefab = GameObject.Instantiate(Main.assetBundle.LoadAsset<GameObject>("PrecursorIngot_Prefab"));
                prefab.SetActive(false);

                prefab.EnsureComponent<TechTag>().type = TechType;
                prefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
                prefab.EnsureComponent<Pickupable>();
                prefab.EnsureComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.metal;
                var rb = prefab.EnsureComponent<Rigidbody>();
                rb.mass = 15f;
                rb.isKinematic = true;
                prefab.EnsureComponent<WorldForces>();
                prefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;

                var inspect = prefab.EnsureComponent<InspectOnFirstPickup>();
                inspect.pickupAble = prefab.GetComponent<Pickupable>();
                inspect.collision = prefab.GetComponent<Collider>();
                inspect.rigidBody = prefab.GetComponent<Rigidbody>();
                inspect.animParam = "holding_precursorkey";
                inspect.inspectDuration = 4.1f;

                var vfxFabricating = prefab.transform.GetChild(1).gameObject.AddComponent<VFXFabricating>();
                vfxFabricating.localMinY = -0.2f;
                vfxFabricating.localMaxY = 0.15f;
                vfxFabricating.eulerOffset = new Vector3(0f, 90f, 0f);
                vfxFabricating.posOffset = new Vector3(0f, 0.06f, 0.1f);
                vfxFabricating.scaleFactor = 0.75f;

                MaterialUtils.ApplySNShaders(prefab);
                MaterialUtils.ApplyPrecursorMaterials(prefab, 12);
            }

            yield return null;
            gameObject.Set(prefab);
        }
#endif
    }
}
