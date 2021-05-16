using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System.Collections.Generic;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.Utility;
using UnityEngine;
using System.Collections;

namespace ProjectAncients.Prefabs.Equipment
{
    public class WarpCannonPrefab : Equipable
    {
        public WarpCannonPrefab() : base("WarpCannon", "Handheld Warping Device", "A warp cannon that makes metious go yes.")
        {
        }

        public override EquipmentType EquipmentType => EquipmentType.Hand;

        public override bool UnlockedAtStart => false;

        public override TechCategory CategoryForPDA => TechCategory.Tools;
        public override TechGroup GroupForPDA => TechGroup.Personal;

        public override float CraftingTime => 12f;

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(AUHandler.PrecursorAlloyIngotTechType, 2), new Ingredient(TechType.PrecursorIonBattery, 1), new Ingredient(AUHandler.AlienCompositeGlassTechType, 1)
                }
            };
        }

#if SN1
        public override GameObject GetGameObject()
        {
            GameObject model = Mod.assetBundle.LoadAsset<GameObject>("WarpCannon_Prefab");
            GameObject prefab = GameObject.Instantiate(model);
            prefab.SetActive(false);
            prefab.EnsureComponent<PrefabIdentifier>().classId = ClassID;
            prefab.EnsureComponent<TechTag>().type = TechType;
            prefab.EnsureComponent<Pickupable>();
            var rb = prefab.EnsureComponent<Rigidbody>();
            rb.useGravity = false;
            rb.mass = 10f;
            prefab.EnsureComponent<WorldForces>();
            var fpModel = prefab.EnsureComponent<FPModel>();
            fpModel.propModel = prefab.SearchChild("WorldModel");
            fpModel.viewModel = prefab.SearchChild("ViewModel");

            MaterialUtils.ApplySNShaders(prefab);
            MaterialUtils.ApplyPrecursorMaterials(prefab, 8f);            

            var vfxFabricating = prefab.SearchChild("CraftModel").AddComponent<VFXFabricating>();
            vfxFabricating.localMinY = -0.31f;
            vfxFabricating.localMaxY = 0.1f;
            vfxFabricating.scaleFactor = 0.1f;
            vfxFabricating.posOffset = new Vector3(-0.70f, 0.1f, -0.1f);
            vfxFabricating.eulerOffset = new Vector3(0f, 90f, 90f);

            var chargeSound = prefab.AddComponent<FMOD_StudioEventEmitter>();
            chargeSound.path = "event:/sub/cyclops/shield_on_loop";

            var warpCannon = prefab.AddComponent<Mono.Equipment.WarpCannon>();
            warpCannon.fireSound = ScriptableObject.CreateInstance<FMODAsset>();
            warpCannon.fireSound.path = "event:/creature/warper/portal_open";
            warpCannon.altFireSound = ScriptableObject.CreateInstance<FMODAsset>();
            warpCannon.altFireSound.path = "event:/creature/warper/portal_close";
            warpCannon.drawSound = ScriptableObject.CreateInstance<FMODAsset>();
            warpCannon.drawSound.path = "event:/player/key terminal_close";
            warpCannon.animator = prefab.GetComponentInChildren<Animator>(true);
            warpCannon.leftHandIKTarget = prefab.SearchChild("Attach_Left").transform;
            warpCannon.ikAimRightArm = true;
            warpCannon.ikAimLeftArm = true;
            warpCannon.mainCollider = prefab.GetComponent<Collider>();
            warpCannon.chargeLoop = chargeSound;

            GameObject warperPrefab = CraftData.GetPrefabForTechType(TechType.Warper);
            var warper = warperPrefab.GetComponent<Warper>();

            warpCannon.warpInPrefab = warper.warpOutEffectPrefab; //Yes I know they are swapped
            warpCannon.warpOutPrefab = warper.warpInEffectPrefab;

            var skyApplier = prefab.AddComponent<SkyApplier>();
            skyApplier.renderers = prefab.GetComponentsInChildren<Renderer>(true);

            return prefab;
        }
#else
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            GameObject model = Mod.assetBundle.LoadAsset<GameObject>("WarpCannon_Prefab");
            GameObject prefab = GameObject.Instantiate(model);
            prefab.SetActive(false);
            prefab.EnsureComponent<PrefabIdentifier>().classId = ClassID;
            prefab.EnsureComponent<TechTag>().type = TechType;
            prefab.EnsureComponent<Pickupable>();
            var rb = prefab.EnsureComponent<Rigidbody>();
            rb.useGravity = false;
            rb.mass = 10f;
            prefab.EnsureComponent<WorldForces>();
            var fpModel = prefab.EnsureComponent<FPModel>();
            fpModel.propModel = prefab.SearchChild("WorldModel");
            fpModel.viewModel = prefab.SearchChild("ViewModel");

            MaterialUtils.ApplySNShaders(prefab);
            MaterialUtils.ApplyPrecursorMaterials(prefab, 8f);

            var vfxFabricating = prefab.SearchChild("CraftModel").AddComponent<VFXFabricating>();
            vfxFabricating.localMinY = -0.31f;
            vfxFabricating.localMaxY = 0.1f;
            vfxFabricating.scaleFactor = 0.1f;
            vfxFabricating.posOffset = new Vector3(-0.70f, 0.1f, -0.1f);
            vfxFabricating.eulerOffset = new Vector3(0f, 90f, 90f);

            var chargeSound = prefab.AddComponent<FMOD_StudioEventEmitter>();
            chargeSound.path = "event:/sub/cyclops/shield_on_loop";

            var warpCannon = prefab.AddComponent<Mono.Equipment.WarpCannon>();
            warpCannon.fireSound = ScriptableObject.CreateInstance<FMODAsset>();
            warpCannon.fireSound.path = "event:/creature/warper/portal_open";
            warpCannon.altFireSound = ScriptableObject.CreateInstance<FMODAsset>();
            warpCannon.altFireSound.path = "event:/creature/warper/portal_close";
            warpCannon.drawSound = ScriptableObject.CreateInstance<FMODAsset>();
            warpCannon.drawSound.path = "event:/player/key terminal_close";
            warpCannon.animator = prefab.GetComponentInChildren<Animator>(true);
            warpCannon.leftHandIKTarget = prefab.SearchChild("Attach_Left").transform;
            warpCannon.ikAimRightArm = true;
            warpCannon.ikAimLeftArm = true;
            warpCannon.mainCollider = prefab.GetComponent<Collider>();
            warpCannon.chargeLoop = chargeSound;

            var warperPrefabTask = CraftData.GetPrefabForTechTypeAsync(TechType.Warper);
            yield return warperPrefabTask;
            var warperPrefab = warperPrefabTask.GetResult();
            var warper = warperPrefab.GetComponent<Warper>();

            warpCannon.warpInPrefab = warper.warpOutEffectPrefab; //Yes I know they are swapped
            warpCannon.warpOutPrefab = warper.warpInEffectPrefab;

            var skyApplier = prefab.AddComponent<SkyApplier>();
            skyApplier.renderers = prefab.GetComponentsInChildren<Renderer>(true);

            gameObject.Set(prefab);
        }
#endif

        public override TechType RequiredForUnlock => Mod.warpMasterTech;
    }
}
