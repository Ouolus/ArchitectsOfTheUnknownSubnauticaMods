using System;
using System.Collections.Generic;
using ArchitectsLibrary.Utility;
using RotA.Mono;
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

            //component connections
            fabricatorRootComponent.terminal = terminalComponent;
            terminalComponent.fabricator = fabricatorRootComponent;
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
