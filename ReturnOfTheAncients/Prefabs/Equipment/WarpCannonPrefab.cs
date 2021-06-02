using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using System.Collections.Generic;
using ArchitectsLibrary.Handlers;
using ArchitectsLibrary.Utility;
using UnityEngine;
using System.Collections;
using System.Linq;

namespace RotA.Prefabs.Equipment
{
    public class WarpCannonPrefab : Equipable
    {
        GameObject _cachedPrefab;
        public WarpCannonPrefab() : base("WarpCannon", "Handheld Warping Device", "Alien warping technology refitted into a compact handheld tool for personal use. Requires ion batteries to function. Unstable.")
        {
        }

        List<TechType> compatibleTech => BatteryCharger.compatibleTech.Where(x => x != TechType.Battery).ToList();

        public override EquipmentType EquipmentType => EquipmentType.Hand;

        public override bool UnlockedAtStart => false;

        public override TechCategory CategoryForPDA => TechCategory.Tools;
        public override TechGroup GroupForPDA => TechGroup.Personal;

        public override float CraftingTime => 12f;

        static WarperData warperCreatureData;

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient(AUHandler.PrecursorAlloyIngotTechType, 2), new Ingredient(TechType.PrecursorIonBattery, 1), new Ingredient(AUHandler.ElectricubeTechType, 1)
                }
            };
        }

        private void UpdateWarpCannonRenderer(Renderer renderer)
        {
            renderer.material.SetFloat("_Shininess", 7f);
            renderer.material.SetFloat("_Fresnel", 0.2f);
            renderer.material.SetFloat("_EmissionLM", 0.01f);
            renderer.material.SetFloat("_EmissionLMNight", 0.01f);
            renderer.material.SetColor("_GlowColor", new Color(0.54f, 1f, 0.54f));
            if (renderer.material.name.Contains("Precursor")) //precursor-only
            {
                MaterialUtils.ApplyPrecursorMaterials(renderer.gameObject, 8f, MaterialUtils.PrecursorSpecularColor.Blue, 0.2f);
                renderer.material.SetFloat("_GlowStrength", 10f);
                renderer.material.SetFloat("_GlowStrengthNight", 10f);
                renderer.material.SetFloat("_EmissionLM", 0.3f);
                renderer.material.SetFloat("_EmissionLMNight", 0.3f);
            }
            else //alterra-only
            {
                renderer.material.SetFloat("_SpecInt", 2f);
            }
        }
#if SN1
        public override GameObject GetGameObject()
        {
            if (_cachedPrefab == null)
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
                foreach(Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
                {
                    UpdateWarpCannonRenderer(renderer);
                }

                var vfxFabricating = prefab.SearchChild("CraftModel").AddComponent<VFXFabricating>();
                vfxFabricating.localMinY = -0.42f;
                vfxFabricating.localMaxY = 0.15f;
                vfxFabricating.scaleFactor = 1f;
                vfxFabricating.posOffset = new Vector3(0f, 0.15f, 0f);
                vfxFabricating.eulerOffset = new Vector3(0f, 90f, 0f);

                var chargeSound = prefab.AddComponent<FMOD_StudioEventEmitter>();
                chargeSound.path = "event:/tools/stasis_gun/charge";

                var warpCannon = prefab.AddComponent<Mono.Equipment.WarpCannon>();
                warpCannon.portalOpenSound = ScriptableObject.CreateInstance<FMODAsset>();
                warpCannon.portalOpenSound.path = "event:/creature/warper/portal_open";
                warpCannon.portalCloseSound = ScriptableObject.CreateInstance<FMODAsset>();
                warpCannon.portalCloseSound.path = "event:/creature/warper/portal_close";
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
                warpCannon.warpOutPrefabDestroyAutomatically = warper.warpOutEffectPrefab;
                if (warperCreatureData == null)
                {
                    WarperData originalData = warperPrefab.GetComponent<RangedAttackLastTarget>().attackTypes[0]
                        .ammoPrefab.GetComponent<WarpBall>().warperData;
                    warperCreatureData = GetWarpCannonCreatureSpawnData(originalData);
                }

                warpCannon.warperCreatureData = warperCreatureData;

                warpCannon.primaryNodeVfxPrefab = GetLoopingWarperVfx(warper.warpInEffectPrefab);
                warpCannon.secondaryNodeVfxPrefab = GetLoopingWarperVfx(warper.warpOutEffectPrefab);

                var energyMixin = prefab.GetComponent<EnergyMixin>();
                energyMixin.compatibleBatteries = compatibleTech;
                energyMixin.defaultBattery = TechType.PrecursorIonBattery;
                energyMixin.storageRoot =
                    prefab.FindChild("BatterySlot").gameObject.EnsureComponent<ChildObjectIdentifier>();

                var batteryModels = new List<EnergyMixin.BatteryModels>();

                // required to check for the normal battery too so Custom Batteries will successfully add the custom batteries.
                var defaultTechTypes = new[] {TechType.Battery, TechType.PrecursorIonBattery};

                foreach (var techType in defaultTechTypes)
                {
                    var gameObject = CraftData.GetPrefabForTechType(techType);
                    var obj = GameObject.Instantiate(gameObject);
                    gameObject.SetActive(false);
                    obj.SetActive(false);

                    GameObject.DestroyImmediate(obj.GetComponentInChildren<WorldForces>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<Rigidbody>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<Battery>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<LargeWorldEntity>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<TechTag>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<EntityTag>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<Pickupable>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<Collider>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<SkyApplier>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<UniqueIdentifier>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<VFXFabricating>());

                    obj.transform.SetParent(prefab.SearchChild("ViewModel").transform);
                    obj.transform.localPosition = new(-0.05f, -0.00f, -0.08f);
                    obj.transform.localScale = new(.52f, .52f, .76f);
                    obj.transform.localEulerAngles = new(0, 52, 180);

                    batteryModels.Add(new()
                    {
                        techType = techType,
                        model = obj
                    });
                }

                energyMixin.batteryModels = batteryModels.ToArray();

                var skyApplier = prefab.AddComponent<SkyApplier>();
                skyApplier.renderers = prefab.GetComponentsInChildren<Renderer>(true);

                var illumControl = prefab.SearchChild("ViewModel").AddComponent<Mono.PrecursorIllumControl>();
                illumControl.renderers = new List<Renderer>();
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("p_rotor_low").GetComponent<Renderer>());
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("p_rotor_low.001").GetComponent<Renderer>());
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("p_rotor_low.002").GetComponent<Renderer>());
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("p_rotor_low.003").GetComponent<Renderer>());
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("p_front_low").GetComponent<Renderer>());
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("p_arms_low").GetComponent<Renderer>());
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("d_prongs_low").GetComponent<Renderer>());
                illumControl.light = illumControl.gameObject.SearchChild("Light").EnsureComponent<Light>();
                illumControl.light.intensity = 0.5f;
                warpCannon.illumControl = illumControl;
                
                _cachedPrefab = prefab;
            }
            
            return _cachedPrefab;
        }
#else
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            if (_cachedPrefab == null)
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
                foreach(Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
                {
                    UpdateWarpCannonRenderer(renderer);
                }

                var vfxFabricating = prefab.SearchChild("CraftModel").AddComponent<VFXFabricating>();
                vfxFabricating.localMinY = -0.42f;
                vfxFabricating.localMaxY = 0.15f;
                vfxFabricating.scaleFactor = 1f;
                vfxFabricating.posOffset = new Vector3(0f, 0.15f, 0f);
                vfxFabricating.eulerOffset = new Vector3(0f, 90f, 0f);

                var chargeSound = prefab.AddComponent<FMOD_StudioEventEmitter>();
                chargeSound.path = "event:/tools/stasis_gun/charge";

                var warpCannon = prefab.AddComponent<Mono.Equipment.WarpCannon>();
                warpCannon.portalOpenSound = ScriptableObject.CreateInstance<FMODAsset>();
                warpCannon.portalOpenSound.path = "event:/creature/warper/portal_open";
                warpCannon.portalCloseSound = ScriptableObject.CreateInstance<FMODAsset>();
                warpCannon.portalCloseSound.path = "event:/creature/warper/portal_close";
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
                warpCannon.warpOutPrefabDestroyAutomatically = warper.warpOutEffectPrefab;
                if (warperCreatureData == null)
                {
                    WarperData originalData = warperPrefab.GetComponent<RangedAttackLastTarget>().attackTypes[0]
                        .ammoPrefab.GetComponent<WarpBall>().warperData;
                    warperCreatureData = GetWarpCannonCreatureSpawnData(originalData);
                }

                warpCannon.warperCreatureData = warperCreatureData;

                warpCannon.primaryNodeVfxPrefab = GetLoopingWarperVfx(warper.warpInEffectPrefab);
                warpCannon.secondaryNodeVfxPrefab = GetLoopingWarperVfx(warper.warpOutEffectPrefab);

                var energyMixin = prefab.GetComponent<EnergyMixin>();
                energyMixin.compatibleBatteries = compatibleTech;
                energyMixin.defaultBattery = TechType.PrecursorIonBattery;
                energyMixin.storageRoot = prefab.FindChild("BatterySlot").EnsureComponent<ChildObjectIdentifier>();

                var batteryModels = new List<EnergyMixin.BatteryModels>();

                // required to check for the normal battery too so Custom Batteries will successfully add the custom batteries.
                var defaultTechTypes = new[] {TechType.Battery, TechType.PrecursorIonBattery};

                foreach (var techType in defaultTechTypes)
                {
                    var task = CraftData.GetPrefabForTechTypeAsync(techType);
                    var go = task.GetResult();
                    var obj = GameObject.Instantiate(go);
                    go.SetActive(false);
                    obj.SetActive(false);

                    GameObject.DestroyImmediate(obj.GetComponentInChildren<WorldForces>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<Rigidbody>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<Battery>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<LargeWorldEntity>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<TechTag>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<EntityTag>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<Pickupable>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<Collider>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<SkyApplier>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<UniqueIdentifier>());
                    GameObject.DestroyImmediate(obj.GetComponentInChildren<VFXFabricating>());

                    obj.transform.SetParent(prefab.SearchChild("ViewModel").transform);
                    obj.transform.localPosition = new(-0.05f, -0.00f, -0.08f);
                    obj.transform.localScale = new(.52f, .52f, .76f);
                    obj.transform.localEulerAngles = new(0, 52, 180);

                    batteryModels.Add(new()
                    {
                        techType = techType,
                        model = obj
                    });
                }

                energyMixin.batteryModels = batteryModels.ToArray();

                var skyApplier = prefab.AddComponent<SkyApplier>();
                skyApplier.renderers = prefab.GetComponentsInChildren<Renderer>(true);

                var illumControl = prefab.SearchChild("ViewModel").AddComponent<Mono.PrecursorIllumControl>();
                illumControl.renderers = new List<Renderer>();
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("p_rotor_low").GetComponent<Renderer>());
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("p_rotor_low.001").GetComponent<Renderer>());
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("p_rotor_low.002").GetComponent<Renderer>());
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("p_rotor_low.003").GetComponent<Renderer>());
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("p_front_low").GetComponent<Renderer>());
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("p_arms_low").GetComponent<Renderer>());
                illumControl.renderers.Add(illumControl.gameObject.SearchChild("d_prongs_low").GetComponent<Renderer>());
                illumControl.light = illumControl.gameObject.SearchChild("Light").EnsureComponent<Light>();
                illumControl.light.intensity = 0.5f;
                warpCannon.illumControl = illumControl;

                _cachedPrefab = prefab;
            }

            gameObject.Set(_cachedPrefab);
        }
#endif

        static WarperData GetWarpCannonCreatureSpawnData(WarperData original)
        {
            WarperData so = ScriptableObject.CreateInstance<WarperData>();
            so.warpInCreaturesData = new List<WarperData.WarpInData>(original.warpInCreaturesData);

            var lostRiverCreatures = new List<WarperData.WarpInCreature>() { new WarperData.WarpInCreature() { techType = TechType.SpineEel, minNum = 1, maxNum = 1 }, new WarperData.WarpInCreature { techType = TechType.GhostRayBlue, minNum = 1, maxNum = 2 }, new WarperData.WarpInCreature { techType = TechType.Mesmer, minNum = 2, maxNum = 3 } };
            AddBiomeToWarperData(so, "LostRiver_BonesField", new WarperData.WarpInData() { creatures = lostRiverCreatures});
            AddBiomeToWarperData(so, "LostRiver_BonesField_Corridor", new WarperData.WarpInData() { creatures = lostRiverCreatures});
            ReplaceBiomeInWarperData(so, "safeShallows", new WarperData.WarpInData() { creatures = new List<WarperData.WarpInCreature>() { new WarperData.WarpInCreature() { techType = TechType.Gasopod, minNum = 1, maxNum = 1 }, new WarperData.WarpInCreature() { techType = TechType.RabbitRay, minNum = 4, maxNum = 6 }, new WarperData.WarpInCreature() { techType = TechType.Crash, minNum = 1, maxNum = 1 }, new WarperData.WarpInCreature() { techType = TechType.Jumper, minNum = 1, maxNum = 2 } } });
            ReplaceBiomeInWarperData(so, "kelpForest", new WarperData.WarpInData() { creatures = new List<WarperData.WarpInCreature>() { new WarperData.WarpInCreature() { techType = TechType.Stalker, minNum = 1, maxNum = 2 }, new WarperData.WarpInCreature() { techType = TechType.Mesmer, minNum = 4, maxNum = 6 } } });
            AddBiomeToWarperData(so, "void", new WarperData.WarpInData() { creatures = new List<WarperData.WarpInCreature>() { new WarperData.WarpInCreature() { techType = TechType.GhostLeviathan, minNum = 1, maxNum = 1 }, new WarperData.WarpInCreature() { techType = TechType.GhostRayBlue, minNum = 1, maxNum = 2 }, new WarperData.WarpInCreature() { techType = TechType.Warper, minNum = 1, maxNum = 1 }, new WarperData.WarpInCreature() { techType = TechType.BoneShark, minNum = 1, maxNum = 1 } } });
            return so;
        }

        static void AddBiomeToWarperData(WarperData warperData, string biomeName, WarperData.WarpInData creaturesData)
        {
            int biomeCount = warperData.warpInCreaturesData.Count;
            warperData.warpInCreaturesData.Add(creaturesData);
            warperData.warpInCreaturesData[biomeCount].biomeName = biomeName;
        }

        static void ReplaceBiomeInWarperData(WarperData warperData, string biomeName, WarperData.WarpInData creaturesData)
        {
            for(int i = 0; i < warperData.warpInCreaturesData.Count; i++)
            {
                if (warperData.warpInCreaturesData[i].biomeName == biomeName)
                {
                    warperData.warpInCreaturesData[i] = creaturesData;
                    warperData.warpInCreaturesData[i].biomeName = biomeName;
                }
            }
        }
        GameObject GetLoopingWarperVfx(GameObject originalVfx)
        {
            GameObject returnObj = GameObject.Instantiate(originalVfx);
            returnObj.SetActive(false);
            var destroyAfterSeconds = returnObj.GetComponent<VFXDestroyAfterSeconds>();
            if (destroyAfterSeconds)
            {
                Object.DestroyImmediate(destroyAfterSeconds);
            }
            foreach(ParticleSystem ps in returnObj.GetComponentsInChildren<ParticleSystem>(true))
            {
                var main = ps.main;
                main.loop = true;
                /*main.duration = 2.5f;
                main.startLifetime = 3f;
                main.startDelay = 0f;
                main.maxParticles = main.maxParticles * 3;*/
            }
            return returnObj;
        }

        public override TechType RequiredForUnlock => Mod.warpMasterTech;
    }
}
