using HarmonyLib;
using SMLHelper.V2.Utility;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;
using UWE;

namespace RotA.Patches
{
    [HarmonyPatch(typeof(LaunchRocket))]
    public static class LaunchRocket_Patches
    {
        [HarmonyPatch(nameof(LaunchRocket.RevealPlanet))]
        [HarmonyPostfix]
        static void RevealPlanet_Patch(LaunchRocket __instance)
        {
            var planetMainTexture = ImageUtils.LoadTextureFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", "Planetskin.png"));
            var planetSpecTexture = ImageUtils.LoadTextureFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Assets", "Planetspec.png"));
            if (planetMainTexture == null || planetSpecTexture == null)
            {
                QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Error, "Couldn't find the planet texture, skipping the replacement", showOnScreen: true);
                return;
            }

            var renderers = __instance.planetGo.GetAllComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                if (renderer.name.Contains("Planet"))
                {
                    renderer.material.mainTexture = planetMainTexture;
                    renderer.material.SetTexture(ShaderPropertyID._SpecTex, planetSpecTexture);
                    QModManager.Utility.Logger.Log(QModManager.Utility.Logger.Level.Info, "Successfully changed the Planet textures.", showOnScreen: true);
                    return;
                }
            }
        }
        [HarmonyPatch(nameof(LaunchRocket.HideCrashedShip))]
        [HarmonyPostfix]
        public static void HideCrashedShip_Patch()
        {
            CoroutineHost.StartCoroutine(PlayGargRoarDelayed());
        }

        static IEnumerator PlayGargRoarDelayed()
        {
            yield return new WaitForSeconds(19f);
            GameObject roarObj = new GameObject("RocketRoar");
            var src = roarObj.AddComponent<AudioSource>();
            src.volume = ECCLibrary.ECCHelpers.GetECCVolume();
            src.clip = ECCLibrary.ECCAudio.LoadAudioClip("garg_roar-006");
            src.loop = false;
            src.playOnAwake = false;
            src.spatialBlend = 1f;
            src.transform.position = Player.main.transform.position + new Vector3(-60f, -15f, -30f);
            src.minDistance = 60f;
            src.maxDistance = 300f;
            src.Play();
            GameObject.Destroy(roarObj, 19f);
        }
    }
}
