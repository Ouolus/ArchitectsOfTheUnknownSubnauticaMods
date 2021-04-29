using System;
using System.Collections;
using UnityEngine;

namespace ProjectAncients.Mono
{
    public class VoidGargSpawner : MonoBehaviour
    {
        private bool playerWasInVoid = false;
        private float timeToSpawnGarg;
        private TechType adultPrefab;
        private const float spawnOutDistance = 100f;
        private const float spawnYLevel = -400;
        private const float leashYOffset = 300f;

        bool coroutinePlaying = false;

        private void Start()
        {
            InvokeRepeating("UpdateSpawn", 1f, 4f);
            adultPrefab = Mod.gargVoidPrefab.TechType;
        }

        private void UpdateSpawn()
        {
            Player player = Player.main;
            if (player)
            {
                bool playerInVoidNow = IsVoidBiome(player.GetBiomeString());
                if (playerWasInVoid != playerInVoidNow)
                {
                    if (playerInVoidNow == true)
                    {
                        timeToSpawnGarg = Time.time + 10f;
                    }
                }
                playerWasInVoid = playerInVoidNow;
            }
        }

        private void Update()
        {
            if (playerWasInVoid && Time.time > timeToSpawnGarg)
            {
                if (!VoidGargSingleton.AdultGargExists && !coroutinePlaying)
                {
                    StartCoroutine(AdultSpawner());
                }
            }
        }

        IEnumerator AdultSpawner()
        {
            coroutinePlaying = true;
            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(adultPrefab);
            yield return task;
            
            var obj = task.GetResult();
            Vector3 gargSpawnPoint = GetGargSpawnPoint(Player.main.transform.position);
            GameObject newGargantuan = Instantiate(obj, gargSpawnPoint, Quaternion.LookRotation(Vector3.up));
            newGargantuan.SetActive(true);
            newGargantuan.AddComponent<SetLeashPositionDelayed>().leashPosition = gargSpawnPoint + leashYOffset;
            coroutinePlaying = false;
        }

        public static bool IsVoidBiome(string biomeName)
        {
            return string.Equals(biomeName, "void", StringComparison.OrdinalIgnoreCase) || biomeName == string.Empty;
        }

        private static Vector3 GetGargSpawnPoint(Vector3 playerWorldPosition)
        {
            Vector3 playerPositionAtY0 = new Vector3(playerWorldPosition.x, 0f, playerWorldPosition.z);
            Vector3 directionToAbyss = playerPositionAtY0.normalized;
            Vector3 spawnOffset = directionToAbyss * spawnOutDistance;
            Vector3 spawnPosition = playerWorldPosition + spawnOffset;
            Vector3 spawnPositionWithCorrectYLevel = new Vector3(spawnPosition.x, Mathf.Clamp(playerWorldPosition.y + spawnYLevel, -10000f, spawnYLevel), spawnPosition.z);
            return spawnPositionWithCorrectYLevel;
        }
    }
}
