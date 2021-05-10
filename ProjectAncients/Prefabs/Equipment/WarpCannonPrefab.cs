using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System.Collections.Generic;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.Utility;
using UnityEngine;

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

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(AUHandler.PrecursorAlloyIngotTechType, 2), new Ingredient(TechType.PrecursorIonCrystal, 1), new Ingredient(TechType.EnameledGlass, 1) //enameled glass is substitute for reinforced glass
                }
            };
        }

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
            vfxFabricating.localMinY = 0f;
            vfxFabricating.localMaxY = 0.2f;
            vfxFabricating.eulerOffset = new Vector3(0f, 0f, 90f);

            var warpCannon = prefab.AddComponent<Mono.Equipment.WarpCannon>();
            warpCannon.fireSound = ScriptableObject.CreateInstance<FMODAsset>();
            warpCannon.fireSound.path = "event:/creature/warper/portal_open";
            warpCannon.altFireSound = ScriptableObject.CreateInstance<FMODAsset>();
            warpCannon.altFireSound.path = "event:/creature/warper/portal_close";
            warpCannon.drawSound = ScriptableObject.CreateInstance<FMODAsset>();
            warpCannon.drawSound.path = "event:/tools/teraformer/draw";
            warpCannon.animator = prefab.GetComponentInChildren<Animator>(true);
            warpCannon.leftHandIKTarget = prefab.SearchChild("Attach_Left").transform;
            warpCannon.ikAimRightArm = true;
            warpCannon.ikAimLeftArm = true;
            warpCannon.mainCollider = prefab.GetComponent<Collider>();

            var skyApplier = prefab.AddComponent<SkyApplier>();
            skyApplier.renderers = prefab.GetComponentsInChildren<Renderer>(true);

            return prefab;
        }
    }
}
