using System.Collections;
using UnityEngine;
using UWE;

namespace RotA.Mono
{
    public class VoidShoalSpawner : MonoBehaviour
    {
        GameObject shoalPrefab_Spinefish;
        VFXSchoolFishManager shoalManager;
        private const string spinefishShoalClassId = "2d3ea578-e4fa-4246-8bc9-ed8e66dec781";
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
            if (request.TryGetPrefab(out GameObject prefab))
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
            return shoalPrefab_Spinefish;
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
