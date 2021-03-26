using ECCLibrary;
using ProjectAncients.Mono.AlienTech;
using SMLHelper.V2.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UWE;

namespace ProjectAncients.Prefabs.AlienBase
{
    public class RuinedGuardianPrefab : Spawnable
    {
        static GameObject prefab;
        const string volumetricLight = "aa6b2ede-a1bf-4f70-980c-9ed2a51375a1";

        public RuinedGuardianPrefab() : base("RuinedGuardian", "Mysterious wreckage", "")
        {
            OnFinishedPatching = () =>
            {
                StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(TechType, new Vector3(367.1f, -341f, -1747f),
                    "DestroyedGuardian", 200f));
            };
        }


        public override WorldEntityInfo EntityInfo => new WorldEntityInfo()
        {
            classId = ClassID,
            cellLevel = LargeWorldEntity.CellLevel.Medium,
            localScale = Vector3.one,
            slotType = EntitySlot.Type.Large,
            techType = this.TechType
        };

        public override GameObject GetGameObject()
        {
            if (prefab == null)
            {
                prefab = GameObject.Instantiate(Mod.assetBundle.LoadAsset<GameObject>("Guardian_Destroyed"));
                prefab.SetActive(false);
                prefab.EnsureComponent<SkyApplier>();
                prefab.EnsureComponent<TechTag>().type = TechType;
                prefab.EnsureComponent<PrefabIdentifier>().classId = ClassID;
                prefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Medium;
                prefab.EnsureComponent<GuardianEyes>();
                prefab.EnsureComponent<AudioClipEmitter>().clipPoolPrefix = "Creaking";
                ECCHelpers.ApplySNShaders(prefab, new UBERMaterialProperties(5f, 1f, 1f));
                AddVolumetricLight(prefab.SearchChild("LightPos1", ECCStringComparison.Equals).transform);
                AddVolumetricLight(prefab.SearchChild("LightPos2", ECCStringComparison.Equals).transform);
            }
            return prefab;
        }

        private void AddVolumetricLight(Transform transform)
        {
            if (PrefabDatabase.TryGetPrefab(volumetricLight, out GameObject volumetricPrefab))
            {
                GameObject child = GameObject.Instantiate(volumetricPrefab, transform);
                child.transform.localPosition = Vector3.zero;
                child.transform.localRotation = Quaternion.LookRotation(Vector3.forward);
                child.transform.localScale = Vector3.one * 0.01f;
            }
        }
    }
}
