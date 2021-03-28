using ECCLibrary;
using ECCLibrary.Internal;
using HarmonyLib;
using UnityEngine;

namespace ProjectAncients.Patches
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
            GameObject referenceMusic = GameObject.Instantiate(musicParent.SearchChild("precursorCave"), musicParent.transform);
            referenceMusic.name = "voidAmbience";
            referenceMusic.GetComponent<FMODGameParams>().onlyInBiome = "void";
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(WaterBiomeManager), nameof(WaterBiomeManager.Start))]
        public static void WaterBiomeManager_Start_Postfix(WaterBiomeManager __instance)
        {
            WaterscapeVolume.Settings voidWaterscapeSettings = new WaterscapeVolume.Settings()
            {
                absorption = new Vector3(6f, 6f, 6f),
                ambientScale = 0f,
                emissiveScale = 0f,
                sunlightScale = 1f,
                murkiness = 0.5f,
                startDistance = 50f,
                scatteringColor = Color.green,
                temperature = 0f,
                scattering = 0.15f
            };
            PatchBiomeFog(__instance, "void", voidWaterscapeSettings);
            PatchBiomeFog(__instance, string.Empty, voidWaterscapeSettings);
        }

        static void PatchBiomeFog(WaterBiomeManager waterBiomeManager, string biomeName, WaterscapeVolume.Settings waterScapeSettings)
        {
            if (!waterBiomeManager.biomeLookup.ContainsKey(biomeName))
            {
                WaterBiomeManager.BiomeSettings biomeSettings = new WaterBiomeManager.BiomeSettings()
                {
                    name = biomeName,
                    skyPrefab = null,
                    settings = waterScapeSettings
                };
                waterBiomeManager.biomeSettings.Add(biomeSettings);
                int indexForNew = waterBiomeManager.biomeSettings.Count - 1;
                waterBiomeManager.biomeLookup.Add(biomeName, indexForNew);
            }
        }
    }
}
