using System;
using System.Collections.Generic;
using ArchitectsLibrary.Utility;
using RotA.Mono;
using RotA.Mono.AlienTech;
using SMLHelper.V2.Assets;
using UnityEngine;
using UWE;  

namespace RotA.Prefabs.AlienBase
{
    public class OmegaCubeFabricator : Spawnable
    {
        public OmegaCubeFabricator() : base("OmegaCubeFabricator", "Fabricator Device", "Fabricates omega cubes.")
        {
        }

        public override GameObject GetGameObject()
        {
            //prefab essentials
            GameObject model = Mod.assetBundle.LoadAsset<GameObject>("OmegaCubeFabricator");
            GameObject prefab = GameObject.Instantiate(model);
            prefab.SetActive(false);
            prefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Medium;
            prefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            prefab.EnsureComponent<TechTag>().type = TechType;
            MaterialUtils.ApplySNShaders(prefab, 8f);
            MaterialUtils.ApplyPrecursorMaterials(prefab, 8f);
            OmegaFabricatorRoot fabricatorRootComponent = prefab.EnsureComponent<OmegaFabricatorRoot>();
            fabricatorRootComponent.animator = prefab.GetComponent<Animator>();
            fabricatorRootComponent.constructSoundEmitter = prefab.EnsureComponent<FMOD_CustomLoopingEmitter>();
            fabricatorRootComponent.constructSoundEmitter.asset = ScriptableObject.CreateInstance<FMODAsset>();
            fabricatorRootComponent.constructSoundEmitter.asset.path = "event:/env/antechamber_scan_loop";

            //drillable base
            PrefabDatabase.TryGetPrefab("2db600ca-25f7-4000-93a5-f8c2a4ec0387", out GameObject drillableIonCubePrefab);
            var drillableBase = GameObject.Instantiate(drillableIonCubePrefab, prefab.transform);
            drillableBase.transform.localPosition = Vector3.zero;
            drillableBase.transform.localEulerAngles = new(0f, 45f, 0f);
            drillableBase.transform.localScale = new(1.5f, 1.5f, 1.5f);
            drillableBase.TryDestroyChildComponent<PrefabPlaceholder>();
            drillableBase.TryDestroyChildComponent<PrefabPlaceholdersGroup>();
            drillableBase.GetComponentInChildren<VFXVolumetricLight>().color = new(0.25f, 1f, 0.63f);
            drillableBase.GetComponentInChildren<Light>().color = new(0.25f, 1f, 0.63f);
            DestroyPrefabComponents(drillableBase);
            fabricatorRootComponent.generateCubeTransform = drillableBase.SearchChild("DrillablePrecursorIonCrystal(Placeholder)").transform;

            //terminal
            PrefabDatabase.TryGetPrefab("b629c806-d3cd-4ee4-ae99-7b1359b60049", out GameObject terminalPrefab);
            var terminal = GameObject.Instantiate(terminalPrefab, prefab.SearchChild("OmegaCubeTerminalSpawn").transform);
            terminal.transform.localPosition = Vector3.zero;
            terminal.transform.localEulerAngles = Vector3.zero;
            DestroyPrefabComponents(terminal);
            terminal.TryDestroyChildComponent<StoryHandTarget>();
            OmegaTerminal terminalComponent = terminal.EnsureComponent<OmegaTerminal>();

            PrefabDatabase.TryGetPrefab("e8143977-448e-4202-b780-83485fa5f31a", out GameObject antechamberPrefab);
            fabricatorRootComponent.vfxController = fabricatorRootComponent.gameObject.EnsureComponent<VFXController>();
            VFXController originalController = antechamberPrefab.GetComponent<VFXController>();
            fabricatorRootComponent.vfxController.emitters = originalController.emitters;
            fabricatorRootComponent.vfxController.emitters[0].parentTransform = fabricatorRootComponent.transform;
            fabricatorRootComponent.vfxController.emitters[1].parentTransform = fabricatorRootComponent.transform;
            fabricatorRootComponent.vfxController.emitters[0].posOffset = new Vector3(0f, 11f, 0f); //elec arc
            fabricatorRootComponent.vfxController.emitters[1].posOffset = new Vector3(0f, 2f, 0f); //beam
            fabricatorRootComponent.vfxController.emitters[0].fx = CreateNewElecArcPrefab(originalController.emitters[0].fx);
            fabricatorRootComponent.vfxController.emitters[1].fx = CreateNewBeamPrefab(originalController.emitters[1].fx);

            //component connections
            fabricatorRootComponent.terminal = terminalComponent;
            terminalComponent.fabricator = fabricatorRootComponent;
            return prefab;
        }

        GameObject CreateNewElecArcPrefab(GameObject originalPrefab)
        {
            GameObject prefab = GameObject.Instantiate(originalPrefab);
            prefab.name = "OmegaElecArc";
            prefab.SetActive(false);
            prefab.SearchChild("ElecArcTarget").transform.localPosition = new Vector3(0f, -11f, 0f);
            return prefab;
        }
        GameObject CreateNewBeamPrefab(GameObject originalPrefab)
        {
            GameObject prefab = GameObject.Instantiate(originalPrefab);
            prefab.name = "OmegaBeam";
            prefab.SetActive(false);
            foreach(ParticleSystem ps in originalPrefab.GetComponentsInChildren<ParticleSystem>())
            {
                var main = ps.main;
                main.startSize = new ParticleSystem.MinMaxCurve(ps.main.startSize.constant / 2f);
            }
            prefab.SearchChild("xTopGlow").transform.localPosition = new Vector3(0f, 0f, -8f);
            prefab.SearchChild("xElecCubeTopUpper").transform.localPosition = new Vector3(0f, -0.1f, -11f);
            prefab.SearchChild("xElecCubeTop").transform.localPosition = new Vector3(0f, -0.1f, -9f);
            prefab.SearchChild("xBeam").transform.localPosition = new Vector3(0f, -0.1f, 2f);
            return prefab;
        }

        private void DestroyPrefabComponents(GameObject obj)
        {
            obj.TryDestroyChildComponents<LargeWorldEntity>();
            obj.TryDestroyChildComponents<PrefabIdentifier>();
            obj.TryDestroyChildComponents<TechTag>();
            obj.TryDestroyChildComponents<Rigidbody>();
        }
    }
}
