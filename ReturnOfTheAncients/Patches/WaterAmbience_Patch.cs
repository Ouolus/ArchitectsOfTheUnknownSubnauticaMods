using ECCLibrary;
using HarmonyLib;
using UnityEngine;

namespace RotA.Patches
{
    [HarmonyPatch]
    class VoidAmbience_Patches
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(WaterAmbience), nameof(WaterAmbience.Start))]
        public static void WaterAmbience_Start_Postfix(WaterAmbience __instance)
        {
            GameObject ambienceParent = __instance.gameObject.SearchChild("background");
            GameObject voidAmbience = GameObject.Instantiate(ambienceParent.SearchChild("arcticAmbience"), ambienceParent.transform);
            voidAmbience.name = "voidAmbience";
            voidAmbience.GetComponent<FMODGameParams>().onlyInBiome = "void";

            GameObject musicParent = __instance.gameObject.SearchChild("music");
            GameObject referenceMusic = GameObject.Instantiate(musicParent.SearchChild("dunes"), musicParent.transform);
            referenceMusic.name = "voidAmbience";
            referenceMusic.GetComponent<FMODGameParams>().onlyInBiome = "void";
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
                murkiness = 0.7f,
                startDistance = 100f,
                scatteringColor = new Color(0f, 0.2f, 0.02f),
                temperature = 5f,
                scattering = 0.25f
            };
            PatchBiomeFog(__instance, "void", voidWaterscapeSettings, __instance.biomeSkies[0]);
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
                int indexForNew = waterBiomeManager.biomeSettings.Count - 1;
                waterBiomeManager.biomeLookup.Add(biomeName, indexForNew);
            }
        }
    }
}
