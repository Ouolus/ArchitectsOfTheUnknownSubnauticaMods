using System.Collections;
using UnityEngine;
using UWE;

namespace RotA.Mono
{
    public class VoidShoalSpawner : MonoBehaviour
    {
        GameObject shoalPrefab_Spinefish;
        GameObject shoalPrefab_Hoopfish;
        VFXSchoolFishManager shoalManager;
        private const string spinefishShoalClassId = "2d3ea578-e4fa-4246-8bc9-ed8e66dec781";
        private const string hoopfishShoalClassId = "08cb3290-504b-4191-97ee-6af1588af5c0";
        private const int shoalCap = 20;
        private bool canSpawn = false;

        private IEnumerator Start()
        {
            shoalManager = VFXSchoolFishManager.main;
            yield return SetShoalPrefabAsync();
            InvokeRepeating("UpdateCanSpawn", Random.value, 4f); //Biome checks are probably slow, so they are at a much lower rate than the actual spawning
            InvokeRepeating("TrySpawn", Random.value, 0.3f);
        }

        private IEnumerator SetShoalPrefabAsync()
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync(spinefishShoalClassId);
            yield return request;
            if(request.TryGetPrefab(out GameObject prefab))
            {
                shoalPrefab_Spinefish = GameObject.Instantiate(prefab);
                shoalPrefab_Spinefish.SetActive(false);
                shoalPrefab_Spinefish.name = "Spinefish_Void_School";
                Renderer renderer = shoalPrefab_Spinefish.GetComponentInChildren<Renderer>();
                Material material = renderer.material;
                material.SetColor("_GlowColor", new Color(0.99f, 0.97f, 0.81f));
                material.SetFloat("_GlowStrength", 1.50f);
                material.SetFloat("_GlowStrengthNight", 1.50f);
                renderer.material = material;
                renderer.transform.localScale = Vector3.one * 1.50f;
                GameObject.Destroy(shoalPrefab_Spinefish.GetComponent<LargeWorldEntity>());
                GameObject.Destroy(shoalPrefab_Spinefish.GetComponent<PrefabIdentifier>());
                GameObject.Destroy(shoalPrefab_Spinefish.GetComponent<Collider>());
                shoalPrefab_Spinefish.AddComponent<DestroyWhenFarAway>();
            }
            else
            {
                ECCLibrary.Internal.ECCLog.AddMessage("Failed to grab spinefish shoal prefab");
            }
            IPrefabRequest request2 = PrefabDatabase.GetPrefabAsync(hoopfishShoalClassId);
            yield return request2;
            if (request2.TryGetPrefab(out GameObject prefab2))
            {
                shoalPrefab_Hoopfish = GameObject.Instantiate(prefab2);
                shoalPrefab_Hoopfish.SetActive(false);
                shoalPrefab_Hoopfish.name = "Spinefish_Void_School";
                Renderer renderer = shoalPrefab_Hoopfish.GetComponentInChildren<Renderer>();
                Material material = renderer.material;
                material.SetColor("_GlowColor", new Color(1f, 0f, 1f));
                material.SetColor("_Color", new Color(1.00f, 0f, 0f));
                material.SetFloat("_GlowStrength", 3f);
                material.SetFloat("_GlowStrengthNight", 3f);
                material.SetFloat("_EmissionLM", 0.5f);
                material.SetFloat("_EmissionLMNight", 0.5f);
                material.SetFloat("_SpecInt", 1f);
                renderer.material = material;
                renderer.transform.localScale = Vector3.one * 1.50f;
                GameObject.Destroy(shoalPrefab_Hoopfish.GetComponent<LargeWorldEntity>());
                GameObject.Destroy(shoalPrefab_Hoopfish.GetComponent<PrefabIdentifier>());
                GameObject.Destroy(shoalPrefab_Hoopfish.GetComponent<Collider>());
                shoalPrefab_Hoopfish.AddComponent<DestroyWhenFarAway>();
            }
            else
            {
                ECCLibrary.Internal.ECCLog.AddMessage("Failed to grab hoopfish shoal prefab");
            }
        }

        void UpdateCanSpawn()
        {
            if (VoidGargSpawner.IsVoidBiome(Player.main.GetBiomeString()))
            {
                if (GetAllShoalsInWorld() < shoalCap)
                {
                    canSpawn = true;
                    return;
                }
            }
            canSpawn = false;
        }
        void TrySpawn()
        {
            if (canSpawn)
            {
                Instantiate(GetPrefab(), GetRandomSpawnPosition(), Random.rotation).SetActive(true);
            }
        }
        GameObject GetPrefab()
        {
            if(Random.value > 0.1f)
            {
                return shoalPrefab_Spinefish;
            }
            else
            {
                return shoalPrefab_Hoopfish;
            }
        }

        int GetAllShoalsInWorld()
        {
            int emptySlots = shoalManager.freeSchoolIndices.Count;
            int allSlots = shoalManager.schools.Count;
            return Mathf.Abs(emptySlots - allSlots);
        }

        Vector3 GetRandomSpawnPosition()
        {
            Vector3 pos = Player.main.transform.position + (Random.onUnitSphere * 50f);
            return new Vector3(pos.x, Mathf.Clamp(pos.y, -9999f, -5f), pos.z);
        }
    }
}
