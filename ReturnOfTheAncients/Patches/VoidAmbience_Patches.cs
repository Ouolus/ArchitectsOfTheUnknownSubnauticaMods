using ECCLibrary;
using HarmonyLib;
using UnityEngine;

namespace RotA.Patches
{
    [HarmonyPatch]
    class VoidAmbience_Patches
    {
        private const string voidBiomeName = "void";

        private static int voidBiomeIndex = -1;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(WaterAmbience), nameof(WaterAmbience.Start))]
        public static void WaterAmbience_Start_Postfix(WaterAmbience __instance)
        {
            PatchBiomeSounds(__instance.gameObject, voidBiomeName, "arcticAmbience", "dunes");
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(WaterBiomeManager), nameof(WaterBiomeManager.Start))]
        public static void WaterBiomeManager_Start_Postfix(WaterBiomeManager __instance)
        {
            if (__instance.biomeSkies.Count < 3) //a check to see if the main menu water biome manager is loaded, rather than the main one. if we don't end the method here, the game will throw an exception.
            {
                return;
            }
            WaterscapeVolume.Settings voidWaterscapeSettings = new WaterscapeVolume.Settings()
            {
                absorption = new Vector3(40, 15f, 9f) / 5f,
                ambientScale = 0.5f,
                emissiveScale = 0f,
                sunlightScale = 1.1f,
                murkiness = 0.82f,
                startDistance = 100f,
                scatteringColor = new Color(0.3f, 0.3f, 0.3f),
                temperature = 5f,
                scattering = 0.25f
            };
            PatchBiomeFog(__instance, voidBiomeName, voidWaterscapeSettings, __instance.biomeSkies[22]);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(WaterBiomeManager), nameof(WaterBiomeManager.GetBiomeIndex))]
        public static void WaterBiomeManager_GetBiomeIndex_Postfix(ref int __result)
        {
            if (__result == -1)
            {
                __result = voidBiomeIndex;
            }
        }

        static void PatchBiomeSounds(GameObject waterAmbienceParent, string biomeName, string ambienceReference, string musicReference)
        {
            GameObject ambienceParent = waterAmbienceParent.SearchChild("background");
            GameObject voidAmbience = GameObject.Instantiate(ambienceParent.SearchChild(ambienceReference), ambienceParent.transform);
            voidAmbience.name = $"{biomeName}Ambience";
            voidAmbience.GetComponent<FMODGameParams>().onlyInBiome = biomeName;

            GameObject musicParent = waterAmbienceParent.SearchChild("music");
            GameObject referenceMusic = GameObject.Instantiate(musicParent.SearchChild(musicReference), musicParent.transform);
            referenceMusic.name = $"{biomeName}Ambience";
            referenceMusic.GetComponent<FMODGameParams>().onlyInBiome = biomeName;
        }

        static void PatchBiomeFog(WaterBiomeManager waterBiomeManager, string biomeName, WaterscapeVolume.Settings waterScapeSettings, mset.Sky sky)
        {
            if (!waterBiomeManager.biomeLookup.ContainsKey(biomeName))
            {
                GameObject skyPrefab = null;
                if (sky)
                {
                    skyPrefab = sky.gameObject;
                }
                WaterBiomeManager.BiomeSettings biomeSettings = new WaterBiomeManager.BiomeSettings()
                {
                    name = biomeName,
                    skyPrefab = skyPrefab,
                    settings = waterScapeSettings
                };
                waterBiomeManager.biomeSkies.Add(sky);
                waterBiomeManager.biomeSettings.Add(biomeSettings);
                voidBiomeIndex = waterBiomeManager.biomeSettings.Count - 1;
                waterBiomeManager.biomeLookup.Add(biomeName, voidBiomeIndex);
            }
        }
    }
}
