using System;
using System.Collections.Generic;
using SMLHelper.V2.Assets;
using UnityEngine;

namespace RotA.Prefabs.AlienBase
{
    public class OmegaCubeFabricator : Spawnable
    {
        public OmegaCubeFabricator() : base("OmegaCubeFabricator", "Fabricator Device", "Fabricates omega cubes.")
        {
        }

        public override GameObject GetGameObject()
        {
            GameObject model = Mod.assetBundle.LoadAsset<GameObject>("OmegaCubeFabricator");
            GameObject prefab = GameObject.Instantiate(model);
            prefab.SetActive(false);
            prefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Medium;
            prefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            prefab.EnsureComponent<TechTag>().type = TechType;

            return prefab;
        }
    }
}
