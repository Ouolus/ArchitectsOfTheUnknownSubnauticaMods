using UnityEngine;

namespace ProjectAncients.Mono
{
    public class MainMenuAtmosphereUpdater : MonoBehaviour
    {
        void Start()
        {
            uSkyManager skyManager = FindObjectOfType<uSkyManager>();
            skyManager.Timeline = 4f;
            skyManager.StarIntensity = 2f;
            skyManager.NightSky = uSkyManager.NightModes.Rotation;
            skyManager.planetRadius = 500f;
        }
    }
}
