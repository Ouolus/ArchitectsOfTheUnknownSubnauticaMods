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
            GameObject ambienceParent = __instance.gameObject.SearchChild("background");
            GameObject voidAmbience = GameObject.Instantiate(ambienceParent.SearchChild("arcticAmbience"), ambienceParent.transform);
            voidAmbience.name = "voidAmbience";
            voidAmbience.GetComponent<FMODGameParams>().onlyInBiome = voidBiomeName;

            GameObject musicParent = __instance.gameObject.SearchChild("music");
            GameObject referenceMusic = GameObject.Instantiate(musicParent.SearchChild("dunes"), musicParent.transform);
            referenceMusic.name = "voidAmbience";
            referenceMusic.GetComponent<FMODGameParams>().onlyInBiome = voidBiomeName;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(WaterBiomeManager), nameof(WaterBiomeManager.Start))]
        public static void WaterBiomeManager_Start_Postfix(WaterBiomeManager __instance)
        {
            WaterscapeVolume.Settings voidWaterscapeSettings = new WaterscapeVolume.Settings()
            {
                absorption = new Vector3(7f, 6f, 6f) / 2f,
                ambientScale = 0f,
                emissiveScale = 0f,
                sunlightScale = 1f,
                murkiness = 0.8f,
                startDistance = 130f,
                scatteringColor = new Color(0f, 0.2f, 0.02f),
                temperature = 5f,
                scattering = 0.25f
            };
            PatchBiomeFog(__instance, voidBiomeName, voidWaterscapeSettings, __instance.biomeSkies[6]);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(WaterBiomeManager), nameof(WaterBiomeManager.GetBiomeIndex))]
        public static void WaterBiomeManager_GetBiomeIndex_Postfix(WaterBiomeManager __instance, string name, ref int __result)
        {
            if (__result == -1)
            {
                __result = voidBiomeIndex;
            }
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
