using ArchitectsLibrary.API;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.Utility;
using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System.Collections.Generic;
using UnityEngine;
using SMLHelper.V2.Handlers;
using RotA.Mono.Equipment;

namespace RotA.Prefabs.Equipment
{
    public class IonKnifePrefab : Equipable
    {
        GameObject _cachedPrefab;
        public IonKnifePrefab() : base("IonKnife", "Ion Knife", "Ion knife that makes me go yes.")
        {
            OnFinishedPatching += () =>
            {
                KnownTechHandler.SetAnalysisTechEntry(TechType, new TechType[0],
                    UnlockSprite: Mod.assetBundle.LoadAsset<Sprite>("AlienUpgrade_Popup"));
            };
        }

        List<TechType> compatibleTech => new List<TechType>() { TechType.PrecursorIonCrystal, AUHandler.OmegaCubeTechType, AUHandler.ElectricubeTechType, AUHandler.RedIonCubeTechType };

        public override EquipmentType EquipmentType => EquipmentType.Hand;

        public override bool UnlockedAtStart => false;

        public override TechCategory CategoryForPDA => TechCategory.Tools;
        public override TechGroup GroupForPDA => TechGroup.Personal;

        public override float CraftingTime => 10f;

        public override Vector2int SizeInInventory => new Vector2int(1, 1);

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData() { craftAmount = 1, Ingredients = new List<Ingredient>() { new Ingredient(TechType.Knife, 1), new Ingredient(AUHandler.PrecursorAlloyIngotTechType, 1) } };
        }

        public override TechType RequiredForUnlock => Mod.warpMasterTech;

        protected override Atlas.Sprite GetItemSprite()
        {
            return new Atlas.Sprite(Mod.assetBundle.LoadAsset<Sprite>("IonKnife_Icon"));
        }

        public override GameObject GetGameObject()
        {
            if (_cachedPrefab == null)
            {
                GameObject model = Mod.assetBundle.LoadAsset<GameObject>("IonKnife_Prefab");
                GameObject prefab = GameObject.Instantiate(model);
                prefab.SetActive(false);
                prefab.EnsureComponent<PrefabIdentifier>().classId = ClassID;
                prefab.EnsureComponent<TechTag>().type = TechType;
                prefab.EnsureComponent<Pickupable>();
                prefab.EnsureComponent<SkyApplier>().renderers = prefab.GetComponentsInChildren<Renderer>(true);
                var rb = prefab.EnsureComponent<Rigidbody>();
                rb.useGravity = false;
                rb.mass = 8f;
                prefab.EnsureComponent<WorldForces>();
                var fpModel = prefab.EnsureComponent<FPModel>();
                fpModel.propModel = prefab.SearchChild("WorldModel");
                fpModel.viewModel = prefab.SearchChild("ViewModel");
                
                //fix position in hand
                var viewModelChild = fpModel.viewModel.transform.GetChild(0);
                viewModelChild.transform.localPosition = new Vector3(0.01f, 0.04f, 0f);
                viewModelChild.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                viewModelChild.transform.localEulerAngles = new Vector3(0, 160, 0);

                MaterialUtils.ApplySNShaders(prefab);
                MaterialUtils.ApplyPrecursorMaterials(prefab, 6f);
                MaterialUtils.FixIonCubeMaterials(prefab, 1f);

                var vfxFabricating = prefab.SearchChild("CraftModel").AddComponent<VFXFabricating>();
                vfxFabricating.localMinY = -0.6f;
                vfxFabricating.localMaxY = 0.6f;
                vfxFabricating.scaleFactor = 2f;
                vfxFabricating.posOffset = new Vector3(0f, 0.01f, 0f);
                vfxFabricating.eulerOffset = new Vector3(90f, 90f, 0f);

                var tool = prefab.AddComponent<IonKnife>();
                tool.ikAimRightArm = true;
                tool.mainCollider = prefab.GetComponent<Collider>();
                tool.drawSound = SNAudioEvents.GetFmodAsset("event:/player/cube terminal_open");

                _cachedPrefab = prefab;
            }
            return _cachedPrefab;
        }
    }
}
