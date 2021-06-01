using System;
using System.Collections.Generic;
using ArchitectsLibrary.Utility;
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

            //drillable base
            PrefabDatabase.TryGetPrefab("2db600ca-25f7-4000-93a5-f8c2a4ec0387", out GameObject drillableIonCubePrefab);
            var drillableBase = GameObject.Instantiate(drillableIonCubePrefab, prefab.transform);
            drillableBase.transform.localPosition = Vector3.zero;
            drillableBase.TryDestroyChildComponent<PrefabPlaceholder>();
            drillableBase.TryDestroyChildComponent<PrefabPlaceholdersGroup>();
            return prefab;
        }
    }
}
