using ECCLibrary;
using RotA.Mono.AlienTech;
using SMLHelper.V2.Assets;
using System.Collections;
using UnityEngine;
using UWE;

namespace RotA.Prefabs.AlienBase
{
    public class RuinedGuardianPrefab : Spawnable
    {
        static GameObject prefab;
        const string volumetricLight = "aa6b2ede-a1bf-4f70-980c-9ed2a51375a1";

        public RuinedGuardianPrefab() : base("RuinedGuardian", "Mysterious wreckage", "")
        {
            OnFinishedPatching = () =>
            {
                StaticCreatureSpawns.RegisterStaticSpawn(new StaticSpawn(TechType, new Vector3(365.86f, -330.00f, -1735.00f),
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

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
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
                CoroutineHost.StartCoroutine(AddVolumetricLight(prefab.SearchChild("LightPos1").transform));
                CoroutineHost.StartCoroutine(AddVolumetricLight(prefab.SearchChild("LightPos2").transform));
                prefab.transform.localScale = Vector3.one * 0.5f;
            }

            yield return null;

            gameObject.Set(prefab);
        }

        IEnumerator AddVolumetricLight(Transform transform)
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync(volumetricLight);
            yield return request;

            if (request.TryGetPrefab(out GameObject volumetricPrefab))
            {
                GameObject child = GameObject.Instantiate(volumetricPrefab, transform);
                child.transform.localPosition = Vector3.zero;
                child.transform.localRotation = Quaternion.LookRotation(Vector3.forward);
                child.transform.localScale = Vector3.one * 0.01f;
            }
        }
    }
}
