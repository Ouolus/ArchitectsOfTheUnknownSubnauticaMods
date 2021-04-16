using System.Collections;
using UnityEngine;
using UWE;

namespace ProjectAncients.Mono
{
    public class VoidShoalSpawner : MonoBehaviour
    {
        GameObject shoalPrefab;
        VFXSchoolFishManager shoalManager;
        private const string shoalClassId = "2d3ea578-e4fa-4246-8bc9-ed8e66dec781";
        private const int shoalCap = 70;

        private IEnumerator Start()
        {
            shoalManager = VFXSchoolFishManager.main;
            yield return SetShoalPrefabAsync();
            InvokeRepeating("UpdateSpawn", 1f, 4f);
        }

        private IEnumerator SetShoalPrefabAsync()
        {
            IPrefabRequest request = PrefabDatabase.GetPrefabAsync(shoalClassId);
            yield return request;
            if(request.TryGetPrefab(out GameObject prefab))
            {
                shoalPrefab = GameObject.Instantiate(prefab);
                shoalPrefab.SetActive(false);
                shoalPrefab.name = "Spinefish_Void_School";
                Renderer renderer = shoalPrefab.GetComponentInChildren<Renderer>();
                Material material = renderer.material;
                material.SetColor("_GlowColor", new Color(0.99f, 0.97f, 0.81f));
                material.SetFloat("_GlowStrength", 1.50f);
                material.SetFloat("_GlowStrengthNight", 1.50f);
                renderer.material = material;
                renderer.transform.localScale = Vector3.one * 1.50f;
                GameObject.Destroy(shoalPrefab.GetComponent<LargeWorldEntity>());
                GameObject.Destroy(shoalPrefab.GetComponent<PrefabIdentifier>());
                shoalPrefab.AddComponent<DestroyWhenFarAway>();
            }
            else
            {
                ECCLibrary.Internal.ECCLog.AddMessage("Failed to grab VoidShoalSpawner shoal prefab");
            }
        }

        void UpdateSpawn()
        {
            if(GetAllShoalsInWorld() < shoalCap)
            {
                Instantiate(shoalPrefab, GetRandomSpawnPosition(), Random.rotation).SetActive(true);
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
            return Player.main.transform.position + (Random.onUnitSphere * 50f);
        }
    }
}
