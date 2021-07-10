using RotA.Mono.AlienTech;
using SMLHelper.V2.Assets;
using UnityEngine;

namespace RotA.Prefabs.AlienBase
{
    public class BlackHolePrefab : Spawnable
    {        
        public BlackHolePrefab() : base("ResearchBaseBlackHole", "Contained singularity", "...")
        {
        }

#if SN1
        public override GameObject GetGameObject()
        {
            GameObject seamothPrefab = CraftData.GetPrefabForTechType(TechType.Seamoth);
            GameObject prefab = FixVFX(seamothPrefab.GetComponent<SeaMoth>().torpedoTypes[0].prefab.GetComponent<SeamothTorpedo>().explosionPrefab.GetComponent<PrefabSpawn>().prefab);
            prefab.EnsureComponent<TechTag>().type = TechType;
            prefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            prefab.EnsureComponent<SphereCollider>().radius = 3.5f;
            prefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Medium;
            prefab.EnsureComponent<BlackHole>();
            GameObject containment = Object.Instantiate(Mod.assetBundle.LoadAsset<GameObject>("SingularityContainment_Prefab"));
            containment.transform.SetParent(prefab.transform, false);
            containment.transform.localPosition = new Vector3(0f, -4.32f, 0f);

            prefab.SetActive(true);
            return prefab;
        }
#else
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            var task = CraftData.GetPrefabForTechTypeAsync(TechType.Seamoth);
            yield return task;
            
            GameObject prefab = FixVFX(task.GetResult().GetComponent<SeaMoth>().torpedoTypes[0].prefab.GetComponent<SeamothTorpedo>().explosionPrefab.GetComponent<PrefabSpawn>().prefab);
            prefab.EnsureComponent<TechTag>().type = TechType;
            prefab.EnsureComponent<PrefabIdentifier>().ClassId = ClassID;
            prefab.EnsureComponent<SphereCollider>().radius = 3.5f;
            prefab.EnsureComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Medium;
            prefab.EnsureComponent<BlackHole>();
            GameObject containment = Object.Instantiate(Mod.assetBundle.LoadAsset<GameObject>("SingularityContainment_Prefab"));
            containment.transform.SetParent(prefab.transform, false);
            containment.transform.localPosition = new Vector3(0f, -4.32f, 0f);
            
            prefab.SetActive(true);
            gameObject.Set(prefab);
        }
#endif

        GameObject FixVFX(GameObject original)
        {
            GameObject newVfx = GameObject.Instantiate(original);
            if (newVfx != null)
            {
                DisablePS(newVfx);
                newVfx.transform.GetChild(0).gameObject.SetActive(false);
                //newVfx.transform.GetChild(1).gameObject.SetActive(false);
                newVfx.transform.GetChild(2).gameObject.SetActive(false);
                newVfx.transform.GetChild(4).gameObject.SetActive(false);
                newVfx.transform.GetChild(6).gameObject.SetActive(false);
                newVfx.transform.GetChild(7).gameObject.SetActive(false);
                MakeParticleSystemsLooping(newVfx);
                Object.DestroyImmediate(newVfx.GetComponent<VFXDestroyAfterSeconds>());
            }
            return newVfx;
        }

        private void DisablePS(GameObject obj)
        {
            Object.DestroyImmediate(obj.GetComponent<ParticleSystem>());
        }

        private void MakeParticleSystemsLooping(GameObject obj)
        {
            foreach (ParticleSystem ps in obj.GetComponentsInChildren<ParticleSystem>())
            {
                var main = ps.main;
                main.loop = true;
            }
        }
    }
}
